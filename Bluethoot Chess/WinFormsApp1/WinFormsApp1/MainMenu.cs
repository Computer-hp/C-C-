using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MainMenu : Form
    {
        private ChessBoardForm chessBoardForm;
        private Thread mainFormThread;

        public MainMenu()
        {
            InitializeComponent();
            InitializeChessBoard();
        }

        private void InitializeChessBoard()
        {
            BackColor = Color.RosyBrown;
            ForeColor = Color.White;
            Font = new Font("Arial", 12, FontStyle.Bold);

            int buttonWidth = 100;
            int buttonHeight = 60;
            int formWidth = ClientSize.Width;
            int formHeight = ClientSize.Height;

            Button buttonNewGame = new()
            {
                Text = "New Game",
                Width = buttonWidth,
                Height = buttonHeight,
                Left = (formWidth - buttonWidth) / 2,
                Top = (formHeight - buttonHeight) / 2 - 70,

                BackColor = Color.PaleGreen,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            Button buttonExit = new()
            {
                Text = "Exit",
                Width = buttonWidth,
                Height = buttonHeight,
                Left = (formWidth - buttonWidth) / 2,
                Top = (formHeight - buttonHeight) / 2 + 70,

                BackColor = Color.PaleGreen,
                ForeColor = Color.Black,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            Button buttonConnect = new()
            {
                Text = "Connect",
                Width = buttonWidth - 20,
                Height = buttonHeight - 20,
                Left = formWidth - 90,
                Top = formHeight - 50,

                BackColor = Color.PaleGreen,
                ForeColor = Color.Black,
                Font = new Font("Arial", 11, FontStyle.Bold)
            };

            buttonNewGame.Click += Create_ChessBoard;
            buttonExit.Click += Button_Exit;

            buttonExit.Click += Button_ConnectBluetooth;



            Controls.Add(buttonNewGame);
            Controls.Add(buttonExit);
            Controls.Add(buttonConnect);
        }

        private void Create_ChessBoard(object sender, EventArgs e)
        {
            chessBoardForm = new ChessBoardForm();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // popUp to choose the color and the time

            this.Invoke(chessBoardForm.Show);
            this.Invoke(new Action(Hide));

            mainFormThread = new Thread(() => HandleChessBoard());
            mainFormThread.Start();
        }

        private void HandleChessBoard()
        {
            while (true)
            {
                if (chessBoardForm.isRestarted)
                    break;

                if (chessBoardForm.isClosed)
                {
                    this.Invoke(new Action(Show));
                    break;
                }

            }

            if (chessBoardForm.isRestarted)
            {
                this.Invoke(new Action(chessBoardForm.Close));
                Create_ChessBoard(null, EventArgs.Empty);
            }
        }

        

        private void Button_ConnectBluetooth(object sender, EventArgs e)
        {

        }

        private void Button_Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}