// /* 
// Creare una classe Centralina che ha un nome, un indirizzo, e un vettore che contiene i valori delle misure.
// Un valore è anch'esso una classe che ha come variabili la Data e il valore.
// Fare la classe Centralina con delle funzioni per aggiungere o togliere valori.
// Una centrale
// più date e misure
// metodo che quando lo chiamo gli spedisco i valori da mettere nell'array
//  */
// namespace Es1
// {
// class CCentralina
// {
//     public const int N = 3;
//     private string _Nome, _Indirizzo;
//     private CMisure[] _Misure;

//     public string Nome
//     {
//         get { return _Nome; }
//         set { _Nome = value; }
//     }

//     public string Indirizzo
//     {
//         get { return _Indirizzo; }
//         set { _Indirizzo = value; }
//     }
//     public CMisure[] Misure
//     {
//         get { return _Misure; }
//         set { _Misure = value; }
//     }
//     public CCentralina(string Nome, string Indirizzo)
//     {
//         this.Nome = Nome;
//         this.Indirizzo = Indirizzo;
//         Misure = new CMisure[N];
//     }
//     // this. si può anche omettere
//     public void aggiungiVal(CMisure C, int i)
//     {
//         this.Misure[i] = C;
//     }
    
// }
// class CMisure
// {
//     private string _Data, _Valore;

//     public string Data
//     {
//         get { return _Data; }
//         set { _Data = value; }
//     }
//     public string Valore
//     {
//         get { return _Valore; }
//         set { _Valore = value; }
//     }

//     public CMisure(string Data, string Valore)
//     {
//         this.Data = Data;
//         this.Valore = Valore;
//     }

//     public override string ToString()
//     {
//         string str = "";
//         str = this.Data + " " + this.Valore;

//         return str;
//     }
// }
// }