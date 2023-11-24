using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Frazioni
{
    class CFrazione
    {
        // 1. variabili di istanza
        private int _num, _den; // usabili solo in questa classe

        // num è una variabile pubblica, quindi si può usare nei costruttori
        // 2. dichiarazione delle property
        public int num
        {
            get
            {
                return _num;
            }
            set
            {
                _num = value;
            }
        }
        // servono per controllare se i valori 
        // dati in input sono accettabili
        public int den
        {
            get
            {
                return _den;
            }
            set
            {
                if (value != 0)
                    _den = value;

                else
                    _den = 1; 
            }
        }

        // 3. Definizione costruttori
        public CFrazione() //costruttore di default è quello che non ha parametri
        {
            num = 0; //è come mettere this.num = 0; 
            den = 0; //con la property me lo mette lui a 1
        }
// _____________________________________________________________________________________________________________
// da qui iniziano i costruttori ai quali accedono f1, f2, f3, f4, f5 nel file "Program.cs"
        public CFrazione(int num, int den) //overloading costruttore = diamo più informazioni
        {
            // num property è uguale a num parametro
            this.num = num; 
            this.den = den;
        }
// __________________________________________________________________________________________________________________________
// Da qui si calcola il risultato --> r
    /*
        Metodo di classe è quello che ha lo static, non hanno bisogno 
        di un oggetto per essere chiamati ma del nome della classe
        Esempio: sqrt()
        Siccome non ha bisogno di un oggetto per essere chiamato non avrà this

        Metodo di istanza è il metodo public CFrazione Somma()
    */
    // Metodo di classe Somma 
        // static = di classe
        // nei metodi static non si può usare this.
        public static CFrazione Somma(CFrazione f1, CFrazione f2)
        {
            CFrazione ris;
            ris = new CFrazione();
            ris.den = f1.den * f2.den;
            ris.num = (ris.den / f1.den) * f1.num + (ris.den / f2.den) * f2.num;
            return ris.semplifica(ris);
        }
        // Metodo di istanza Somma:

        public CFrazione Somma(CFrazione f) // da fare sull'oggetto
        {
            CFrazione ris = CFrazione.Somma(this, f); // this = l'oggetto su cui applico la funzione --> f1
                                                      // della classe Program
            return ris;
        }

        //override dell'operatore +
        public static CFrazione operator +(CFrazione f1, CFrazione f2) // static = di classe
        {
            CFrazione ris = CFrazione.Somma(f1, f2);
            return ris;
        }

         // override sovrascrive il ToString:

         /* permette di estendere o modificare l'implementazione 
            astratta o virtuale di un metodo, una proprietà,
            un indicizzatore o un evento ereditato.

            Infatti, siccome esiste gia' il metodo ToString(),
            per farne uno nostro usiamo l'override.
        */
        // Si esegue da solo:
        public override string ToString() // da warning perchè è già presente in ogni classe il metodo ToString
        {                                 // override per sovrascrivere e togliere il warning :)
            // string risultato = "";
            // risultato = this.num.ToString() + "/" + this.den.ToString();
            // oppure:
            string risultato = this.num.ToString() + "/" + this.den.ToString();
            return risultato;
        }
        
        // questo si usa per gli oggetti Javascript
        // non c'e' l'override perche' c'e' un bool
        // public string ToString(bool json)
        // {
        //         string risultato = "";
        //         if (json)
        //                         // JsonSerializer classe che permette di trasformare una stringa json
        //             risultato = JsonSerializer.Serialize(this);
        //         else
        //             risultato = risultato.ToString();

        //         return risultato;
        // }

        //Sottrazione
        public static CFrazione Sottrazione(CFrazione f1, CFrazione f2) // static = di classe
        {
            CFrazione ris;
            ris = new CFrazione();
            ris.den = f1.den * f2.den;
            ris.num = (ris.den / f1.den) * f1.num - (ris.den / f2.den) * f2.num;
            return ris.semplifica(ris);
        }

        // public CFrazione Sottrazione(CFrazione f1) // da fare sull'oggetto
        // {
        //     CFrazione ris = CFrazione.Sottrazione(this, f1);
        //     return ris.semplifica(ris);
        // }

        //override dell'operatore -
        public static CFrazione operator -(CFrazione f1, CFrazione f2) // static = di classe
        {
            CFrazione ris = CFrazione.Sottrazione(f1, f2);
            return ris;
        }

        //Moltiplicazione
        public static CFrazione Moltiplicazione(CFrazione f1, CFrazione f2) // static = di classe
        {
            CFrazione ris;
            ris = new CFrazione();
            ris.den = f1.den * f2.den;
            ris.num = f1.num * f2.num;
            return ris.semplifica(ris);
        }

        // public CFrazione Moltiplicazione(CFrazione f1) // da fare sull'oggetto
        // {
        //     CFrazione ris = CFrazione.Moltiplicazione(this, f1);
        //     return ris;
        // }

        //override dell'operatore *
        public static CFrazione operator *(CFrazione f1, CFrazione f2) // static = di classe
        {
            CFrazione ris = CFrazione.Moltiplicazione(f1, f2);
            return ris;
        }

        // public CFrazione reciproco()
        // {
        //     CFrazione risultato = new CFrazione (this.den, this.num);
        //     return risultato;
        // }
        //Divisione
        public static CFrazione Divisione(CFrazione f1, CFrazione f2) // static = di classe
        {
            CFrazione ris;
            ris = new CFrazione();
            ris.den = f1.den * f2.num;
            ris.num = f1.num * f2.den;
            return ris.semplifica(ris);
        }

        // public CFrazione Divisione(CFrazione f1) // da fare sull'oggetto
        // {
        //     CFrazione ris = CFrazione.Divisione(this, f1);
        //     return ris;
        // }

        //override dell'operatore /
        public static CFrazione operator /(CFrazione f1, CFrazione f2) // static = di classe
        {
            CFrazione ris = CFrazione.Divisione(f1, f2);
            return ris;
        }
        // metodo di istanza
        public CFrazione semplifica(CFrazione result)
        {
            int mcd = 0;
            
            // CFrazione.MCD si può usare se MCD è static ----> private static int MCD() !!!!!
            mcd = MCD(result.num, result.den); 
                result.num /= mcd;
                result.den /= mcd;
            // return dell'oggetto cambiato
                return result;
                
            }

        // Funzione per trovare l'MCD
        private int MCD(int n1, int n2)
        {
            // Math.Abs() restituisce il valore assoluto di un numero!
            n1 = Math.Abs(n1);

            n2 = Math.Abs(n2);

            while (n1 != 0 && n2 != 0){
                if (n1 > n2)
                    n1 %= n2;
                else
                    n2 %= n1;
            }

            /* oppure
            while (n1 != n2)
            {
                if (n1 > n2) n1 = n1 - n2;
                else n2 = n2 - n1;
            }
            return a;
            */

            // | serve per far ritornare il valore di n1 e n2 piu' grande!
            Console.WriteLine("OR {0}", n1 | n2);
            return n1 | n2;
        }
    }
}