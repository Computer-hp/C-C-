using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinFormsApp1
{
    public class CMatrixBoard
    {
        private const int boardSize = 8;

        private CPiece[,] mBoard;

        public CPiece[,] Board { get { return mBoard; } set { mBoard = value; } }

        public List<CSquare> validMoves = new List<CSquare>();

        public List<CSquare> copyMoves = new List<CSquare>();

        public List<CSquare> invalidSquaresKing = new List<CSquare>();

        public Dictionary<Tuple<int, int>, List<CSquare>> stopCheckWithPiece = new Dictionary<Tuple<int, int>, List<CSquare>>();

        private MethodInfo method = null;

        private int x, y, differenceX, differenceY;

        private bool right, left, up, down;

        private bool rightUp, leftUp, rightDown, leftDown;

        public CMatrixBoard()
        {
            this.Board = new CPiece[boardSize, boardSize];
        }

        // add pieces
        public void InitializePieces()
        {
            string[] pieces = { "R", "N", "B", "Q", "K", "B", "N", "R" };

            for (int x = 0; x < 8; x++)
            {
                this.Board[x, 1] = new CPiece(x, 1, "P", "White");
                this.Board[x, 0] = new CPiece(x, 0, pieces[x], "White");

                this.Board[x, 6] = new CPiece(x, 6, "P", "Black");
                this.Board[x, 7] = new CPiece(x, 7, pieces[x], "Black");
            }
        }

        public void Pawns(CPiece P)
        {
            if (P.pieceType == "White" && P.y < boardSize) 
            {
                if (this.Board[P.x, P.y + 1] == null)
                    validMoves.Add(new CSquare(P.x, P.y + 1));

                checkRigth(1, 1, P, this);

                checkLeft(-1, 1, P, this);

                if (P.y == 1 && this.Board[P.x, P.y + 2] == null)
                    validMoves.Add(new CSquare(P.x, P.y + 2));
            }
            if (P.pieceType == "Black" && P.y >= 0)
            {
                if (this.Board[P.x, P.y - 1] == null)
                    validMoves.Add(new CSquare(P.x, P.y - 1));

                checkRigth(1, -1, P, this);

                checkLeft(-1, -1, P, this);

                if (P.y == 6 && this.Board[P.x, P.y - 2] == null)
                    validMoves.Add(new CSquare(P.x, P.y - 2));
            }
        }

        // for white and black pawns
        private void checkRigth(int X, int Y, CPiece P, CMatrixBoard B)
        {
            int rightX = P.x + X, upORdown = P.y + Y;

            if (rightX >= boardSize || (upORdown >= boardSize || upORdown < 0))
                return;

            validMoves.Add(new CSquare(rightX, upORdown));
        }


        // for white and black pawns
        private void checkLeft(int X, int Y, CPiece P, CMatrixBoard B)
        {
            int leftX = P.x + X, upORdown = P.y + Y;

            if (leftX < 0 || (upORdown >= boardSize || upORdown < 0))
                return;

            validMoves.Add(new CSquare(leftX, upORdown));
        }

        public void Straight(CPiece P, int times, string direction)
        {
            x = P.x; y = P.y;

            int counter = 2;

            right = true; left = true; up = true; down = true;

            for (int i = 0; i < times; i++)
            {
                x++; y++;

                // opposite side           // opposite side
                differenceX = x - counter; differenceY = y - counter;

                if (direction != "")
                {
                    method = typeof(CMatrixBoard).GetMethod(direction);
                    object[] parameters = new object[] { this, P };
                    method.Invoke(this, parameters);
                }
                else
                {
                    Up(this, P);
                    Down(this, P);
                    Right(this, P);
                    Left(this, P);
                }

                counter += 2;
            }
        }

        public void Up(CMatrixBoard B, CPiece P)
        {
            if (y < boardSize && up)
            {
                if (B.Board[P.x, y] == null) {
                    validMoves.Add(new CSquare(P.x, y));
                    return;
                }
                if (B.Board[P.x, y].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(P.x, y));
                }
                
                up = false;
            }
        }
        public void Down(CMatrixBoard B, CPiece P)
        {
            if (differenceY >= 0 && down)
            {
                if (B.Board[P.x, differenceY] == null)
                {
                    validMoves.Add(new CSquare(P.x, differenceY));
                    return;
                }
                if (B.Board[P.x, differenceY].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(P.x, differenceY));
                }

                down = false;
            }

        }
        public void Left(CMatrixBoard B, CPiece P)
        {
            if (differenceX >= 0 && left)
            {
                if (B.Board[differenceX, P.y] == null)
                {
                    validMoves.Add(new CSquare(differenceX, P.y));
                    return;
                }
                if (B.Board[differenceX, P.y].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(differenceX, P.y));
                }
                
                left = false;
            }
        }
        public void Right(CMatrixBoard B, CPiece P)
        {
            if (x < boardSize && right)
            {
                if (B.Board[x, P.y] == null)
                {
                    validMoves.Add(new CSquare(x, P.y));
                    return;
                }
                if (B.Board[x, P.y].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(x, P.y));
                }
                
                right = false;
            }
        }

        public void Diagonal(CPiece P, int times, string direction)
        {
            x = P.x; y = P.y;

            int counter = 2;

            rightUp = true; leftUp = true; rightDown = true; leftDown = true;

            for (int i = 0; i < times; i++)
            {
                x++; y++;
                // opposite side           opposite side
                differenceX = x - counter; differenceY = y - counter;

                if (direction != "")
                {
                    method = typeof(CMatrixBoard).GetMethod(direction);
                    object[] parameters = new object[] { this, P };
                    method.Invoke(this, parameters);
                }
                else
                {
                    if (x < boardSize)
                    {
                        RightUp(this, P);
                        RightDown(this, P);
                    }
                    if (differenceX >= 0)
                    {
                        LeftUp(this, P);
                        LeftDown(this, P);
                    }
                }
                counter += 2;
            }

        }
        public void RightUp(CMatrixBoard B, CPiece P)
        {
            if (x < boardSize && y < boardSize && rightUp)
            {
                if (B.Board[x, y] == null)
                {
                    validMoves.Add(new CSquare(x, y));
                    return;
                }

                if (B.Board[x, y] != null && B.Board[x, y].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(x, y));
                }

                rightUp = false;
            }
        }
        public void RightDown(CMatrixBoard B, CPiece P)
        {
            if (x < boardSize && differenceY >= 0 && rightDown)
            {
                if (B.Board[x, differenceY] == null)
                {
                    validMoves.Add(new CSquare(x, differenceY));
                    return;
                }
                if (B.Board[x, differenceY] != null && B.Board[x, differenceY].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(x, differenceY));
                }

                rightDown = false;
            }
        }
        public void LeftUp(CMatrixBoard B, CPiece P)
        {
            if (differenceX >= 0 && y < boardSize && leftUp)
            {
                if (B.Board[differenceX, y] == null)
                {
                    validMoves.Add(new CSquare(differenceX, y));
                    return;
                }
                if (B.Board[differenceX, y] != null && B.Board[differenceX, y].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(differenceX, y));
                }

                leftUp = false;
            }
        }
        public void LeftDown(CMatrixBoard B, CPiece P)
        {
            if (differenceX >= 0 && differenceY >= 0 && differenceY < boardSize && leftDown)
            {
                if (B.Board[differenceX, differenceY] == null)
                {
                    validMoves.Add(new CSquare(differenceX, differenceY));
                    return;
                }

                if (B.Board[differenceX, differenceY] != null && B.Board[differenceX, differenceY].pieceType != P.pieceType)
                {
                    validMoves.Add(new CSquare(differenceX, differenceY));
                }
                    
                leftDown = false;
            }
        }

        public void Jump(CPiece P)
        {
            x = P.x; y = P.y;

            int counterX = 2;
            int counterY = 4;

            for (int i = 0; i < 2; i++)
            {
                int provisoryX = x + 1;
                int provisoryY = y + 2;

                differenceX = provisoryX - counterX; 
                differenceY = provisoryY - counterY;

                JumpRight(provisoryX, provisoryY);
                JumpLeft(provisoryY);


                x++;
                y--;
                counterY -= 2;
                counterX += 2;
            }
        }
        public void JumpRight(int provisoryX, int provisoryY)
        {
            if (provisoryX < boardSize)
            {
                if (provisoryY < boardSize)
                    validMoves.Add(new CSquare(provisoryX, provisoryY));
                if (differenceY >= 0)
                    validMoves.Add(new CSquare(provisoryX, differenceY));
            }
        }
        public void JumpLeft(int provisoryY)
        {
            if (differenceX >= 0)
            {
                if (provisoryY < boardSize)
                    validMoves.Add(new CSquare(differenceX, provisoryY));
                if (differenceY >= 0)
                    validMoves.Add(new CSquare(differenceX, differenceY));

            }
        }
        public void checkJump(CPiece P)
        {
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {

                    if (this.Board[x, y] != null && this.Board[x, y].pieceType == P.pieceType)
                        if (this.validMoves.Exists(item => item.x == x && item.y == y) == true)
                            this.validMoves.RemoveAll(item => item.x == x && item.y == y);
                }
            }
        }

        public override string ToString()
        {
            string output = "";
            foreach (var element in this.validMoves)
            {
                output += element.x + "," + element.y + " ";
            }
            return output;
        }
    }

    public class CSquare
    {
        public int x { get; set; }
        public int y { get; set; }

        public CSquare(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
