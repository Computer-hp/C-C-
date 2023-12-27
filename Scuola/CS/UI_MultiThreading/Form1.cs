using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace displayMultiThreading
{
    public partial class Form1 : Form
    {
        private const int boardSize = 2, pictureSize = 80;

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private CharacterMover character1;
        private CharacterMover character2;
        private CharacterMover character3;

        private int waitTime = 1000;

        private CharacterMover[,] matrix = new CharacterMover[boardSize, boardSize];

        private readonly object matrixLock = new object();

        public Form1()
        {
            InitializeComponent();
            InitializeMatrix();
            InitializeLabelMatrix();

            Thread thread1 = new Thread(() => MoveCharacter());
            Thread thread2 = new Thread(() => MoveCharacter());
            Thread thread3 = new Thread(() => MoveCharacter());
            Thread thread4 = new Thread(() => MoveCharacter());
            Thread thread5 = new Thread(() => MoveCharacter());
            
            thread1.Name = "T_1";
            thread2.Name = "T_2";
            thread3.Name = "T_3";
            thread4.Name = "T_4";
            thread5.Name = "T_5";

            Thread formThread = new Thread(UpdateLabelMatrix);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
            
            formThread.Start();
        }

        private void InitializeMatrix()
        {
            character1 = new CharacterMover("Harry");
            character2 = new CharacterMover("Potter");
            character3 = new CharacterMover("John");

            matrix[0, 0] = character1;
            matrix[boardSize - 1, boardSize - 1] = character2;
            matrix[0, 1] = character3;
        }

        private void InitializeLabelMatrix()
        {
            int x = 0, y = boardSize - 1;

            int centerX = (ClientSize.Width - boardSize * pictureSize) / 2;
            int centerY = (ClientSize.Height - boardSize * pictureSize) / 2;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Label square = new Label
                    {
                        Size = new Size(pictureSize, pictureSize),
                        Location = new Point(centerX + col * pictureSize, (centerY - 20) + row * pictureSize),
                        BackColor = (row + col) % 2 == 0 ? Color.LimeGreen : Color.AliceBlue,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    square.Tag = (x, y);

                    if (matrix[row, col] != null)
                    {
                        square.Text = matrix[row, col].str;
                    }

                    Controls.Add(square);

                    if (x < boardSize - 1)
                        x++;

                    else
                    {
                        x = 0;
                        y--;
                    }
                }
            }
        }

        public void MoveCharacter()
        {
            Random rnd = new Random();

            while (true)
            {
                int x, y;

                x = rnd.Next(0, boardSize); 
                y = rnd.Next(0, boardSize);

                Debug.WriteLine($"{Thread.CurrentThread.Name} => {x}, {y}");

                Monitor.Enter(matrixLock);

                try
                {
                    if (matrix[x, y] == null)
                        continue;

                    if (matrix[x, y] != null && matrix[x, y].isOccupied)
                    {
                        Monitor.Wait(matrixLock);
                        Debug.WriteLine("Passed");
                    }

                    matrix[x, y].isOccupied = true;

                    Move(x, y);

                    Monitor.Pulse(matrixLock);
                }
                finally
                {
                    Monitor.Exit(matrixLock);
                }

                Thread.Sleep(waitTime);
            }
        }


        private void Move(int x, int y)
        {
            int previous_x = x, previous_y = y;

            int dest_x, dest_y;

            Random rnd = new Random();

            do
            {
                dest_x = rnd.Next(0, boardSize);
                dest_y = rnd.Next(0, boardSize);

            } while (matrix[dest_x, dest_y] != null);

            matrix[dest_x, dest_y] = matrix[previous_x, previous_y];

            matrix[previous_x, previous_y] = null;

            matrix[dest_x, dest_y].isOccupied = false;
        }


        private void UpdateLabelMatrix()
        {
            while (true)
            {
                Monitor.Enter(matrixLock);

                try
                {
                    int x = 0, y = boardSize - 1;

                    foreach (var label in Controls.OfType<Label>())
                    {
                        if (matrix[x, y] == null)
                            SetLabelText(label, "");
                        else
                            SetLabelText(label, matrix[x, y].str);


                        if (x < boardSize - 1)
                            x++;
                        else
                        {
                            x = 0;
                            y--;
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(matrixLock);
                }

                Thread.Sleep(2);
            }
        }

        private void SetLabelText(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke((MethodInvoker)delegate { label.Text = text; });
            }
            else
            {
                label.Text = text;
            }
        }

    }
    public class CharacterMover
    {
        public string str { get; set; }
        public bool isOccupied { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public CharacterMover(string str)
        {
            this.str = str;
            isOccupied = false;
            x = 0;
            y = 0;
        }

    }
}
