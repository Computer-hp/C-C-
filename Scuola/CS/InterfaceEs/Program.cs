using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Principal;

class CPunto
{
    public int x {get; set;}
    public int y {get; set;}

    public CPunto(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

}

public interface IVelivolo
{
    int portata {get; set;}
    double velocitàMax {get; set;}

    int annoFabbricazione {get; set;}

    void decolla(int h, int inc);
    void atterra();    
}

class CBimotore : IVelivolo
{
    public int portata {get; set;}

    public double velocitàMax {get; set;}

    public int annoFabbricazione {get; set;}

    public int numeroMotori {get; set;}

    public int numeroPasseggeri {get; set;}

    public CPunto Punto {get; set;}

    public void decolla(int h, int inc)
    {
        for (int i = 0; i < h; i++)
        {
            this.Punto.x += inc;
            this.Punto.y += inc;
        }
    }
    public void atterra()
    {

    }
    public CBimotore()
    {
        Punto = new CPunto(0,0);
    }
}

class CMig : IVelivolo
{
    public int portata {get; set;}

    public double velocitàMax {get; set;}

    public int annoFabbricazione {get; set;}

    public int mitragliatrici {get; set;}

    public CPunto Punto {get; set;}

    public void decolla(int h, int inc)
    {
        for (int i = 0; i < h; i++)
        {
            this.Punto.x += inc;
            this.Punto.y += inc;
        }
    }
    public void spara()
    {

    }
    public void atterra()
    {

    }

    public CMig()
    {
        Punto = new CPunto(0,0);
    }
}

class CUltraLeggeroSenzaMotore : IVelivolo
{
    public int portata {get; set;}

    public double velocitàMax {get; set;}

    public int annoFabbricazione {get; set;}

    public int numeroAssi {get; set;}

    public CPunto Punto {get; set;}
    public void decolla(int h, int inc)
    {
        for (int i = 0; i < h; i++)
        {
            this.Punto.x += inc;
            this.Punto.y += inc;
        }
    }
    public void atterra()
    {

    }
    public CUltraLeggeroSenzaMotore()
    {
        Punto = new CPunto(0,0);
    }
}


public class Program
{
    static void Main()
    {
        CVeicoli Aerei = new CVeicoli(10);

        for (int i = 0; i < Aerei.Veicoli.Length; i++)
        {
            

            if (i % 2 == 0){
                Aerei.Veicoli[i] = new CMig();
                CMig mig = Aerei.Veicoli[i] as CMig;  // sets automatically as a reference

                if (mig != null)
                {
                    Console.WriteLine("\nPosizione inizialie {0} = {1} {2}", mig.GetType().Name, mig.Punto.x, mig.Punto.y);
                    Aerei.Veicoli[i].decolla(10, 2);
                    Console.WriteLine("Posizione finale {0} = {1} {2}\n", mig.GetType().Name, mig.Punto.x, mig.Punto.y);
                }
            }
            else{

                Aerei.Veicoli[i] = new CBimotore();
                CBimotore bimotore = Aerei.Veicoli[i] as CBimotore; // sets automatically as a reference

                if (bimotore != null)
                {
                    Console.WriteLine("\nPosizione inizialie {0} = {1} {2}", bimotore.GetType().Name, bimotore.Punto.x, bimotore.Punto.y);
                    Aerei.Veicoli[i].decolla(20, 4);
                    Console.WriteLine("Posizione finale {0} = {1} {2}\n", bimotore.GetType().Name, bimotore.Punto.x, bimotore.Punto.y);
                }
            }
        }
    }
    
}

public class CVeicoli
{
    public IVelivolo[] Veicoli {get; set;}

    public  CVeicoli(int n)
    {
        Veicoli = new IVelivolo[n];
    }


}