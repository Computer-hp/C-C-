namespace Animal_Sounds
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            cat = new Button();
            dog = new Button();
            rooster = new Button();
            chimpanzee = new Button();
            horse = new Button();
            sheep = new Button();
            cow = new Button();
            rattlesnake = new Button();
            wolf = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // cat
            // 
            cat.BackgroundImage = Properties.Resources.cat;
            cat.BackgroundImageLayout = ImageLayout.Zoom;
            cat.Dock = DockStyle.Fill;
            cat.Location = new Point(3, 3);
            cat.Name = "cat";
            cat.Size = new Size(260, 143);
            cat.TabIndex = 0;
            cat.UseVisualStyleBackColor = true;
            cat.Click += cat_Click;
            // 
            // dog
            // 
            dog.BackgroundImage = Properties.Resources.dog;
            dog.BackgroundImageLayout = ImageLayout.Zoom;
            dog.Dock = DockStyle.Fill;
            dog.Location = new Point(3, 152);
            dog.Name = "dog";
            dog.Size = new Size(260, 143);
            dog.TabIndex = 1;
            dog.UseVisualStyleBackColor = true;
            dog.Click += dog_Click;
            // 
            // rooster
            // 
            rooster.BackgroundImage = (Image)resources.GetObject("rooster.BackgroundImage");
            rooster.BackgroundImageLayout = ImageLayout.Zoom;
            rooster.Dock = DockStyle.Fill;
            rooster.Location = new Point(3, 301);
            rooster.Name = "rooster";
            rooster.Size = new Size(260, 146);
            rooster.TabIndex = 2;
            rooster.UseVisualStyleBackColor = true;
            rooster.Click += rooster_Click;
            // 
            // chimpanzee
            // 
            chimpanzee.BackgroundImage = (Image)resources.GetObject("chimpanzee.BackgroundImage");
            chimpanzee.BackgroundImageLayout = ImageLayout.Zoom;
            chimpanzee.Dock = DockStyle.Fill;
            chimpanzee.Location = new Point(269, 3);
            chimpanzee.Name = "chimpanzee";
            chimpanzee.Size = new Size(260, 143);
            chimpanzee.TabIndex = 3;
            chimpanzee.UseVisualStyleBackColor = true;
            chimpanzee.Click += chimpanzee_Click;
            // 
            // horse
            // 
            horse.BackgroundImage = (Image)resources.GetObject("horse.BackgroundImage");
            horse.BackgroundImageLayout = ImageLayout.Zoom;
            horse.Dock = DockStyle.Fill;
            horse.Location = new Point(269, 152);
            horse.Name = "horse";
            horse.Size = new Size(260, 143);
            horse.TabIndex = 4;
            horse.UseVisualStyleBackColor = true;
            horse.Click += horse_Click;
            // 
            // sheep
            // 
            sheep.BackgroundImage = (Image)resources.GetObject("sheep.BackgroundImage");
            sheep.BackgroundImageLayout = ImageLayout.Zoom;
            sheep.Dock = DockStyle.Fill;
            sheep.Location = new Point(269, 301);
            sheep.Name = "sheep";
            sheep.Size = new Size(260, 146);
            sheep.TabIndex = 5;
            sheep.UseVisualStyleBackColor = true;
            sheep.Click += sheep_Click;
            // 
            // cow
            // 
            cow.BackgroundImage = (Image)resources.GetObject("cow.BackgroundImage");
            cow.BackgroundImageLayout = ImageLayout.Zoom;
            cow.Dock = DockStyle.Fill;
            cow.Location = new Point(535, 3);
            cow.Name = "cow";
            cow.Size = new Size(262, 143);
            cow.TabIndex = 6;
            cow.UseVisualStyleBackColor = true;
            cow.Click += cow_Click;
            // 
            // rattlesnake
            // 
            rattlesnake.BackgroundImage = (Image)resources.GetObject("rattlesnake.BackgroundImage");
            rattlesnake.BackgroundImageLayout = ImageLayout.Zoom;
            rattlesnake.Dock = DockStyle.Fill;
            rattlesnake.Location = new Point(535, 152);
            rattlesnake.Name = "rattlesnake";
            rattlesnake.Size = new Size(262, 143);
            rattlesnake.TabIndex = 7;
            rattlesnake.UseVisualStyleBackColor = true;
            rattlesnake.Click += rattlesnake_Click;
            // 
            // wolf
            // 
            wolf.BackgroundImage = (Image)resources.GetObject("wolf.BackgroundImage");
            wolf.BackgroundImageLayout = ImageLayout.Zoom;
            wolf.Dock = DockStyle.Fill;
            wolf.Location = new Point(535, 301);
            wolf.Name = "wolf";
            wolf.Size = new Size(262, 146);
            wolf.TabIndex = 8;
            wolf.UseVisualStyleBackColor = true;
            wolf.Click += wolf_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(wolf, 2, 2);
            tableLayoutPanel1.Controls.Add(dog, 0, 1);
            tableLayoutPanel1.Controls.Add(rattlesnake, 2, 1);
            tableLayoutPanel1.Controls.Add(rooster, 0, 2);
            tableLayoutPanel1.Controls.Add(sheep, 1, 2);
            tableLayoutPanel1.Controls.Add(chimpanzee, 1, 0);
            tableLayoutPanel1.Controls.Add(horse, 1, 1);
            tableLayoutPanel1.Controls.Add(cow, 2, 0);
            tableLayoutPanel1.Controls.Add(cat, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button cat;
        private Button dog;
        private Button rooster;
        private Button chimpanzee;
        private Button horse;
        private Button sheep;
        private Button cow;
        private Button rattlesnake;
        private Button wolf;
        private TableLayoutPanel tableLayoutPanel1;
    }
}