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
    public partial class Form2 : Form
    {
        public string pieceName;

        private int buttonWidth = Form1.squareSize, buttonHeight = Form1.squareSize;

        private List<string> promotionPiecesName = new List<string> { "Q", "R", "B", "N" };


        public Form2(int turn)
        {
            InitializeComponent();

            this.BackColor = Color.Black;

            string DIR;

            DIR = (turn == 0) ? "White" : "Black";

            int counter = 0;

            foreach (var buttonName in promotionPiecesName)
            {

                InitializePromotionForm(DIR, buttonName, counter);
                counter += buttonHeight;
            }
        }

        private void InitializePromotionForm(string DIR, string buttonName, int counter)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            Bitmap resizedImage = Form1.SetImageToButton(new CPiece(0, 0, buttonName, DIR));

            Button button = new Button
            {
                Width = buttonWidth,
                Height = buttonHeight,
                Left = (formWidth - buttonWidth) / 2,
                Top = counter,
                Name = buttonName,
                BackColor = Color.Ivory,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 1 },
                BackgroundImage = resizedImage,
                BackgroundImageLayout = ImageLayout.Zoom,
            };

            button.Click += Piece_Promote;

            this.Controls.Add(button);
        }

        private void Piece_Promote(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            pieceName = button.Name;
            this.Close();
        }
    }
}
