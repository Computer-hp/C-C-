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
        public Form2(int turn)
        {
            InitializeComponent();

            string DIR;

            if (turn == 0)
                DIR = "White";
            else
                DIR = "Black";

            foreach (var button in new Button[] { Q, R, B, N })
            {
                Bitmap resizedImage = Form1.SetImageToButton(new CPiece(0, 0, button.Name, DIR));

                button.Image = resizedImage;
            }
        }

        private void Piece_Promote(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            pieceName = button.Name;
            this.Close();
        }
    }
}
