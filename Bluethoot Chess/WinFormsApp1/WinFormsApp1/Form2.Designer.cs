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
            Q = new Button();
            R = new Button();
            B = new Button();
            N = new Button();
            SuspendLayout();
            // 
            // Q
            // 
            Q.Anchor = AnchorStyles.None;
            Q.Location = new Point(0, 1);
            Q.Margin = new Padding(0);
            Q.Name = "Q";
            Q.Size = new Size(50, 50);
            Q.TabIndex = 0;
            Q.UseVisualStyleBackColor = true;
            Q.Click += Piece_Promote;
            // 
            // R
            // 
            R.Anchor = AnchorStyles.None;
            R.Location = new Point(0, 51);
            R.Margin = new Padding(0);
            R.Name = "R";
            R.Size = new Size(50, 50);
            R.TabIndex = 1;
            R.UseVisualStyleBackColor = true;
            R.Click += Piece_Promote;
            // 
            // B
            // 
            B.Anchor = AnchorStyles.None;
            B.Location = new Point(0, 104);
            B.Name = "B";
            B.Size = new Size(50, 50);
            B.TabIndex = 2;
            B.UseVisualStyleBackColor = true;
            B.Click += Piece_Promote;
            // 
            // N
            // 
            N.Anchor = AnchorStyles.None;
            N.Location = new Point(0, 157);
            N.Margin = new Padding(0);
            N.Name = "N";
            N.Size = new Size(50, 50);
            N.TabIndex = 3;
            N.UseVisualStyleBackColor = true;
            N.Click += Piece_Promote;
            // 
            // Form2
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(48, 218);
            ControlBox = false;
            Controls.Add(N);
            Controls.Add(B);
            Controls.Add(R);
            Controls.Add(Q);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            RightToLeft = RightToLeft.No;
            ShowIcon = false;
            ResumeLayout(false);
        }

        #endregion

        private Button Q;
        private Button R;
        private Button B;
        private Button N;
    }
}