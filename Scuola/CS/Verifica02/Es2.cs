// /* 
// Si crei una classe scarpe con gli strectch (linguette adesive col feltro); ogni paio di scarpe è caratterizzato dalla dimensione 
// (data da un numero intero) e ha tre stretch sulla scarpa destra e tre sulla scarpa sinistra.
// Creare due paia di scarpe con gli stretch e per 10 volte estrarre casualmente su quale paio di scarpe cambiare da allacciato a 
// slacciato o viceversa uno degli stretch (scelto casualmente) di una delle due scarpe scelta casualmente.
// Dopo aver fatto aprire e chiudere per le 10 volte gli stretch, stampare la situazione finale delle scarpe; si consideri che 
// inizialmente tutti gli stretch sono slacciati.
//  */
// using System;

// class CScarpe
// {
//     private int mDimensione;
//     private int[] mNumeroStretchDx;
//     private int[] mNumeroStretchSx;

//     public int Dimensione
//     {
//         get { return mDimensione; }
//         set { mDimensione = value; }
//     }
//     public int[] NumeroStretchDx
//     {
//         get { return mNumeroStretchDx; }
//         set { mNumeroStretchDx = value; }
//     }
//     public int[] NumeroStretchSx
//     {
//         get { return mNumeroStretchSx; }
//         set { mNumeroStretchSx = value; }
//     }

//     public CScarpe(int Dimensione)
//     {
//         this.Dimensione = Dimensione;
//         // celle = 0, stretch slacciati
//         NumeroStretchDx = new int[3];
//         NumeroStretchSx = new int[3];
//     }

//     // metodo per cambiare stato stretch
//     public void Modifica(int nStretch, string lato)
//     {
//         if (lato == "Destra"){
//             if (this.NumeroStretchDx[nStretch] == 1) 
//                 this.NumeroStretchDx[nStretch] = 0;
//             else 
//                 this.NumeroStretchDx[nStretch] = 1;
//         }
//         else {
//             if (this.NumeroStretchDx[nStretch] == 1) 
//                 this.NumeroStretchDx[nStretch] = 0;
//             else 
//                 this.NumeroStretchDx[nStretch] = 1;
//         }
//     }
//     public override string ToString()
//     {
//         string resultDx = "";
//         string resultSx = "";

//         for (int i = 0; i < 3; i++)
//         {
//             if (this.NumeroStretchDx[i] == 1)
//                 resultDx = resultDx + " " + (i + 1) + "° stretch aperto";
//             else
//                 resultDx = resultDx + " " + (i + 1) + "° stretch chiuso";
            
//             if (this.NumeroStretchSx[i] == 1)
//                 resultSx = resultSx + " " + (i + 1) + "° stretch aperto";
//             else
//                 resultSx = resultSx + " " + (i + 1) + "° stretch chiuso";
//         }
//         return this.Dimensione + " Stretch destri: " + resultDx + " Stretch sinistri: " + resultSx;
//     }
//     static void Main()
//     {
//         CScarpe scarpe1 = new CScarpe(40);
//         CScarpe scarpe2 = new CScarpe(38);

//         Random random = new Random();
//         Random randomStr = new Random();

//         string[] strings = {"Destra", "Sinistra"};

//         for(int i = 0; i < 3; i++)
//         {
//             // se esce 1 sono scarpe1, se esce 2 sono scarpe2
//             int randomNumber = random.Next(1, 3);
//             int randomIndex = random.Next(0, strings.Length);
//             string lato = strings[randomIndex];

//             if (randomNumber == 1)
//             {
//                 int rStretch = random.Next(0, 3);
//                 scarpe1.Modifica(rStretch, lato);
//             }
//             else
//             {
//                 int rStretch = random.Next(0, 3);
//                 scarpe2.Modifica(rStretch, lato);
//             }
//         }
//         Console.WriteLine("Paio di scarpe 1: " + scarpe1.ToString());
//         Console.WriteLine("Paio di scarpe 2: " + scarpe2.ToString());
//     }
// }