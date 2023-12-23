/*using System;
using System.Collections.Generic;

class SquareNull : Character
{
    public bool isSquareOccupied { get; set; }

    public SquareNull()
    {
        isSquareOccupied = false;
    }
}

class Character
{
    public char Value { get; set; }
    public List<Tuple<int, int>> ValidMoves { get; set; }

    public int Life {get; set;} = 5;

    public Character(char value)
    {
        Value = value;
        ValidMoves = new List<Tuple<int, int>>();
    }
    
    public Character() { Value = '*'; }
}

class CharMatrix
{
    private Character[,] matrix;
    private Random random;
    private int matrixSize;

    private Character Winner = null;

    public CharMatrix(int size)
    {
        matrixSize = size;

        matrix = new Character[matrixSize, matrixSize];
        random = new Random();

        StartGame();
    }

    private void StartGame()
    {
        FillWithRandomCharacters();

        while (true)
        {
            PrintMatrix();

            int x = random.Next(0, matrixSize);
            int y = random.Next(0, matrixSize);

            MoveCharacter(x, y);

            if (CheckWinner() <= 1)
                break;
        }

        PrintMatrix();
    }

    private void MoveCharacter(int x, int y)
    {
        if (matrix[x, y] is SquareNull)
            return;

        CalculateValidMoves(x, y);
        RemoveInvalidMoves(x, y);

        if (matrix[x, y].ValidMoves.Count < 1)
            return;

        int previousX = x, previousY = y;

        Tuple<int, int> destination = RandomMove(x, y);

        Character character = matrix[x, y];

        if (!(matrix[destination.Item1, destination.Item2] is SquareNull))
        {
            matrix[destination.Item1, destination.Item2] = character;
            matrix[previousX, previousY] = new SquareNull();

            Thread.Sleep(1000);
            return;
        }

        SquareNull squareNull = (SquareNull)matrix[destination.Item1, destination.Item2];

        character.Life--;

        matrix[destination.Item1, destination.Item2] = character;
        matrix[previousX, previousY] = squareNull;

        Thread.Sleep(1500);

        if (character.Life <= 0)
        {
            matrix[destination.Item1, destination.Item2].Value = (char)(matrix[destination.Item1, destination.Item2].Value - 32);
            PrintMatrix();
            Thread.Sleep(1500);
            matrix[destination.Item1, destination.Item2] = new SquareNull();
        }
    }

    private Tuple<int, int> RandomMove(int x, int y)
    {
        int validMovesLength = matrix[x, y].ValidMoves.Count();

        return matrix[x, y].ValidMoves[random.Next(0, validMovesLength)];
    }


    private int CheckWinner()
    {
        int counter = 0;

        Character possibleWinner = null;

        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                if (!(matrix[i, j] is SquareNull))
                {
                    possibleWinner = matrix[i, j];
                    counter++;
                }

                if (counter > 1)
                    return counter;
            }
        }

        if (possibleWinner == null)
            return 0;

        Winner = possibleWinner;

        return 1;
    }

    public void FillWithRandomCharacters()
    {
        int i, j;

        for (i = 0; i < matrix.GetLength(0); i++)
        {
            for (j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = new Character(GetRandomChar());
            }
        }

        i = random.Next(0, matrixSize);
        j = random.Next(0, matrixSize);

        matrix[i, j] = new SquareNull();
    }
    private char GetRandomChar()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return chars[random.Next(chars.Length)];
    }

    public void PrintMatrix()
    {
        Console.SetCursorPosition(0, 5);
        //Console.WriteLine();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j].Value + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void CalculateValidMoves(int row, int col)
    {
        ref Character character = ref matrix[row, col];

        character.ValidMoves.Clear();

        character.ValidMoves.Add(Tuple.Create(row - 1, col));
        character.ValidMoves.Add(Tuple.Create(row + 1, col));
        character.ValidMoves.Add(Tuple.Create(row, col - 1));
        character.ValidMoves.Add(Tuple.Create(row, col + 1));
    }

    public void RemoveInvalidMoves(int row, int col)
    {
        Character character = matrix[row, col];

        character.ValidMoves.RemoveAll(move => !IsValidCell(move.Item1, move.Item2));
        //character.ValidMoves.RemoveAll(move => !CanEat(character, matrix[move.Item1, move.Item2]));
    }

    private bool IsValidCell(int row, int col)
    {
        return row >= 0 && row < matrixSize && col >= 0 && col < matrixSize;
    }

    private bool CanEat(Character source, Character target)
    {
        return !(target is SquareNull) && source.Value - target.Value == 1;
    }
}

class Program
{
    static void Main()
    {
        CharMatrix charMatrix = new CharMatrix(3);
    }
}
*/