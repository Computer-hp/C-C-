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
    public partial class RestartForm : Form
    {
        public RestartForm()
        {
            InitializeComponent();
            InitializeRetartMenu();
        }

        private void InitializeRetartMenu()
        {
            this.BackColor = Color.RosyBrown;
            this.ForeColor = Color.White;
            this.Font = new Font("Arial", 12, FontStyle.Bold);

            int buttonWidth = 100;
            int buttonHeight = 40;
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            Button button1 = new Button();
            button1.Text = "New Game";
            button1.Width = buttonWidth;
            button1.Height = buttonHeight;
            button1.Left = (formWidth - buttonWidth) / 2;
            button1.Top = (formHeight - buttonHeight) / 2 - 30;

            button1.BackColor = Color.PaleGreen;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Arial", 12, FontStyle.Bold);

            Button button2 = new Button();
            button2.Text = "Main Menu";
            button2.Width = buttonWidth;
            button2.Height = buttonHeight;
            button2.Left = (formWidth - buttonWidth) / 2;
            button2.Top = (formHeight - buttonHeight) / 2 + 30;

            button2.BackColor = Color.PaleGreen;
            button2.ForeColor = Color.Black;
            button2.Font = new Font("Arial", 12, FontStyle.Bold);

            this.Controls.Add(button1);
            this.Controls.Add(button2);
        }
    }
}
