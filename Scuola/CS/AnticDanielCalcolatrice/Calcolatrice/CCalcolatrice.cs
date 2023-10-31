//using System;
//using System.Collections.Generic;
//using System.Text;
    class CCalcolatrice
    {
        private CHugeNumber mPrimoOperando;
        private CHugeNumber mSecondoOperando;
        private CHugeNumber mRisultato;

        public CHugeNumber PrimoOperando
        {
            get { return mPrimoOperando; }
            set { mPrimoOperando = value; }
        }

        public CHugeNumber SecondoOperando
        {
            get { return mSecondoOperando; }
            set { mSecondoOperando = value; }
        }

        public CHugeNumber Risultato
        {
            get { return mRisultato; }
            set { mRisultato = value; }
        }
    }
