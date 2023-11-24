using System.Data;
using System.Runtime.CompilerServices;

public delegate void FileNameEventHandler(string name);

public abstract class CFile
{
    public string Name {get; set;}
    public string Extension {get; set;}
    public double Size {get; set;}

    public event FileNameEventHandler OnChanged;

    public CFile(string Name, string Extension, double Size)
    {
        this.Name = Name;
        this.Extension = Extension;
        this.Size = Size;
    }

    public void Update(string newName)
    {
        Name = newName;

        if (OnChanged != null)
        {
            OnChanged(newName);
        }
    }
}


public class CTXT : CFile
{
    public CTXT(string Name, string Extension, double Size) : base(Name, Extension, Size) 
    {

    }

    public static void Subscriber_Print(string name)
    {
        Console.WriteLine("The new name of the file is {0}", name);
    }
}

public class Program
{
    static void Main()
    {
        var txt = new CTXT("Buguggiate", ".txt", 135643);

        txt.OnChanged += CTXT.Subscriber_Print;

        txt.Update("Bergeggi");
        
    }
}