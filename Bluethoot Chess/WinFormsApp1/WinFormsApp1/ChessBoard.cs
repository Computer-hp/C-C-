using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class CMatrixBoard
    {
        private const int boardSize = 8;

        private CPiece[,] mBoard;

        public CPiece[,] Board { get { return mBoard; } set { mBoard = value; } }

        public List<CSquare> validMoves = new List<CSquare>();

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
                // white pawns & pieces
                this.Board[x, 1] = new CPiece(x, 1, "P", "White");
                this.Board[x, 0] = new CPiece(x, 0, pieces[x], "White");

                // black pawns & pieces
                this.Board[x, 6] = new CPiece(x, 6, "P", "Black");
                this.Board[x, 7] = new CPiece(x, 7, pieces[x], "Black");
            }
        }

        public void Pawns(CPiece P)
        {
            //Debug.WriteLine(P.pieceType + " " + P.pieceName);

            if (P.pieceType == "White" && P.y < boardSize) 
            {
                if (this.Board[P.x, P.y + 1] == null)
                    validMoves.Add(new CSquare(P.x, P.y + 1));

                checkRigth(1, 1, P, this);

                checkLeft(-1, 1, P, this);

                if (P.y == 1)
                    validMoves.Add(new CSquare(P.x, P.y + 2));
            }
            else if (P.pieceType == "Black" && P.y >= 0)
            {
                if (this.Board[P.x, P.y - 1] == null)
                    validMoves.Add(new CSquare(P.x, P.y - 1));

                checkRigth(1, -1, P, this);

                checkLeft(-1, -1, P, this);

                if (P.y == 6)
                    validMoves.Add(new CSquare(P.x, P.y - 2));
            }
        }
        // for white and black pawns
        private void checkRigth(int X, int Y, CPiece P, CMatrixBoard B)
        {
            int rightX = P.x + X, upORdown = P.y + Y;

            if ((rightX >= boardSize) || (upORdown >= boardSize || upORdown < 0))
                return;

            if (B.Board[rightX, upORdown] != null && B.Board[rightX, upORdown].pieceType != P.pieceType)
                validMoves.Add(new CSquare(rightX, upORdown));
        }
        // for white and black pawns
        private void checkLeft(int X, int Y, CPiece P, CMatrixBoard B)
        {
            int leftX = P.x + X, upORdown = P.y + Y;

            if (leftX < 0 || (upORdown >= boardSize || upORdown < 0))
                return;

            if (B.Board[leftX, upORdown] != null && B.Board[leftX, upORdown].pieceType != P.pieceType)
                validMoves.Add(new CSquare(leftX, upORdown));
        }

        public void Straight(CPiece P)
        {
            findStraight(this, P, 8);
        }
        private void findStraight(CMatrixBoard B, CPiece P, int times)
        {
            bool right = true, left = true, up = true, down = true;

            int x = P.x;
            int y = P.y;
            int counter = 2;

            for (int i = 0; i < times; i++)
            {
                x++;
                // opposite side
                int differenceX = x - counter;

                // right
                if (x < boardSize && right)
                {
                    if (B.Board[x, P.y] == null)
                        validMoves.Add(new CSquare(x, P.y));
                    else if (B.Board[x, P.y] != null && B.Board[x, P.y].pieceType != P.pieceType)
                    {
                        validMoves.Add(new CSquare(x, P.y));
                        right = false;
                    }
                    else
                        right = false;
                }

                // left
                if (differenceX >= 0 && left)
                {
                    if (B.Board[differenceX, P.y] == null)
                        validMoves.Add(new CSquare(differenceX, P.y));
                    else if (B.Board[differenceX, P.y] != null && B.Board[differenceX, P.y].pieceType != P.pieceType)
                    {
                        validMoves.Add(new CSquare(differenceX, P.y));
                        left = false;
                    }
                    else
                        left = false;
                }

                y++;
                // opposite side
                int differenceY = y - counter;

                // up
                if (y < boardSize && up)
                {
                    if (B.Board[P.x, y] == null)
                        validMoves.Add(new CSquare(P.x, y));
                    else if (B.Board[P.x, y] != null && B.Board[P.x, y].pieceType != P.pieceType)
                    {
                        validMoves.Add(new CSquare(P.x, y));
                        up = false;
                    }
                    else
                        up = false;
                }

                // down
                if (differenceY >= 0 && down)
                {
                    if (B.Board[P.x, differenceY] == null)
                        validMoves.Add(new CSquare(P.x, differenceY));
                    else if (B.Board[P.x, differenceY] != null && B.Board[P.x, differenceY].pieceType != P.pieceType)
                    {
                        validMoves.Add(new CSquare(P.x, differenceY));
                        down = false;
                    }
                    else
                        down = false;
                }

                counter += 2;
            }
        }

        public void Diagonal(CPiece P)
        {
            findDiagonal(this, P, 8);

        }
        private void findDiagonal(CMatrixBoard B, CPiece P, int times)
        {
            bool rightUp = true, rightDown = true, leftUp = true, leftDown = true;

            int x = P.x;
            int y = P.y;
            int counter = 2;

            for (int i = 0; i < times; i++)
            {
                x++;
                y++;
                // opposite side
                int differenceX = x - counter, differenceY = y - counter;
                // right
                if (x < boardSize)
                {
                    if (y < boardSize && rightUp)
                        if (B.Board[x, y] == null)
                            validMoves.Add(new CSquare(x, y));
                        else if (B.Board[x, y] != null && B.Board[x, y].pieceType != P.pieceType)
                        {
                            validMoves.Add(new CSquare(x, y));
                            rightUp = false;
                        }
                        else
                            rightUp = false;


                    if (differenceY >= 0 && rightDown)
                        if (B.Board[x, differenceY] == null)
                            validMoves.Add(new CSquare(x, differenceY));
                        else if (B.Board[x, differenceY] != null && B.Board[x, differenceY].pieceType != P.pieceType)
                        {
                            validMoves.Add(new CSquare(x, differenceY));
                            rightDown = false;
                        }
                        else
                            rightDown = false;
                }
                // left
                if (differenceX >= 0)
                {
                    if (y < boardSize && leftUp)
                        if (B.Board[differenceX, y] == null)
                            validMoves.Add(new CSquare(differenceX, y));
                        else if (B.Board[differenceX, y] != null && B.Board[differenceX, y].pieceType != P.pieceType)
                        {
                            validMoves.Add(new CSquare(differenceX, y));
                            leftUp = false;
                        }
                        else
                            leftUp = false;


                    if (differenceY >= 0 && leftDown)
                        if (differenceY < boardSize && leftDown)
                            if (B.Board[differenceX, differenceY] == null)
                                validMoves.Add(new CSquare(differenceX, differenceY));
                            else if (B.Board[differenceX, differenceY] != null && B.Board[differenceX, differenceY].pieceType != P.pieceType)
                            {
                                validMoves.Add(new CSquare(differenceX, differenceY));
                                leftDown = false;
                            }
                            else
                                leftDown = false;
                }
                counter += 2;
            }
        }

        public void Jump(CPiece P)
        {
            
            int x = P.x;
            int y = P.y;

            int counterX = 2;
            int counterY = 4;

            for (int i = 0; i < 2; i++)
            {
                int provisoryX = x + 1;
                int provisoryY = y + 2;
                int differenceX = provisoryX - counterX, differenceY = provisoryY - counterY;

                if (provisoryX < boardSize)
                {
                    if (provisoryY < boardSize)
                        validMoves.Add(new CSquare(provisoryX, provisoryY));
                    if (differenceY >= 0)
                        validMoves.Add(new CSquare(provisoryX, differenceY));
                }
                if (differenceX >= 0)
                {
                    if (provisoryY < boardSize)
                        validMoves.Add(new CSquare(differenceX, provisoryY));
                    if (differenceY >= 0)
                        validMoves.Add(new CSquare(differenceX, differenceY));

                }

                x++;
                y--;
                counterY -= 2;
                counterX += 2;
            }
        }

        public void King(CPiece P)
        {
            findStraight(this, P, 1);
            findDiagonal(this, P, 1);
        }

        public void checkMoves(CPiece P)
        {
                for (int x = 0; x < boardSize; x++)
                {
                    //CSquare S;
                    for (int y = 0; y < boardSize; y++)
                    {
                        //S = new CSquare(x, y);

                        if (this.Board[x, y] != null && this.Board[x, y].pieceName != P.pieceName)
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

    class CSquare
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
