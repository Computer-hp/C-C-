using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

class CharMatrix
{
    private int matrixSize, waitTime = 500;

    private bool gameIsRunning = false, isDeadLockOccured = false;

    private object lockObject = new object();

    private Character[,] matrix;

    private Character Winner = null;

    private HashSet<Character> chosenCharacters = new HashSet<Character>();

    private ManualResetEvent stopEvent = new ManualResetEvent(false);

    private Random random;

    public CharMatrix(int size)
    {
        matrixSize = size;

        matrix = new Character[matrixSize, matrixSize];
        random = new Random();

        gameIsRunning = true;
        StartGame();
    }

    private void StartGame()
    {
        FillWithRandomCharacters();

        Thread printThread = new Thread(Printing), checkForWinnerThread = new Thread(CheckForWinner);

        for (int i = 0; i < matrixSize; i++)
        {
            Thread thread = new Thread(MoveRandomCharacter);
            thread.Name = "Thread " + (i + 1);
            thread.Start();
        }

        printThread.Start();
        checkForWinnerThread.Start();

        stopEvent.WaitOne();
    }

    private void Printing()
    {
        while (gameIsRunning)
        {
            PrintMatrix();
            Thread.Sleep(50);
        }
    }

    private void CheckForWinner()
    {
        while (gameIsRunning)
        {
            if (CheckWinner() <= 1)
            {
                gameIsRunning = false;
                break;
            }
            Thread.Sleep(waitTime);
        }
        stopEvent.Set();
    }

    private void MoveRandomCharacter()
    {
        Thread.Sleep(100);

        while (gameIsRunning)
        {
            int x, y;

            x = random.Next(0, matrixSize);
            y = random.Next(0, matrixSize);

            Monitor.Enter(lockObject);

            try
            {
                if (matrix[x, y] is SquareNull)
                    continue;

                Debug.WriteLine($"{Thread.CurrentThread.Name} character => {x}, {y}");

                if (chosenCharacters.Contains(matrix[x, y]))
                {
                    isDeadLockOccured = false;

                    Thread controlDeadLockThread = new Thread(HandleDeadLock);

                    controlDeadLockThread.Start();
                   
                    Monitor.Wait(lockObject);
                    
                    if (isDeadLockOccured)
                    {
                        Debug.WriteLine(Thread.CurrentThread.Name + " skipped the loop");
                        continue;
                    }
                    
                    Debug.WriteLine($"Passed {Thread.CurrentThread.Name}");
                }

                chosenCharacters.Add(matrix[x, y]);
                MoveCharacter(x, y);

                if (chosenCharacters.Count() > 0)
                    chosenCharacters.Remove(matrix[x, y]);

                Monitor.Pulse(lockObject);
            }
            finally
            {
                Monitor.Exit(lockObject);
            }

            Thread.Sleep(waitTime);
        }
    }

    private void HandleDeadLock()
    {
        Thread.Sleep(3);

        Monitor.Enter(lockObject);

        if (!isDeadLockOccured)
        {
            Monitor.PulseAll(lockObject);
            isDeadLockOccured = true;
        }

        Monitor.Exit(lockObject);
    }

    private void MoveToCell(int previousX, int previousY, int destX, int destY)
    {
        Thread.Sleep(8);
    
        if (matrix[previousX, previousY].isSquareOccupied)
            return;

        Character character = matrix[previousX, previousY];

        if (matrix[destX, destY] is SquareNull)
        {
            SquareNull squareNull = (SquareNull)matrix[destX, destY];
            matrix[previousX, previousY] = squareNull;
        }
        else
            matrix[previousX, previousY] = new SquareNull();


        matrix[destX, destY] = character;
        matrix[destX, destY].Life--;
        matrix[previousX, previousY].isSquareOccupied = false;
        
    }

    private void LockSquare(int previousX, int previousY, int destX, int destY)
    {
        Monitor.Enter(lockObject);

        try
        {
            if (matrix[destX, destY].isSquareOccupied)
            {
                Monitor.Wait(lockObject);
            }

            matrix[destX, destY].isSquareOccupied = true;
            MoveToCell(previousX, previousY, destX, destY);

            Monitor.Pulse(lockObject);
        }
        finally
        {
            Monitor.Exit(lockObject);
        }

        Thread.Sleep(5);
    }

    private void MoveCharacter(int x, int y)
    {
        if (matrix[x, y] is SquareNull)
        {
            Debug.WriteLine("There is a SquareNull");
            return;
        }

        CalculateValidMoves(x, y);
        RemoveInvalidMoves(x, y);

        if (matrix[x, y].ValidMoves.Count < 1)
            return;

        int previousX = x, previousY = y;

        Tuple<int, int> destination = RandomMove(x, y);

        Debug.WriteLine($"{Thread.CurrentThread.Name} => destination = {destination.Item1}, {destination.Item2}");

        LockSquare(previousX, previousY, destination.Item1, destination.Item2);
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
        /*Monitor.Enter(lockObject);

        try
        {
            for (int i = 0; i < matrixSize; i++)
            {
                Console.SetCursorPosition(5, 5 + i);

                for (int j = 0; j < matrixSize; j++)
                {
                    Console.Write(matrix[i, j].Value + " ");
                }
            }
        }
        finally
        {
            Monitor.Exit(lockObject);
        }*/

        for (int i = 0; i < matrixSize; i++)
        {
            Console.SetCursorPosition(5, 5 + i);

            for (int j = 0; j < matrixSize; j++)
            {
                Console.Write(matrix[i, j].Value + " ");
            }
        }
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
    }

    private bool IsValidCell(int row, int col)
    {
        return row >= 0 && row < matrixSize && col >= 0 && col < matrixSize;
    }
}

public class SquareNull : Character
{
    public SquareNull() : base()
    {
        isSquareOccupied = false;
    }
}

public class Character
{
    public char Value { get; set; }
    public List<Tuple<int, int>> ValidMoves { get; set; }
    public bool isSquareOccupied { get; set; }
    public int Life { get; set; } = 5;

    public Character(char value)
    {
        Value = value;
        ValidMoves = new List<Tuple<int, int>>();
        isSquareOccupied = false;
    }

    public Character() { Value = '*'; }
}

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        CharMatrix charMatrix = new CharMatrix(10);
    }
}