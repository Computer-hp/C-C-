class Program
    {
        static void Main(string[] args)
        {
 
            Console.Write("n=");
            int n = int.Parse(Console.ReadLine());
 
            Console.Write("val=");
            int val = int.Parse(Console.ReadLine());
 
            int x = 0;
 
            int i = 0;
            int mag_val = 0;
            int min_val = 0;
 
            while(i<n) { Console.Write("Inserisci numero {0}:", i); x = int.Parse(Console.ReadLine()); if (x > val) mag_val++;
                else if (x < val) min_val++;
 
                i++;
            }
 
            Console.WriteLine($"Maggiori di val: {mag_val}");
            Console.WriteLine($"Minori di val: {min_val}");
 
            Console.WriteLine($"\n\nPremi un tasto per terminare il programma");
            Console.ReadKey();
        }
    }