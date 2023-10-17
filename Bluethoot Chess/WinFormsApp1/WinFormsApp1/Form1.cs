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

        CPiece selectedPiece = null;

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
                        FlatAppearance = { BorderSize = 0 }
                    };

                    Controls.Add(square);
                    square.Tag = (x, y);

                    if (ChessBoard.Board[x, y] != null)
                    {
                        Bitmap resizedImage = SetImageToButton(ChessBoard.Board[x, y]);
                        square.Image = resizedImage;
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

        public static Bitmap SetImageToButton(CPiece P)
        {
            string DIR = P.pieceType;

            string imagePath = DIR + "\\" + P.pieceName + ".png";

            Bitmap resizedImage = null;
            try
            {
                using (Bitmap originalImage = (Bitmap)Image.FromFile(projectPath + imagePath))
                {
                    double maxImageSize = Math.Min(squareSize, squareSize);
                    double aspectRatio = (double)originalImage.Width / originalImage.Height;

                    int newWidth = (int)maxImageSize;
                    int newHeight = (int)(maxImageSize / aspectRatio);

                    resizedImage = new Bitmap(originalImage, newWidth, newHeight);
                    return resizedImage;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading the image: {ex.Message}");
                return resizedImage;
            }
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



                if (check && selectedPiece.pieceName != "K")
                {
                    Tuple<int, int> key = Tuple.Create(selectedPiece.x, selectedPiece.y);
                    ChessBoard.copyMoves.Clear();
                    ChessBoard.copyMoves = ChessBoard.stopCheckWithPiece[key];

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
                originalSquare.Image = null;

                ChessBoard.Board[previous_piece_x, previous_piece_y] = null;

                ChessBoard.Board[x, y] = selectedPiece; 

                if (selectedPiece.pieceName == "K" && !firstKingMove[turn])
                    ChessBoard = FirstKingMove(ChessBoard, x, y, Y);

                if (selectedPiece.pieceName == "R")
                    FirstRookMove(ChessBoard, selectedPiece, x, y, Y);

                /*else
                {
                    ChessBoard.Board[x, y] = selectedPiece;

                    if (selectedPiece.pieceName == "K" && !firstKingMove[turn])
                    {
                        firstKingMove[turn] = true;

                        if (O_O[turn] && x == 6 && y == Y)
                        {
                            var tmp = ChessBoard.Board[7, Y];  // copy the rook
                            ChessBoard.Board[7, Y] = null;
                            ChessBoard.Board[5, Y] = tmp;

                            Button RookSquare = GetButtonAtPosition(7, Y);
                            RookSquare.Image = null;

                            Bitmap RookImage = SetImageToButton(tmp);

                            RookSquare = GetButtonAtPosition(5, Y);
                            RookSquare.Image = RookImage;
                        }
                        else if (O_O_O[turn] && x == 2 && y == Y)
                        {
                            var tmp = ChessBoard.Board[7, Y];  // copy the rook
                            ChessBoard.Board[0, Y] = null;
                            ChessBoard.Board[3, Y] = tmp;

                            Button RookSquare = GetButtonAtPosition(0, Y);
                            RookSquare.Image = null;

                            Bitmap RookImage = SetImageToButton(tmp);

                            RookSquare = GetButtonAtPosition(3, Y);
                            RookSquare.Image = RookImage;
                        }
                    }
                    else if (selectedPiece.pieceName == "R")
                    {
                        if (selectedPiece.x == 0 && selectedPiece.y == Y)
                            aRookFirstMove[turn] = true;
                        else if (selectedPiece.x == 7 && selectedPiece.y == Y)
                            hRookFirstMove[turn] = true;
                    }
                }*/
                ChessBoard.Board[x, y].x = x;
                ChessBoard.Board[x, y].y = y;

                // check, evidence the square of the King if it's CHECK
                if (selectedPiece.pieceName != "K")
                {
                    ChessBoard = AvaibleSquares(ChessBoard, ChessBoard.Board[x, y]);

                    IsCheck(ChessBoard, ChessBoard.Board[x, y]);
                }

                Bitmap image = SetImageToButton(ChessBoard.Board[x, y]);

                clickedButton.Image = image;


                // TODO check if there are moves that can stop the check, if not it's checkmate
                if (check)
                {
                    ChessBoard.copyMoves = ChessBoard.validMoves;

                    foreach (var piece in ChessBoard.Board)
                    {
                        if (piece != null && piece.pieceType != selectedPiece.pieceType && piece.pieceName != "K")
                        {
                            //ChessBoard = AvaibleSquares(ChessBoard, piece);

                            /*
                            method that takes x, y of the KING, if it's ROOK, QUEEN or BISHOP
                            check if squares in between can be ocupied.
                            
                            if it's KNIGH then only check if opponent can capture.
                             */

                            /*
                            check if every piece of the oponent has the posibility to stop the check
                            with moving the king or a piece.
                            create a dictionary to save the name of the piece and the moves,
                            so when the oponent clicks the piece the program has to check
                            if that piece is the one in the dictionary and the move is equal
                            to the possible one.
                                */

                            ChessBoard = StopCheck(ChessBoard, piece);

                        }
                    }

                    ChessBoard = AvaibleSquares(ChessBoard, FindKing(ChessBoard, currentPlayer));

                    if (!ChessBoard.validMoves.Any() && !ChessBoard.stopCheckWithPiece.Any())
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


            // sistemare --> quando c'� scacco bisogna controllare se il pezzo che voglio muovere � quello che
            // posso muovere
            if (check && ChessBoard.Board[x, y].pieceName != "K")
            {
                ChessBoard = AvaibleSquares(ChessBoard, ChessBoard.Board[x, y]);
                ChessBoard = SaveInvalidSquares(ChessBoard, ChessBoard.Board[x, y]);

                if (!ChessBoard.validMoves.Any())
                    return;



            }

            //ChessBoard = AvaibleSquares(ChessBoard, selectedPiece);

            Debug.WriteLine(selectedPiece.pieceName);
            Debug.WriteLine(ChessBoard.ToString());

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

            // DELETE make castle true
            /*if (selectedPiece.pieceName == "K")
            {
                ChessBoard = SaveInvalidSquares(ChessBoard, selectedPiece);

                if (ChessBoard.Board[6, Y] == null && ChessBoard.validMoves.Exists(item => item.x == 5 && item.y == Y) == true && !firstKingMove[turn] && !hRookFirstMove[turn])
                {
                    ChessBoard.validMoves.Add(new CSquare(6, Y));
                    O_O[turn] = true;
                }
                else
                    O_O[turn] = false;


                if (ChessBoard.Board[2, Y] == null && ChessBoard.Board[3, Y] == null && ChessBoard.validMoves.Exists(item => item.x == 3 && item.y == Y) == true && !firstKingMove[turn] && !aRookFirstMove[turn])
                {
                    ChessBoard.validMoves.Add(new CSquare(2, Y));
                    O_O_O[turn] = true;
                }
                else
                    O_O_O[turn] = false;

            }*/
            // DELETE
            /*if (ChessBoard.Board[x, y] != null && ChessBoard.Board[x, y].pieceType == currentPlayer)
            {
                ChessBoard.validMoves.Clear();
                if (check && ChessBoard.Board[x, y].pieceName != "K")
                {
                    ChessBoard = AvaibleSquares(ChessBoard, ChessBoard.Board[x, y]);
                    ChessBoard = SaveInvalidSquares(ChessBoard, ChessBoard.Board[x, y]);
                    
                    if (!ChessBoard.validMoves.Any())
                        return;



                }


                selectedPiece = ChessBoard.Board[x, y];

                ChessBoard = AvaibleSquares(ChessBoard, selectedPiece);

                if (selectedPiece.pieceName == "K")
                {
                    ChessBoard = SaveInvalidSquares(ChessBoard, selectedPiece);

                    if (ChessBoard.Board[6, Y] == null && ChessBoard.validMoves.Exists(item => item.x == 5 && item.y == Y) == true && !firstKingMove[turn] && !hRookFirstMove[turn])
                    {
                        ChessBoard.validMoves.Add(new CSquare(6, Y));
                        O_O[turn] = true;
                    }
                    else
                        O_O[turn] = false;


                    if (ChessBoard.Board[2, Y] == null && ChessBoard.Board[3, Y] == null && ChessBoard.validMoves.Exists(item => item.x == 3 && item.y == Y) == true && !firstKingMove[turn] && !aRookFirstMove[turn])
                    {
                        ChessBoard.validMoves.Add(new CSquare(2, Y));
                        O_O_O[turn] = true;
                    }
                    else
                        O_O_O[turn] = false;

                }

                Debug.WriteLine(selectedPiece.pieceName);
                Debug.WriteLine(ChessBoard.ToString());
            }
            else if (selectedPiece != null)
            {
                if ((ChessBoard.Board[x, y] == null || ChessBoard.Board[x, y].pieceType != currentPlayer) && ChessBoard.validMoves.Exists(item => item.x == x && item.y == y))
                {
                    if (selectedPiece.pieceName == "P")
                        if (selectedPiece.x != x && (ChessBoard.Board[x, y] == null || ChessBoard.Board[x, y].pieceName == "K"))
                            return;

                    Button originalSquare = GetButtonAtPosition(selectedPiece.x, selectedPiece.y);
                    originalSquare.Image = null;

                    ChessBoard.Board[selectedPiece.x, selectedPiece.y] = null;

                    // pawn promotions
                    if (selectedPiece.pieceName == "P" && (selectedPiece.y + 1 == 7 && selectedPiece.pieceType == "White" || selectedPiece.y - 1 == 0 && selectedPiece.pieceType == "Black"))
                    {
                        var promotion = new Form2(turn);
                        promotion.ShowDialog();

                        ChessBoard.Board[x, y] = new CPiece(selectedPiece.x, selectedPiece.y, promotion.pieceName, selectedPiece.pieceType);
                    } 
                    else
                    {
                        ChessBoard.Board[x, y] = selectedPiece;

                        if (selectedPiece.pieceName == "K" && !firstKingMove[turn])
                        {
                            firstKingMove[turn] = true;

                            if (O_O[turn] && x == 6 && y == Y)
                            {
                                var tmp = ChessBoard.Board[7, Y];  // copy the rook
                                ChessBoard.Board[7, Y] = null;
                                ChessBoard.Board[5, Y] = tmp;

                                Button RookSquare = GetButtonAtPosition(7, Y);
                                RookSquare.Image = null;

                                Bitmap RookImage = SetImageToButton(tmp);

                                RookSquare = GetButtonAtPosition(5, Y);
                                RookSquare.Image = RookImage;
                            }
                            else if (O_O_O[turn] && x == 2 && y == Y)
                            {
                                var tmp = ChessBoard.Board[7, Y];  // copy the rook
                                ChessBoard.Board[0, Y] = null;
                                ChessBoard.Board[3, Y] = tmp;

                                Button RookSquare = GetButtonAtPosition(0, Y);
                                RookSquare.Image = null;

                                Bitmap RookImage = SetImageToButton(tmp);

                                RookSquare = GetButtonAtPosition(3, Y);
                                RookSquare.Image = RookImage;
                            }
                        } else if (selectedPiece.pieceName == "R")
                        {
                            if (selectedPiece.x == 0 && selectedPiece.y == Y)
                                aRookFirstMove[turn] = true;
                            else if (selectedPiece.x == 7 && selectedPiece.y == Y)
                                hRookFirstMove[turn] = true;
                        }
                    }
                    ChessBoard.Board[x, y].x = x;
                    ChessBoard.Board[x, y].y = y;

                    // check
                    if (selectedPiece.pieceName != "K")
                    {
                        ChessBoard.validMoves.Clear();

                        ChessBoard = AvaibleSquares(ChessBoard, ChessBoard.Board[x, y]);

                        IsCheck(ChessBoard, ChessBoard.Board[x, y]);
                    }

                    Bitmap image = SetImageToButton(ChessBoard.Board[x, y]);

                    clickedButton.Image = image;

                    


                    // TODO check if there are moves that can stop the check, if not it's checkmate
                    if (check)
                    {
                        ChessBoard.copyMoves = ChessBoard.validMoves;

                        foreach (var piece in ChessBoard.Board)
                        {
                            if (piece != null && piece.pieceType != selectedPiece.pieceType && piece.pieceName != "K")
                            {
                                ChessBoard.validMoves.Clear();
                                ChessBoard = AvaibleSquares(ChessBoard, piece);

                                
                                //check if every piece of the oponent has the posibility to stop the check
                                //with moving the king or a piece.
                                //create a dictionary to save the name of the piece and the moves,
                                //so when the oponent clicks the piece the program has to check
                                //if that piece is the one in the dictionary and the move is equal
                                //to the possible one.
                                

                                StopCheck(ChessBoard, piece);

                            }
                        }

                        ChessBoard = AvaibleSquares(ChessBoard, FindKing(ChessBoard, currentPlayer));

                        if (!ChessBoard.validMoves.Any() && !ChessBoard.stopCheckWithPiece.Any())
                        {
                            var popUp = new Form3();
                            popUp.ShowDialog();
                        }
                    }
                    ChessBoard.validMoves.Clear();

                    // Switch the current player's turn
                    turn = (turn + 1) % 2;

                    selectedPiece = null;
                }
            }*/
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
        // calculates the squares of every piece and compares with validMoves, if there are == moves, saves them in InvalidSquares
        private CMatrixBoard SaveInvalidSquares(CMatrixBoard B, CPiece P)
        {
            B.InvalidSquaresKing.Clear();

            B.InvalidSquaresKing.AddRange(B.validMoves);

            foreach (var piece in B.Board)
            {
                if (piece != null && piece.pieceType != P.pieceType && piece.pieceName != P.pieceName)
                {
                    B.validMoves.Clear();
                    B = AvaibleSquares(B, piece);

                    if (piece.pieceName == "P")
                        if (piece.pieceType == "White")
                            B.validMoves.RemoveAll(square => square.x == piece.x && square.y == piece.y + 1);
                        else
                            B.validMoves.RemoveAll(square => square.x == piece.x && square.y == piece.y - 1);

                    B.InvalidSquaresKing.RemoveAll(square => B.validMoves.Exists(move => move.x == square.x && move.y == square.y));
                }
            }

            B.validMoves.Clear();
            B.validMoves.AddRange(B.InvalidSquaresKing);
            return B;
        }

        private void IsCheck(CMatrixBoard B, CPiece P)
        {
            CPiece king = FindKing(B, currentPlayer);


            foreach (var obj in B.validMoves)
            {
                Debug.WriteLine($"x: {obj.x}, y: {obj.y}");
            }
            Debug.WriteLine($"king x: {king.x}, y: {king.y}");

            if (B.validMoves.Exists(move => move.x == king.x && move.y == king.y) == true)
            {
                check = true;
                Debug.WriteLine(check);
            }
        }

        private CMatrixBoard StopCheck(CMatrixBoard ChessBoard, CPiece Piece)
        {
            List<CSquare> saveMoves = new List<CSquare>();

            Tuple<int, int> key = Tuple.Create(Piece.x, Piece.y);

            foreach (var square in ChessBoard.validMoves)
            {
                if (ChessBoard.copyMoves.Exists(move => move.x == square.x && move.y == square.y))
                {
                    saveMoves.Add(square);
                }
            }
            ChessBoard.stopCheckWithPiece[key] = saveMoves;

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

        private CMatrixBoard ShortCastle(CMatrixBoard ChessBoard, int x, int y, int Y)
        {
            var tmp = ChessBoard.Board[7, Y];  // copy the rook
            ChessBoard.Board[7, Y] = null;
            ChessBoard.Board[5, Y] = tmp;

            Button RookSquare = GetButtonAtPosition(7, Y);
            RookSquare.Image = null;

            Bitmap RookImage = SetImageToButton(tmp);

            RookSquare = GetButtonAtPosition(5, Y);
            RookSquare.Image = RookImage;

            return ChessBoard;
        }

        private CMatrixBoard LongCastle(CMatrixBoard ChessBoard, int x, int y, int Y)
        {
            var tmp = ChessBoard.Board[7, Y];  // copy the rook
            ChessBoard.Board[0, Y] = null;
            ChessBoard.Board[3, Y] = tmp;

            Button RookSquare = GetButtonAtPosition(0, Y);
            RookSquare.Image = null;

            Bitmap RookImage = SetImageToButton(tmp);

            RookSquare = GetButtonAtPosition(3, Y);
            RookSquare.Image = RookImage;

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

                //ChessBoard.Board[x, y] = new CPiece(x, y, promotion.pieceName, selectedPiece.pieceType);

                this.selectedPiece = new CPiece(x, y, promotion.pieceName, selectedPiece.pieceType);

                Debug.WriteLine("Promotion: " + promotion.pieceName);

            }
        }


        private CMatrixBoard AvaibleSquares(CMatrixBoard ChessBoard, CPiece P)
        {
            ChessBoard.validMoves.Clear(); // non so se sia giusto

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