/*
implementare la classe angolo
che rappresenta un angolo gemoetrico espresso in
gradi-primi-secondi es : 45° 34' 22''
45° 60' 00'' -> 46° 00' 00''

specifiche:
    a) costruttore di default
    b) costruttore con parametri   nel main -> Angolo a(33, 22, 11)
    c) metodo set()
    d) metodo getGradi() getPrimi()  getSecondi()
    e) metodo radianti()    convertire l'angolo in radianti -> se Angolo a (90, 0, 0) sarà = pi
*/

#include <iostream>
#include <math.h>

class Angolo {
    private:
        int gradi;
        int primi;
        int secondi;

    public:
        Angolo() { gradi = 0; primi = 0; secondi = 0; }

        Angolo(int g, int p, int s);

        void set();

        // Metodi per ottenere i valori dell'angolo
        int getGradi() const 
        {
            return gradi;
        }

        int getPrimi() const 
        {
            return primi;
        }

        int getSecondi() const 
        {
            return secondi;
        }

        double radianti();
};

Angolo::Angolo(int g, int p, int s)
{
    gradi = g;
    primi = p;
    secondi = s;
}

void Angolo::set() 
{
    if (secondi >= 60) 
    {
        primi += secondi / 60;
        secondi %= 60;
    }
    if (primi >= 60) 
    {
        gradi += primi / 60;
        primi %= 60;
    }
}

double Angolo::radianti()
{
    const double PI = 3.14159265358979323846;
    return (gradi + primi / 60.0 + secondi / 3600.0) * (PI / 180.0);
}

int main() 
{
    Angolo a1;

    std::cout << "Angolo: " << a1.getGradi() << "° " << a1.getPrimi() << "' " << a1.getSecondi() << "''" << std::endl;

    Angolo a2(33, 22, 11);

    std::cout << "Angolo: " << a2.getGradi() << "° " << a2.getPrimi() << "' " << a2.getSecondi() << "''" << std::endl;

    std::cout << "Angolo in radianti: " << a2.radianti() << std::endl;

    return 0;
}