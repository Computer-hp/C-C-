using System;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Collections.Generic;
using System.Collections;
using Microsoft.VisualBasic.ApplicationServices;
using System.Configuration;
using System.Net.NetworkInformation;
using Microsoft.VisualBasic.Devices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const int boardSize = 8;
        private const int squareSize = 50;
        private int turn = 0; // 0 for white, 1 for black
        private string currentPlayer = "";

        private string[] rowLetter = { "a", "b", "c", "d", "e", "f", "g", "h" };

        private bool[] firstKingMove = { false, false };

        private bool[] aRookFirstMove = { false, false };
        private bool[] hRookFirstMove = { false, false };

        private bool[] O_O = { false, false };

        private bool[] O_O_O = { false, false };

        private bool check = false;

        private CPiece selectedPiece = null;

        private string direction = "";

        private CMatrixBoard ChessBoard;

        public static string projectPath = (System.Environment.CurrentDirectory).Replace("WinFormsApp1\\bin\\Debug\\net6.0-windows", "");


        public Form1()
        {
            ChessBoard = new CMatrixBoard();

            InitializeComponent();
            ChessBoard.InitializePieces();
            InitializeChessBoard(ChessBoard);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeChessBoard(CMatrixBoard ChessBoard)
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
                        FlatAppearance = { BorderSize = 0 },
                        BackgroundImageLayout = ImageLayout.Zoom
                };

                    Controls.Add(square);
                    square.Tag = (x, y);

                    if (ChessBoard.Board[x, y] != null)
                    {
                        Bitmap resizedImage = SetImageToButton(ChessBoard.Board[x, y]);
                        square.BackgroundImage = resizedImage;
                    }

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
        // TODO set background image instead of image, and set zoom
        public static Bitmap SetImageToButton(CPiece P)
        {
            string DIR = "images\\" + P.pieceType;

            string imagePath = DIR + "\\" + P.pieceName + ".png";

            Bitmap originalImage = (Bitmap)Image.FromFile(projectPath + imagePath);
            return originalImage;
        }


        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            var position = (ValueTuple<int, int>)clickedButton.Tag;
            int x = position.Item1;
            int y = position.Item2;


            currentPlayer = (turn == 0) ? "White" : "Black";

            int Y = (currentPlayer == "White") ? 0 : 7;


            if (ChessBoard.Board[x, y] == null || ChessBoard.Board[x, y].pieceType != currentPlayer)
            {
                if (!ChessBoard.validMoves.Exists(item => item.x == x && item.y == y))
                    return;

                if (selectedPiece == null)
                    return;

                int previous_piece_x = selectedPiece.x, previous_piece_y = selectedPiece.y;

                // check handle
                if (check)
                {
                    Tuple<int, int> key = Tuple.Create(selectedPiece.x, selectedPiece.y);
                    ChessBoard.copyMoves.Clear();

                    if (selectedPiece.pieceName != "K" && ChessBoard.stopCheckWithPiece.ContainsKey(key))
                        ChessBoard.copyMoves.AddRange(ChessBoard.stopCheckWithPiece[key]);
                    else
                        ChessBoard.copyMoves.AddRange(ChessBoard.validMoves);

                    if (!ChessBoard.copyMoves.Exists(square => ChessBoard.validMoves.Exists(checkSquare => checkSquare.x == square.x && checkSquare.y == square.y)))
                        return;
                }

                if (selectedPiece.pieceName == "P")
                {
                    if (!DiagonalMovementPawn(ChessBoard, selectedPiece, x, y))
                        return;

                    PawnPromotion(ChessBoard, selectedPiece, x, y); 
                }

                // cancel the image at the previous position of the piece
                Button originalSquare = GetButtonAtPosition(previous_piece_x, previous_piece_y);
                originalSquare.BackgroundImage = null;

                ChessBoard.Board[previous_piece_x, previous_piece_y] = null;

                ChessBoard.Board[x, y] = selectedPiece;


                if (selectedPiece.pieceName == "K" && !firstKingMove[turn])
                    ChessBoard = FirstKingMove(ChessBoard, x, y, Y);

                if (selectedPiece.pieceName == "R")
                    FirstRookMove(ChessBoard, selectedPiece, x, y, Y);

                
                ChessBoard.Board[x, y].x = x;
                ChessBoard.Board[x, y].y = y;

                CPiece king = FindKing(ChessBoard, currentPlayer);

                // finds only the moves of the piece that gives check which are after used in StopCheck
                if (selectedPiece.pieceName != "K")
                {

                    direction = "";

                    if (selectedPiece.pieceName == "R")
                    {
                        FindStraightDirection(king);
                        ChessBoard.Straight(selectedPiece, 8, direction);
                    }
                    if (selectedPiece.pieceName == "B")
                    {
                        FindDiagonalyDirection(king);
                        ChessBoard.Diagonal(selectedPiece, 8, direction);
                    }
                    if (selectedPiece.pieceName == "Q")
                    {
                        FindStraightDirection(king);
                        ChessBoard.Straight(selectedPiece, 8, direction);
                        IsCheck(ChessBoard, ChessBoard.Board[x, y]);
                        
                        if (!check)
                        {
                            ChessBoard.validMoves.Clear();
                            FindDiagonalyDirection(king);
                            ChessBoard.Diagonal(selectedPiece, 8, direction);
                        }
                    }

                    // because the piece that gives check can also be captured to stop check, neccessary for Knight and Pawn
                    ChessBoard.validMoves.Add(new CSquare(selectedPiece.x, selectedPiece.y));

                    IsCheck(ChessBoard, ChessBoard.Board[x, y]);

                    Debug.WriteLine("One direction: " + ChessBoard.ToString());

                }

                Bitmap image = SetImageToButton(ChessBoard.Board[x, y]);

                clickedButton.BackgroundImage = image;


                // TODO check if there are moves that can stop the check, if not it's checkmate
                if (check)
                {
                    ChessBoard.copyMoves.Clear();
                    ChessBoard.copyMoves.AddRange(ChessBoard.validMoves);

                    foreach (var piece in ChessBoard.Board)
                    {
                        if (piece != null && piece.pieceType != selectedPiece.pieceType && piece.pieceName != "K")
                        {

                            /*
                            method that takes x, y of the KING, if it's ROOK, QUEEN or BISHOP
                            check if squares in between can be ocupied.
                            
                            if it's KNIGH then only check if opponent can capture.
                             */

                            ChessBoard = AvaibleSquares(ChessBoard, piece);
                            ChessBoard = StopCheck(ChessBoard, piece);

                        }
                    }
                    // check valid moves for King
                    ChessBoard = AvaibleSquares(ChessBoard, king);
                    ChessBoard = SaveInvalidSquares(ChessBoard, king);

                    // Check mate
                    if (!ChessBoard.validMoves.Any() && !ChessBoard.stopCheckWithPiece.Any(kv => kv.Value != null && kv.Value.Count > 0))
                    {
                        var popUp = new Form3();
                        popUp.ShowDialog();
                    }
                }
                ChessBoard.validMoves.Clear();

                // Switch the current player's turn
                turn = (turn + 1) % 2;

                selectedPiece = null;

                return;

            }

            ChessBoard = AvaibleSquares(ChessBoard, ChessBoard.Board[x, y]);
            selectedPiece = ChessBoard.Board[x, y];

            Debug.WriteLine(selectedPiece.pieceName);
            Debug.WriteLine(ChessBoard.ToString() + "\n");

            if (selectedPiece.pieceName != "K")
                return;

            ChessBoard = SaveInvalidSquares(ChessBoard, selectedPiece);


            // Check if  O-O  or  O-O-O  is possible.
            if (ChessBoard.Board[6, Y] == null && ChessBoard.validMoves.Exists(item => item.x == 5 && item.y == Y)
                && !firstKingMove[turn] && !hRookFirstMove[turn])
            {

                ChessBoard.validMoves.Add(new CSquare(6, Y));
                O_O[turn] = true;
            }
            else
                O_O[turn] = false;


            if (ChessBoard.Board[2, Y] == null && ChessBoard.Board[3, Y] == null
                && ChessBoard.validMoves.Exists(item => item.x == 3 && item.y == Y)
                && !firstKingMove[turn] && !aRookFirstMove[turn])
            {

                ChessBoard.validMoves.Add(new CSquare(2, Y));
                O_O_O[turn] = true;
            }
            else
                O_O_O[turn] = false;
        }

        private void FindStraightDirection(CPiece king)
        {
            if (selectedPiece.x > king.x && selectedPiece.y == king.y)
                direction = "Left";

            if (selectedPiece.x < king.x && selectedPiece.y == king.y)
                direction = "Right";

            if (selectedPiece.y > king.y && selectedPiece.x == king.x)
                direction = "Down";

            if (selectedPiece.y < king.y && selectedPiece.x == king.x)
                direction = "Up";
        }

        private void FindDiagonalyDirection(CPiece king)
        {
            if (selectedPiece.x > king.x && selectedPiece.y > king.y)
                direction = "LeftDown";

            if (selectedPiece.x < king.x && selectedPiece.y > king.y)
                direction = "RightUp";

            if (selectedPiece.y > king.y && selectedPiece.x < king.x)
                direction = "RightDown";

            if (selectedPiece.y < king.y && selectedPiece.x > king.x)
                direction = "LeftUp";
        }

        // Find the button at the specified position
        private Button GetButtonAtPosition(int x, int y)
        {
            foreach (var button in Controls.OfType<Button>())
            {
                var position = (ValueTuple<int, int>)button.Tag;
                if (position.Item1 == x && position.Item2 == y)
                {
                    return button;
                }
            }
            return null;
        }
        // Calculates the squares of every piece and compares with validMoves, if there are == moves, saves them in InvalidSquares
        private CMatrixBoard SaveInvalidSquares(CMatrixBoard B, CPiece P)
        {
            B.invalidSquaresKing.Clear();

            B.invalidSquaresKing.AddRange(B.validMoves);

            foreach (var piece in B.Board)
            {
                if (piece != null && piece.pieceType != P.pieceType && piece.pieceName != P.pieceName)
                {
                    B = AvaibleSquares(B, piece);

                    if (piece.pieceName == "P")
                        if (piece.pieceType == "White")
                            B.validMoves.RemoveAll(square => square.x == piece.x && square.y == piece.y + 1);
                        else
                            B.validMoves.RemoveAll(square => square.x == piece.x && square.y == piece.y - 1);

                    B.invalidSquaresKing.RemoveAll(square => B.validMoves.Exists(move => move.x == square.x && move.y == square.y));
                }
            }

            B.validMoves.Clear();
            B.validMoves.AddRange(B.invalidSquaresKing);
            return B;
        }

        private void IsCheck(CMatrixBoard B, CPiece P)
        {
            CPiece king = FindKing(B, currentPlayer);

            if (B.validMoves.Exists(move => move.x == king.x && move.y == king.y) == true)
            {
                check = true;
            }
        }

        private CMatrixBoard StopCheck(CMatrixBoard ChessBoard, CPiece Piece)
        {
            List<CSquare> tmpMoves = new List<CSquare>();

            Tuple<int, int> key;

            foreach (var square in ChessBoard.validMoves)
            {
                if (ChessBoard.copyMoves.Exists(move => move.x == square.x && move.y == square.y))
                {
                    tmpMoves.Add(square);
                }
            }
            Debug.WriteLine("tmpMoves of " +  Piece.pieceName + " =" );

            foreach (var moves in tmpMoves)
            {
                Debug.WriteLine(moves.x + ", " + moves.y);
            }
            Debug.WriteLine("\n");

            if (tmpMoves.Any())
            {
                Debug.WriteLine("Entered");
                key = Tuple.Create(Piece.x, Piece.y);
                ChessBoard.stopCheckWithPiece[key] = tmpMoves;
            }

            return ChessBoard;
        }

        private CMatrixBoard FirstKingMove(CMatrixBoard ChessBoard, int x, int y, int Y)
        {
            firstKingMove[turn] = true;

            if (O_O[turn] && x == 6 && y == Y)
                ChessBoard = ShortCastle(ChessBoard, x, y, Y);

            if (O_O_O[turn] && x == 2 && y == Y)
                ChessBoard = LongCastle(ChessBoard, x, y, Y);

            return ChessBoard;
        }
        // TODO merge Short and Long Castle
        private CMatrixBoard ShortCastle(CMatrixBoard ChessBoard, int x, int y, int Y)
        {
            var tmp = ChessBoard.Board[7, Y];  // copy the rook
            ChessBoard.Board[7, Y] = null;
            ChessBoard.Board[5, Y] = tmp;

            Button RookSquare = GetButtonAtPosition(7, Y);
            RookSquare.BackgroundImage = null;

            Bitmap RookImage = SetImageToButton(tmp);

            RookSquare = GetButtonAtPosition(5, Y);
            RookSquare.BackgroundImage = RookImage;

            return ChessBoard;
        }
        // TODO merge Short and Long Castle
        private CMatrixBoard LongCastle(CMatrixBoard ChessBoard, int x, int y, int Y)
        {
            var tmp = ChessBoard.Board[7, Y];  // copy the rook
            ChessBoard.Board[0, Y] = null;
            ChessBoard.Board[3, Y] = tmp;

            Button RookSquare = GetButtonAtPosition(0, Y);
            RookSquare.BackgroundImage = null;

            Bitmap RookImage = SetImageToButton(tmp);

            RookSquare = GetButtonAtPosition(3, Y);
            RookSquare.BackgroundImage = RookImage;

            return ChessBoard;
        }

        private void FirstRookMove(CMatrixBoard ChessBoard, CPiece selectedPiece, int x, int y, int Y)
        {
            if (selectedPiece.x == 0 && selectedPiece.y == Y)
                aRookFirstMove[turn] = true;
            else if (selectedPiece.x == 7 && selectedPiece.y == Y)
                hRookFirstMove[turn] = true;
        }

        private CPiece FindKing(CMatrixBoard B, string currentPlayer)
        {
            foreach (var piece in B.Board)
            {
                if (piece != null && piece.pieceType != currentPlayer && piece.pieceName == "K")
                {
                    return piece;
                }
            }
            return null;
        }

        private bool DiagonalMovementPawn(CMatrixBoard ChessBoard, CPiece selectedPiece, int x, int y)
        {
            if (selectedPiece.x != x && (ChessBoard.Board[x, y] == null ||
               ChessBoard.Board[x, y].pieceName == "K"))
                return false;

            return true;
        }

        private void PawnPromotion(CMatrixBoard ChessBoard, CPiece selectedPiece, int x, int y)
        {
            if (selectedPiece.y + 1 == 7 && selectedPiece.pieceType == "White" ||
                selectedPiece.y - 1 == 0 && selectedPiece.pieceType == "Black")
            {

                var promotion = new Form2(turn);
                promotion.ShowDialog();

                this.selectedPiece = new CPiece(x, y, promotion.pieceName, selectedPiece.pieceType);

                Debug.WriteLine("Promotion: " + promotion.pieceName);

            }
        }

        private CMatrixBoard AvaibleSquares(CMatrixBoard ChessBoard, CPiece P)
        {
            ChessBoard.validMoves.Clear();

            switch (P.pieceName)
            {
                case "P":
                    ChessBoard.Pawns(P);
                    break;

                case "R":
                    ChessBoard.Straight(P, 8, "");
                    break;

                case "N":
                    ChessBoard.Jump(P);
                    ChessBoard.checkJump(P);
                    break;

                case "B":
                    ChessBoard.Diagonal(P, 8, "");
                    break;

                case "Q":
                    ChessBoard.Straight(P, 8, "");
                    ChessBoard.Diagonal(P, 8, "");
                    break;

                case "K":
                    ChessBoard.Straight(P, 1, "");
                    ChessBoard.Diagonal(P, 1, "");
                    break;
            }
            return ChessBoard;
        }
    }
}