using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MainMenu : Form
    {
        private ChessBoardForm ChessBoardForm;

        public MainMenu()
        {
            InitializeComponent();
            InitializeChessBoard();
        }
        private void InitializeChessBoard()
        {
            this.BackColor = Color.RosyBrown;
            this.ForeColor = Color.White;
            this.Font = new Font("Arial", 12, FontStyle.Bold);

            int buttonWidth = 100;
            int buttonHeight = 60;
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            Button buttonNewGame = new Button()
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

            Button buttonExit = new Button
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

            Button buttonConnect = new Button
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



            this.Controls.Add(buttonNewGame);
            this.Controls.Add(buttonExit);
            this.Controls.Add(buttonConnect);
        }

        private void Create_ChessBoard(object sender, EventArgs e)
        {
            ChessBoardForm = new ChessBoardForm();
            ChessBoardForm.Show();
            this.Hide();
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
