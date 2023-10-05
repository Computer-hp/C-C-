using System.Diagnostics;
//using NAudio.Wave;

namespace Animal_Sounds
{
    public partial class Form1 : Form
    {
        public static string projectPath = (System.Environment.CurrentDirectory).Replace("Animal Sounds\\bin\\Debug\\net6.0-windows", "");

        CAnimal Animal;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void cat_Click(object sender, EventArgs e)
        {
            Animal = new Cat();
            Animal.MakeSound(projectPath, Animal.GetType().Name);
        }

        private void chimpanzee_Click(object sender, EventArgs e)
        {
            Animal = new Chimpanzee();
            Animal.MakeSound(projectPath, Animal.GetType().Name);

        }

        private void cow_Click(object sender, EventArgs e)
        {
            Animal = new Cow();
            Animal.MakeSound(projectPath, Animal.GetType().Name);

        }

        private void dog_Click(object sender, EventArgs e)
        {
            Animal = new Dog();
            Animal.MakeSound(projectPath, Animal.GetType().Name);

        }

        private void horse_Click(object sender, EventArgs e)
        {
            Animal = new Horse();
            Animal.MakeSound(projectPath, Animal.GetType().Name);
        }

        private void rattlesnake_Click(object sender, EventArgs e)
        {
            Animal = new Rattlesnake();
            Animal.MakeSound(projectPath, Animal.GetType().Name);

        }

        private void rooster_Click(object sender, EventArgs e)
        {
            Animal = new Rooster();
            Animal.MakeSound(projectPath, Animal.GetType().Name);
        }

        private void sheep_Click(object sender, EventArgs e)
        {
            Animal = new Sheep();
            Animal.MakeSound(projectPath, Animal.GetType().Name);
        }
        private void wolf_Click(object sender, EventArgs e)
        {
            Animal = new Wolf();
            Animal.MakeSound(projectPath, Animal.GetType().Name);
        }
    }
}