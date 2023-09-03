namespace WinFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Location = new Point(0, 1);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(50, 50);
            button1.TabIndex = 0;
            button1.Text = "Q";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Piece_Promote;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.Location = new Point(0, 51);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(50, 50);
            button2.TabIndex = 1;
            button2.Text = "R";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Piece_Promote;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.Location = new Point(0, 104);
            button3.Name = "button3";
            button3.Size = new Size(50, 50);
            button3.TabIndex = 2;
            button3.Text = "B";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Piece_Promote;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.None;
            button4.Location = new Point(0, 157);
            button4.Margin = new Padding(0);
            button4.Name = "button4";
            button4.Size = new Size(50, 50);
            button4.TabIndex = 3;
            button4.Text = "N";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Piece_Promote;
            // 
            // Form2
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(48, 218);
            ControlBox = false;
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            RightToLeft = RightToLeft.No;
            ShowIcon = false;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}