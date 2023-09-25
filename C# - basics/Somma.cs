using System;

namespace Daniel
{
    class Numeri
    {
        private int _num;
        // private int _num2;

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
        // public int num2
        // {
        //     get
        //     {
        //         return _num2;
        //     }
        //     set
        //     {
        //         _num2 = value;
        //     }
        // }
        public Numeri()
        {
            num = 0;
        }
        public Numeri(int n)
        {
            this.num = n;
        }
        // Primo metodo:
        public static Numeri somma(Numeri f1, Numeri f2)
        {
            Numeri risultato = new Numeri();
            risultato.num = f1.num + f2.num;
            return risultato;
        }
        // Secondo metodo:
        public static Numeri operator +(Numeri f1, Numeri f2) // static = di classe
        {
            Numeri ris = Numeri.somma(f1, f2);
            return ris;
        }

        // Si esegue da solo
        public override string ToString() // da warning perchè è già presente in ogni classe il metodo ToString
        { // override per sovrascrivere e togliere il warning :)
            string risultato = "";
            risultato = this.num.ToString();
            return risultato;
        }
    }
}