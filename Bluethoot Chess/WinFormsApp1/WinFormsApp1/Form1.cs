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
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics.Metrics;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const int boardSize = 8;
        public const int squareSize = 70;

        private int turn = 0; // 0 for white, 1 for black
        private int UpOrDown;

        private int[] secondsElapsed = new int[2];

        private string currentPlayer = "";
        private string direction = "";

        private string[] rowLetter = { "a", "b", "c", "d", "e", "f", "g", "h" };

        public static string projectPath = (System.Environment.CurrentDirectory).Replace("WinFormsApp1\\bin\\Debug\\net6.0-windows", "");

        private bool firstMove = false;

        private bool[] firstKingMove = { false, false };

        private bool[] aRookFirstMove = { false, false }, hRookFirstMove = { false, false };

        private bool[] O_O = { false, false }, O_O_O = { false, false };

        private bool check = false;

        private CMatrixBoard ChessBoard;

        private CPiece selectedPiece = null;

        private MethodInfo method = null;

        private Label[] timerLabel = new Label[2];

        private Timer[] timer = new Timer[2];


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
            int centerX = (ClientSize.Width - boardSize * squareSize) / 2 - 70;
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

            timerLabel[0] = new Label
            {
                Text = "White: 00:00",
                Font = new Font("Arial", 18),
                Location = new Point(centerX + boardSize * squareSize + 10, ClientSize.Height - squareSize/2 - 10),
                AutoSize = true
            };

            Controls.Add(timerLabel[0]);

            timerLabel[1] = new Label
            {
                Text = "Black: 00:00",
                Font = new Font("Arial", 18),
                Location = new Point(centerX + boardSize * squareSize + 10, centerY),
                AutoSize = true
            };

            Controls.Add(timerLabel[1]);

            // Initialize the timer
            timer[0] = new Timer { Interval = 1000 };

            timer[1] = new Timer { Interval = 1000 };

            timer[0].Tick += Timer_Tick;
            timer[1].Tick += Timer_Tick;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed[turn]++;

            TimeSpan time = TimeSpan.FromSeconds(secondsElapsed[turn]);

            string player = (turn == 0) ? "White: " : "Black: ";

            string timerText = string.Format(player + "{0:D2}:{1:D2}", time.Minutes, time.Seconds);
            timerLabel[turn].Text = timerText;
        }

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

            UpOrDown = (currentPlayer == "White") ? 1 : -1;

            if (ChessBoard.Board[x, y] == null || ChessBoard.Board[x, y].pieceType != currentPlayer)
            {
                if (!ChessBoard.validMoves.Exists(item => item.x == x && item.y == y))
                    return;

                if (selectedPiece == null)
                    return;

                int previous_piece_x = selectedPiece.x, previous_piece_y = selectedPiece.y;

                // check handle
                if (check && !ChessBoard.validMoves.Any())
                {
                    ChessBoard.copyMoves.Clear();

                    Tuple<int, int> key = Tuple.Create(selectedPiece.x, selectedPiece.y);
                    ChessBoard.copyMoves.Clear();

                    if (selectedPiece.pieceName != "K" && ChessBoard.stopCheckWithPiece.ContainsKey(key))
                    {
                        List<CSquare> squareList = ChessBoard.stopCheckWithPiece[key];

                        if (squareList.Any(square => square.x == x && square.y == y))
                            ChessBoard.copyMoves.AddRange(ChessBoard.stopCheckWithPiece[key]);
                    }

                    if (!ChessBoard.copyMoves.Exists(square => ChessBoard.validMoves.Exists(checkSquare => checkSquare.x == square.x && checkSquare.y == square.y)))
                        return;

                    // Clear the list associated with each Tuple

                    foreach (var entry in ChessBoard.stopCheckWithPiece)
                        entry.Value.Clear();

                    ChessBoard.stopCheckWithPiece.Clear(); // Clear the entire dictionary
                }

                if (selectedPiece.pieceName == "P")
                    PawnPromotion(ChessBoard, selectedPiece, x, y);


                Button originalSquare = GetButtonAtPosition(previous_piece_x, previous_piece_y);
                originalSquare.BackgroundImage = null;


                ChessBoard.Board[previous_piece_x, previous_piece_y] = null;

                //ChessBoard.Board[x, y] = selectedPiece; // this becomes a reference. I'm not sure of using it

                ChessBoard.Board[x, y] = new CPiece(x, y, selectedPiece.pieceName, selectedPiece.pieceType);


                if (selectedPiece.pieceName == "K" && !firstKingMove[turn])
                    ChessBoard = FirstKingMove(ChessBoard, x, y, Y);

                if (selectedPiece.pieceName == "R")
                    FirstRookMove(selectedPiece, x, y, Y);

                ChessBoard.Board[x, y].x = x;
                ChessBoard.Board[x, y].y = y;

                if (!firstMove)
                {
                    firstMove = true;
                    timer[1].Start();
                }
                else
                {
                    timer[turn + UpOrDown].Stop();
                    timer[turn].Start();
                }

                CPiece king = FindKing(ChessBoard, currentPlayer);

                // finds only the moves of the piece that gives check which are after used in StopCheck()
                if (selectedPiece.pieceName != "K")
                {
                    ChessBoard.validMoves.Clear();

                    direction = "";

                    if (selectedPiece.pieceName == "R")
                        DefineMethod("Straight", king, x, y);

                    if (selectedPiece.pieceName == "B")
                        DefineMethod("Diagonal", king, x, y);

                    if (selectedPiece.pieceName == "Q")
                        DefineMethod("Straight", king, x, y);

                    // because the piece that gives check can also be captured to stop check, neccessary for Knight and Pawn
                    ChessBoard.validMoves.Add(new CSquare(x, y));

                    IsCheck(ChessBoard, ChessBoard.Board[x, y], king);
                }

                Bitmap image = SetImageToButton(ChessBoard.Board[x, y]);

                clickedButton.BackgroundImage = image;

                if (check)
                {
                    Debug.WriteLine("Check");

                    ChessBoard.copyMoves.Clear();
                    ChessBoard.copyMoves.AddRange(ChessBoard.validMoves);

                    foreach (var piece in ChessBoard.Board)
                    {
                        if (piece != null && piece.pieceType != selectedPiece.pieceType && piece.pieceName != "K")
                        {
                            ChessBoard = AvaibleSquares(ChessBoard, piece);

                            if (piece.pieceName == "P")
                                DiagonalMovementPawn(ChessBoard, piece, piece.x, piece.y - UpOrDown);

                            ChessBoard = StopCheck(ChessBoard, piece);
                        }
                    }

                    // check valid moves for King
                    ChessBoard = AvaibleSquares(ChessBoard, king);
                    ChessBoard = SaveInvalidSquares(ChessBoard, king);

                    int counter = 0;

                    foreach (var key in ChessBoard.stopCheckWithPiece.Keys)
                    {
                        Debug.WriteLine($"Key: {key}");

                        List<CSquare> values = ChessBoard.stopCheckWithPiece[key];

                        Debug.Write("Values: ");

                        foreach (var value in values)
                        {
                            Debug.Write($"{value.x}, {value.y}");
                        }

                        Debug.WriteLine("\n");
                        counter++;
                    }

                    Debug.WriteLine("There are {0} keys", counter);

                    Debug.Write("validMoves = ");

                    foreach (var square in ChessBoard.validMoves)
                    {
                        Debug.WriteLine($"{square.x}, {square.y}");
                    }
                    Debug.Write("\n");

                    // Check mate
                    if (!ChessBoard.validMoves.Any() && !ChessBoard.stopCheckWithPiece.Any(kv => kv.Value != null && kv.Value.Count > 0))
                    {
                        var popUp = new RestartForm();

                        popUp.StartPosition = FormStartPosition.CenterParent;

                        popUp.ShowDialog(this);
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

            if (ChessBoard.Board[x, y].pieceName == "P")
                DiagonalMovementPawn(ChessBoard, selectedPiece, x, y + UpOrDown);


            Debug.WriteLine(selectedPiece.pieceName);
            Debug.WriteLine(ChessBoard.ToString() + "\n");

            if (selectedPiece.pieceName != "K")
                return;

            ChessBoard = SaveInvalidSquares(ChessBoard, selectedPiece);


            // Check if  O-O  or  O-O-O  is possible.
            if (!O_O[turn])
                CheckCastle(6, Y, 5, ref O_O[turn], hRookFirstMove[turn]);

            if (!O_O_O[turn])
                CheckCastle(2, Y, 3, ref O_O_O[turn], aRookFirstMove[turn]);
        }

        private void DefineMethod(string moveTo, CPiece king, int x, int y)
        {
            direction = "";

            ChessBoard.validMoves.Clear();

            if (moveTo == "Straight")
                FindStraightDirection(king, x, y);
            else
                FindDiagonalyDirection(king, x, y);

            if (direction != "")
            {
                method = typeof(CMatrixBoard).GetMethod(moveTo);
                object[] parameters = new object[] { ChessBoard.Board[x, y], 8, direction };
                method.Invoke(ChessBoard, parameters);

                IsCheck(ChessBoard, ChessBoard.Board[selectedPiece.x, selectedPiece.y], king);
            }

            if (selectedPiece.pieceName == "Q" && !check && moveTo != "Diagonal")
                DefineMethod("Diagonal", king, x, y);
        }

        private void CheckCastle(int kingMoveX, int Y, int compareX, ref bool castle, bool firstRookMove)
        {
            if (compareX == 3 && ChessBoard.Board[2, Y] != null)
                return;

            if (!firstKingMove[turn] && !firstRookMove &&
                ChessBoard.Board[kingMoveX, Y] == null && ChessBoard.validMoves.Exists(item => item.x == compareX && item.y == Y))
            {
                ChessBoard.validMoves.Add(new CSquare(kingMoveX, Y));
                castle = true;
                return;
            }
            castle = false;
        }

        private void FindStraightDirection(CPiece king, int x, int y)
        {
            if (x > king.x && y == king.y)
            {
                direction = "Left";
                return;
            }

            if (x < king.x && y == king.y)
            {
                direction = "Right";
                return;
            }

            if (y > king.y && x == king.x)
            {
                direction = "Down";
                return;
            }

            if (y < king.y && x == king.x)
            {
                direction = "Up";
                return;
            }
        }

        private void FindDiagonalyDirection(CPiece king, int x, int y)
        {
            if (x > king.x && y > king.y)
            {
                direction = "LeftDown";
                return;
            }

            if (x < king.x && y > king.y)
            {
                direction = "RightUp";
                return;
            }

            if (y > king.y && x < king.x)
            {
                direction = "RightDown";
                return;
            }

            if (y < king.y && x > king.x)
            {
                direction = "LeftUp";
                return;
            }
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
        private CMatrixBoard SaveInvalidSquares(CMatrixBoard B, CPiece king)
        {
            B.invalidSquaresKing.Clear();

            B.invalidSquaresKing.AddRange(B.validMoves);

            foreach (var piece in B.Board)
            {
                if (piece != null && piece.pieceType != king.pieceType && piece.pieceName != king.pieceName)
                {
                    B = AvaibleSquares(B, piece);

                    if (piece.pieceName == "P")
                    {
                        B.validMoves.RemoveAll(square => square.x == piece.x && square.y == piece.y + UpOrDown);
                    }

                    B.invalidSquaresKing.RemoveAll(square => B.validMoves.Exists(move => move.x == square.x && move.y == square.y));
                }
            }

            B.validMoves.Clear();
            B.validMoves.AddRange(B.invalidSquaresKing);
            return B;
        }

        private void IsCheck(CMatrixBoard B, CPiece P, CPiece king)
        {
            if (B.validMoves.Exists(move => move.x == king.x && move.y == king.y))
                check = true;
            else
                check = false;
        }

        private CMatrixBoard StopCheck(CMatrixBoard ChessBoard, CPiece Piece)
        {
            List<CSquare> tmpMoves = new List<CSquare>();

            Tuple<int, int> key;

            foreach (var square in ChessBoard.validMoves)
            {
                if (ChessBoard.copyMoves.Exists(move => move.x == square.x && move.y == square.y))
                    tmpMoves.Add(square);
            }

            if (tmpMoves.Any())
            {
                key = Tuple.Create(Piece.x, Piece.y);
                ChessBoard.stopCheckWithPiece[key] = tmpMoves;
            }

            return ChessBoard;
        }

        private CMatrixBoard FirstKingMove(CMatrixBoard ChessBoard, int x, int y, int Y)
        {
            firstKingMove[turn] = true;

            if (O_O[turn] && x == 6 && y == Y)
                ChessBoard = ShortAndLongCastle(ChessBoard, 7, Y);

            if (O_O_O[turn] && x == 2 && y == Y)
                ChessBoard = ShortAndLongCastle(ChessBoard, 0, Y);

            return ChessBoard;
        }

        private CMatrixBoard ShortAndLongCastle(CMatrixBoard ChessBoard, int rookX, int Y)
        {
            var tmp = ChessBoard.Board[rookX, Y];  // copy the rook

            FirstRookMove(tmp, rookX, Y, Y);  // the rook has done its first move

            ChessBoard.Board[rookX, Y] = null;

            Button RookSquare = GetButtonAtPosition(rookX, Y);
            RookSquare.BackgroundImage = null;

            //transpose the rook
            rookX = (rookX == 0) ? 3 : 5;

            ChessBoard.Board[rookX, Y] = tmp;

            Bitmap RookImage = SetImageToButton(tmp);

            RookSquare = GetButtonAtPosition(rookX, Y);
            RookSquare.BackgroundImage = RookImage;

            return ChessBoard;
        }

        private void FirstRookMove(CPiece selectedPiece, int x, int y, int Y)
        {
            if (selectedPiece.x == 0 && selectedPiece.y == Y)
                aRookFirstMove[turn] = true;

            if (selectedPiece.x == 7 && selectedPiece.y == Y)
                hRookFirstMove[turn] = true;
        }

        private CPiece FindKing(CMatrixBoard B, string currentPlayer)
        {
            foreach (var piece in B.Board)
            {
                if (piece != null && piece.pieceType != currentPlayer && piece.pieceName == "K")
                    return piece;
            }
            return null;
        }

        private void DiagonalMovementPawn(CMatrixBoard ChessBoard, CPiece piece, int x, int y)
        {
            // 7
            int oppositeX = x - 1;

            if (oppositeX >= 0 && (ChessBoard.Board[oppositeX, y] == null))
                Debug.WriteLine("There is no piece");

            x++;

            if (x < 8 && (ChessBoard.Board[x, y] == null ||
                ChessBoard.Board[x, y].pieceName == "K"))
                ChessBoard.validMoves.RemoveAll(square => square.x == x && square.y == y);

            if (oppositeX >= 0 && (ChessBoard.Board[oppositeX, y] == null || ChessBoard.Board[oppositeX, y].pieceName == "K"))
                ChessBoard.validMoves.RemoveAll(square => square.x == oppositeX && square.y == y);

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