using System.Data.Common;
using System.Reflection;

// interfaccia è un elenco di funzioni
/*interface IComportamenti
{
    public void cammina();
    public void dormi();
    public bool mangia(int a);
}*/

interface IVerso
{
    public void makeSound()
    {

    }
}


abstract class Animal : IVerso
{
    public void cammina()
    {
        throw new NotImplementedException();
    }
    public void dormi()
    {
        throw new NotImplementedException();
    }
    public bool mangia(int a)
    {
        throw new NotImplementedException();
    }
    public abstract void makeSound();

    public virtual void msg()
    {
        Console.WriteLine("Hello");
    }

    public void Prova()
    {

    }
}
// Dog eredita da Animal e implementa l'interfaccia IComportamenti (cioè possiede tutto quello che è stato definito in IComportamenti)
class Dog : Animal
{
    public override void makeSound()
    {
        Console.WriteLine("Bau Bau");
    }
    public override void msg(){
        Console.WriteLine("Hello Bau bau");
    }
}
class Cat : Animal
{
    public override void makeSound()
    {
        Console.WriteLine("Miau Miau");
    }
}

class Snake : Animal
{
    public override void makeSound()
    {
        Console.WriteLine("Bsssssssssss");
    }
}

class Sconosciuto : IVerso
{
    public void cammina()
    {
        throw new NotImplementedException();
    }
    public void dormi()
    {
        throw new NotImplementedException();
    }

    public string foo()
    {
        return ("bar");
    }
    public bool mangia(int a)
    {
        throw new NotImplementedException();
    }
}

internal class Program
{
    public static void Verso(IVerso a)
    {
        a.makeSound();
        a.msg();
    }

    static void Main()
    {
        IVerso a;
        a = new Dog();
                  // p ha un comportamento polimorfo
        
        Verso(a);


        a = new Cat();
        Verso(a);
    
        a = new Snake();
        Verso(a);

        Console.ReadLine();
    }
}