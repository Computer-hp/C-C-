using System;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Collections.Generic;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const int boardSize = 8;
        private const int squareSize = 50;
        private int turn = 0; // 0 for white, 1 for black
        private string currentPlayer = "";

        private string[] rowLetter = { "a", "b", "c", "d", "e", "f", "g", "h"};

        CPiece selectedPiece = null;

        private CMatrixBoard ChessBoard;

        public Form1()
        {
            ChessBoard = new CMatrixBoard();

            InitializeComponent();
            ChessBoard.InitializePieces(); //forse sopra riga 16
            InitializeChessBoard();
        }
        private void InitializeChessBoard()
        {
            // Calculate the position to center the chessboard
            int centerX = (ClientSize.Width - boardSize * squareSize) / 2;
            int centerY = (ClientSize.Height - boardSize * squareSize) / 2;

            int x = 0, y = boardSize - 1; // Start from the bottom-left corner

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Button square = new Button
                    {
                        Size = new Size(squareSize, squareSize),
                        Location = new Point(centerX + col * squareSize, centerY + row * squareSize),
                        BackColor = (row + col) % 2 == 0 ? Color.Ivory : Color.Brown,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 0 }
                    };

                    Controls.Add(square);
                    square.Tag = (x, y);

                    if (ChessBoard.Board[x, y] != null)
                        square.Text = ChessBoard.Board[x, y].pieceName;

                    // click event handler
                    square.Click += Button_Click;

                    if (x < boardSize - 1)
                    {
                        x++;
                    }
                    else
                    {
                        x = 0;
                        y--;
                    }
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            var position = (ValueTuple<int, int>)clickedButton.Tag;
            int x = position.Item1;
            int y = position.Item2;

            if (turn == 0)
                currentPlayer = "White";
            else
                currentPlayer = "Black";

            if (ChessBoard.Board[x, y] != null && ChessBoard.Board[x, y].pieceType == currentPlayer)
            {
                selectedPiece = ChessBoard.Board[x, y];
                
                ChessBoard.validMoves.Clear();

                ChessBoard = AvaibleSquares(ChessBoard, selectedPiece);

                if (selectedPiece.pieceName == "K")
                    ChessBoard = SaveInvalidSquares(ChessBoard, selectedPiece);

                Debug.WriteLine(ChessBoard.ToString());
            }
            else if (selectedPiece != null)
            {
                if ((ChessBoard.Board[x, y] == null || ChessBoard.Board[x, y].pieceType != currentPlayer) && ChessBoard.validMoves.Exists(item => item.x == x && item.y == y) == true)
                {
                    Button originalSquare = GetButtonAtPosition(selectedPiece.x, selectedPiece.y);
                    originalSquare.Text = "";

                    ChessBoard.Board[selectedPiece.x, selectedPiece.y] = null;

                    // pawn promotions
                    if (selectedPiece.pieceName == "P" && (selectedPiece.y + 1 == 7 && selectedPiece.pieceType == "White" || selectedPiece.y - 1 == 0 && selectedPiece.pieceType == "Black"))
                    {
                        var promotion = new Form2();
                        promotion.ShowDialog();

                        ChessBoard.Board[x, y] = new CPiece(selectedPiece.x, selectedPiece.y, promotion.pieceName, selectedPiece.pieceType);
                    } else
                    {
                        ChessBoard.Board[x, y] = selectedPiece;
                    }

                    ChessBoard.Board[x, y].x = x;
                    ChessBoard.Board[x, y].y = y;
                    clickedButton.Text = ChessBoard.Board[x, y].pieceName;

                    ChessBoard.validMoves.Clear();

                    // Switch the current player's turn
                    turn = (turn + 1) % 2; // Toggle between 0 (white) and 1 (black)
                    selectedPiece = null;
                }
            }
            else
            {
                selectedPiece = null;
                ChessBoard.validMoves.Clear();
            }

        }
        private Button GetButtonAtPosition(int x, int y)
        {
            // Find the button at the specified position
            foreach (var button in Controls.OfType<Button>())
            {
                var position = (ValueTuple<int, int>)button.Tag;
                if (position.Item1 == x && position.Item2 == y)
                {
                    return button;
                }
            }
            return null; // Button not found
        }
        private CMatrixBoard SaveInvalidSquares(CMatrixBoard B, CPiece P)
        {
            // calculate the squares of every piece and compare with validMoves, if there are == moves, save it in the InvalidSquares
            B.InvalidSquaresKing.Clear();

            B.InvalidSquaresKing.AddRange(B.validMoves);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    B.validMoves.Clear();

                    if (B.Board[i, j] != null && B.Board[i, j].pieceType != P.pieceType && B.Board[i, j].pieceName != P.pieceName)
                    {
                        B = AvaibleSquares(B, B.Board[i, j]);

                        B.InvalidSquaresKing.RemoveAll(item => B.validMoves.Contains(item));
                    }

                }
                Debug.WriteLine(B.ToString());
            }
            B.validMoves.Clear();
            B.validMoves.AddRange(B.InvalidSquaresKing);
            return B;
        }

        private CMatrixBoard AvaibleSquares(CMatrixBoard ChessBoard, CPiece P)
        {

            switch (P.pieceName)
            {
                case "P":
                    ChessBoard.Pawns(P);
                    break;

                case "R":
                    ChessBoard.Straight(P);
                    break;

                case "N":
                    ChessBoard.Jump(P);
                    ChessBoard.checkJump(P);
                    break;

                case "B":
                    ChessBoard.Diagonal(P);
                    break;

                case "Q":
                    ChessBoard.Straight(P);
                    ChessBoard.Diagonal(P);
                    break;

                case "K":
                    ChessBoard.King(P);
                    break;
            }
            return ChessBoard;
        }
    }
}