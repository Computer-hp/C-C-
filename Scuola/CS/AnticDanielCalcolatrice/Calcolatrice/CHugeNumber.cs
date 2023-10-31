using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class CHugeNumber
    {
        private const int N = 300;
        private int[] mCifre;
        public int[] Cifre
        {
            get { return mCifre; }
            set { mCifre = value; }
        }

        public CHugeNumber()
        {
            // metti un puntatore a un oggetto che è un vettore di tipo int
            Cifre = new int[N]; // cosi' si crea anche un vettore con 0 in tutte le celle
        }
        // usiamo una stringa perche' puo' possedere piu' caratteri di un int, double, ecc.
        public CHugeNumber(string numero)
        {
            bool isNegative = false;
            char[] stringArray;
        // se il primo carattere e' negativo, tolgo il - dall'array di char
            if (numero[0] == '-')
            {
                stringArray = (numero.Substring(1)).ToCharArray();
                isNegative = true;
            }
            else
                stringArray = numero.ToCharArray();
            // giro l'array di char e lo salvo nella stringa numero
            Array.Reverse(stringArray);
            numero = new string(stringArray);
            Cifre = new int[N];
        // salvo nell'ultima cella dell'array il primo carattere
            if (numero.Length < N) {
                for (int i = 0; i < numero.Length; i++)
                {
                    // Cifre[N - i - 1] = numero[numero.Length - i - 1] - "0";
                    if (isNegative == true && i == numero.Length - 1)
                    // se e' negativo il primo numero allora nell'array lo tasformo in negativo
                        Cifre[N - i - 1] = -(Convert.ToInt32(numero[i]) - 48);
                    else
                        Cifre[N - i - 1] = Convert.ToInt32(numero[i]) - 48;
                }
            }   else {
                    throw new InvalidOperationException();
                }
        }
        
        // Somma
        public static CHugeNumber operator +(CHugeNumber n1, CHugeNumber n2){
            // si crea l'array con lunghezza N tramite il costruttore public CHugeNumber()
            CHugeNumber risultato = new CHugeNumber();

            int Riporto = 0;

            for (int i = N - 1; i >= 0; i--)
            {
                    risultato.Cifre[i] = n1.Cifre[i] + n2.Cifre[i] + Riporto;

                    if (risultato.Cifre[i] > 9)
                    {
                        Riporto = 1;
                        risultato.Cifre[i] -= 10;

                        if (i == 0)
                            // throw new OverflowException();
                            Riporto = 0;
                    } else
                        Riporto = 0;
            }
            return risultato;
        }

        // Sottrazione
        public static CHugeNumber operator -(CHugeNumber n1, CHugeNumber n2){
            CHugeNumber risultato = new CHugeNumber();

            risultato = n1 + n2.complemento();

            if (risultato.Cifre[0] == 9)
            {
                risultato = risultato.complemento();
                for (int i = 0; i < risultato.Cifre.Length && risultato.Cifre[i] == 0; i++)
                {
                    if (i + 1 < risultato.Cifre.Length && risultato.Cifre[i + 1] != 0)
                        risultato.Cifre[i + 1] = 0 - risultato.Cifre[i + 1];
                }
                return risultato;
            }
            else
            {
                return risultato;
            }
        }
        private CHugeNumber complemento()
        {
            CHugeNumber risultato = new CHugeNumber();
            CHugeNumber uno = new CHugeNumber("1");

            for (int i = N - 1; i >= 0; i--)
            {
                risultato.Cifre[i] = 9 - this.Cifre[i];
            }
            return risultato + uno;
        }
        public static CHugeNumber operator * (CHugeNumber n1, CHugeNumber n2)
        {
            CHugeNumber ris = new CHugeNumber();
            CHugeNumber uno = new CHugeNumber("1");
            CHugeNumber zero = new CHugeNumber("0");
            while (maggiore(n2, zero) == true)
            {
                ris += n1;
                n2 = n2 - uno;
            }
            return ris;
        }

        public static CHugeNumber operator / (CHugeNumber n1, CHugeNumber n2)
        {
            CHugeNumber ris = new CHugeNumber("0");
            CHugeNumber uno = new CHugeNumber("1");
            CHugeNumber zero = new CHugeNumber("0");
            if (maggiore(n2, n1) == true)
            {
                return ris;
            }
            else
            {
                while (maggiore(n1, zero) == true)
                {
                // quando n1 e' minore del dividendo, devo stoppare
                if (maggiore(n1, n2) == true || uguale(n1, n2) == true)
                    {
                        ris += uno;
                    }
                    n1 -= n2;
                }
                return ris;
            }
        }
    // per controllare se le volte sono uguali al numero sotto radice
    public static bool uguale(CHugeNumber n1, CHugeNumber n2)
    {
        // n1 = volte * volte   n2 = numero sotto radice
        int len = N - lunghezza(n1), len2 = N - lunghezza(n2);
        if (len == len2)
        {
            int i = 0;

            while (i < len)
            {
                if (n1.Cifre[N - len + i + 1 - 1] == n2.Cifre[N - len + i + 1 - 1])
                    i++;
                else
                    return false;
            }
            if (i == len)
                return true;
        }
        return false;
    }
    // Radice quadrata
        public static CHugeNumber sqrt(CHugeNumber n)
        {
        CHugeNumber uno = new CHugeNumber("1");
        CHugeNumber result = new CHugeNumber("0");
        CHugeNumber volte = new CHugeNumber("1");
        while (maggiore(volte, n) == false)
            {
            if (uguale(volte * volte, n) == true)
            {
                result = volte;
                break;
            }

                volte += uno;
            }
            return result;
                
        }

        static int lunghezza(CHugeNumber n)
        {
            int len = 0;
            if (n.Cifre[0] != 0)
                return 0;
            else{
                int i = 0, state = 1;
                while (i < N && state == 1)
                {
                    if (n.Cifre[i] == 0){
                        len++;
                    } else
                        state = 0;

                    i++;
                }
                if (len == N)
                    return N;
            }
            return len;
        }
        public static bool maggiore(CHugeNumber n1, CHugeNumber n2)
        {
            int len = N - lunghezza(n1), len2 = N - lunghezza(n2);
            // se il primo numero nell'array e' negativo
            if (len != 0){
            // per controllare se la prima cifra e' negativa
            if (n1.Cifre[N - len] >= 0){
            if (len > len2)
                return true;
            else if (len == len2){
                int i = 0, state = 1;
                while (state == 1 && i < len){
                    if (n1.Cifre[N - len + 1 + i - 1] > n2.Cifre[N - len + 1 + i - 1])
                        return true;
                    else if (n1.Cifre[N - len + 1 + i - 1] < n2.Cifre[N - len + 1 + i - 1])
                        return false;
                    else
                        state = 1;
                    i++;
                }
            }
            }
        }
            return false;
        }
        public override string ToString()
        {
            string ris = "";
            int counter = 0;
            bool afterZero = false;
            
                for (int i = 0; i < N; i++)
                {
                    if (this.Cifre[i] == 0 && afterZero == false)
                        counter++;
                    else
                    {
                        ris += this.Cifre[i];
                        afterZero = true;
                    }
                }
                if (counter == N)
                    return "0";
                else
                    return ris.TrimStart('0');
        }
    }