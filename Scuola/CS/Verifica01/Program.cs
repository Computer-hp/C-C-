using System;

namespace MySpace{
class CStudente
{
public const int N = 10;

private string _Nome, _Cognome, _DataDiNascita;
private int[] _Voti;

private int numeroVoti = 0;

public string Nome{
    get { return _Nome; }
    set { _Nome = value; }
}
public string Cognome{
    get {return _Cognome; }
    set { _Cognome = value; }
}
public string DataDiNascita{
    get { return _DataDiNascita; }
    set { _DataDiNascita = value; }
}
public int[] Voti{
    get { return _Voti; }
    set { _Voti = value; }
}
// per togliere i warning basta andare sul file .csproj e cambiare
// enable in disable nel <Nullable> ... </Nullable>
public CStudente(){
    Nome = "Pierino";
    Cognome = "Rossi";
    DataDiNascita = "1/1/1970";
    Voti = new int[N];
}
// per togliere i warning basta andare sul file .csproj e cambiare
// enable in disable nel <Nullable> ... </Nullable>
public CStudente(string Nome, string Cognome, string DataDiNascita)
{
    // if ( && Cognome is not null && DataDiNascita is not null){
    this.Nome = Nome;
    this.Cognome = Cognome;
    this.DataDiNascita = DataDiNascita;
    Voti = new int[N];
    // }
}
// funziona che genera un voto
// void perche' non restituisce niente
public void AddVoto(int Voto)
{
    if ((Voto > 0) && (Voto < 11))
    {
        if (numeroVoti < 10)
            Voti[numeroVoti++] = Voto;
            else
                throw new Exception();
    } else
        throw new Exception();
}

public static float media(CStudente s)
{
    int somma = 0;

    for (int i = 0; i < s.numeroVoti; i++){
        somma += s.Voti[i];
    }
    return (somma / s.numeroVoti);
}
// se non fosse statico
// public float media()
// {
//     int somma = 0;

//     for (int i = 0; i < numeroVoti; i++){
//         somma += this.Voti[i];
//     }
//     return (somma / numeroVoti);
// }
// mi ritorna una stringa
public override string ToString()
{
    return "Studente: " + this.Nome + " " + this.Cognome + " nato il " + this.DataDiNascita;
}
}
class User
{
    static void Main() {
        CStudente s1, s2;
        s1 = new CStudente();
        s2 = new CStudente("Daniel", "Antic", "18/02/2005");

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Inserisci un voto per il primo studente");
            int voto = Convert.ToInt32(Console.ReadLine());
            s1.AddVoto(voto);
        }

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Inserisci un voto per il secondo studente");
            int voto = Convert.ToInt32(Console.ReadLine());
            s2.AddVoto(voto);
        }

        if (CStudente.media(s1) > CStudente.media(s2))
            Console.WriteLine(s1.ToString());
        else
            Console.WriteLine(s2.ToString());
    }
}
}