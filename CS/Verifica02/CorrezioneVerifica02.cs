using System;
namespace Verifica02{
class CSegnalazione
{
    private string mMatricola, mMarca, mModello, mErrore;

    public string Matricola { get{ return mMatricola; } set{ mMatricola = value; }}
    public string Marca { get{ return mMarca; } set{ mMarca = value; }}
    public string Modello { get{ return mModello; } set{ mModello = value; }}
    public string Errore { get{ return mErrore; } set{ mErrore = value; }}

    public CSegnalazione()
    {
        Matricola = "";
        Marca = "";
        Modello = "";
        Errore = "Error";
    }
    public CSegnalazione (string Matricola, string Marca, string Modello, string Errore)
    {
        this.Matricola = Matricola;
        this.Marca = Marca;
        this.Modello = Modello;
        this.Errore = Errore;
    }
    public override string ToString()
    {
        string result = "";
        return result = this.Matricola + " " + this.Marca + " " + this.Modello + " " + this.Errore;
    }

}
class CLog
{
    private const int MAX_SEGNALAZIONI = 100;
    private string mInizio, mFine;
    // oppure
    private DateTime mStart, mEnd;
    private CSegnalazione[] mSegnalazioni;
    private int mNumeroSegnalazioni;

    public string Inizio
    {
        get { return mInizio; }
        set { mInizio = value; }
    }
    public string Fine
    {
        get { return mFine; }
        set { mFine = value; }
    }
    public CSegnalazione[] Segnalazioni
    {
        get { return mSegnalazioni; }
        set { mSegnalazioni = value; }
    }
    public DateTime Start
    {
        get { return mStart; }
        set { mStart = value; }
    }
    public DateTime End
    {
        get { return mEnd; }
        set { mEnd = value; }
    }
    public CLog()
    {
        Start = DateTime.Now;
        End = new DateTime();
        Segnalazioni = new CSegnalazione[MAX_SEGNALAZIONI];
        mNumeroSegnalazioni = 0;
    }
    // oppure
    // public CLog()
    // {
    //     Inizio = "";
    //     Fine = "";
    //     Segnalazioni = new CSegnalazione[3];
    // }
    public CLog(DateTime Start, DateTime End)
    {
        this.Start = Start;
        this.End = End;
        Segnalazioni = new CSegnalazione[MAX_SEGNALAZIONI];
        mNumeroSegnalazioni = 0;
    }
    public override string ToString()
    {
        string result = "";
        result = this.Inizio + " " + this.Fine + " ";
        for (int i = 0; i < mNumeroSegnalazioni; i++)
        {
            result += "\n" + this.Segnalazioni[i].ToString() + " ";
        }
        return result;
    }
    public void AddSegnalazione(CSegnalazione s)
    {
        if (mNumeroSegnalazioni < MAX_SEGNALAZIONI){
            this.Segnalazioni[mNumeroSegnalazioni] = s;
            mNumeroSegnalazioni++;
        }
        else
            throw new IndexOutOfRangeException(); // genero un ecezzione
    }
}
class HelloWorld
{
    static void Main()
    {
        CLog TodayLog = new CLog(DateTime.Now, DateTime.Now);
        TodayLog.AddSegnalazione(new CSegnalazione("12345", "HP", "E123", ""));
        CSegnalazione s1, s2;
        s1 = new CSegnalazione();
        s2 = new CSegnalazione();
        TodayLog.AddSegnalazione(s1);
        TodayLog.AddSegnalazione(s2);
        Console.WriteLine(TodayLog.ToString());
    }
}
}