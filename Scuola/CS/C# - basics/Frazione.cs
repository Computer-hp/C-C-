using System;

namespace Daniel
{
    class Program
    {
       static void Main()
       {
            Console.Write("Inserisci un numero ");
            int first = /*Convert.ToInt32(Console.ReadLine());*/ 5;

            Console.Write("Inserisci un numero ");
            int second = /*Convert.ToInt32(Console.ReadLine());*/ 6;

// Vecchio metoto, non funziona più con quello nuovo:

            // Frazioni f1 = new Frazioni(first);

            // Frazioni f2 = new Frazioni(second);
            
            // int risultato = Numeri.somma(first, second);
            
            // Console.WriteLine("La somma dei due numeri e' = " + risultato);


// --------------------------------------------------------------------------------------
            // Using operators
            Numeri f1 = new Numeri(first);
            Numeri f2 = new Numeri(second);

            // Primo metodo:
            Numeri result = Numeri.somma(f1, f2);
            // Viene eseguito il costruttore override ToString in automatico
            // Se non ci fosse il costruttore override ToString result sarebbe = a Daniel.somma 
            //                                                               (non so perché)!!!

            Console.WriteLine("\nLa somma dei due numeri e' = " + result);
            
            // Secondo metodo:
            result = (f1 + f2);

            Console.WriteLine("\nLa somma dei due numeri e' = " + result);

       }
    }
}