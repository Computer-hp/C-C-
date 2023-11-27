/*implementare la classe ANGOLO
* che rappresenta un angolo geometrico espresso in
* gradi-primi-secondi es:45°34'22''
* 45°60'00'' -> 46°00'00''
*
* specifiche :
* a) costruttore di default
* b) costruttore con parametri ( main-> Angolo a1(90,0,0) )
* c) metodo set(int g , int p , int s)
* d) metodo getGradi()
* d) metodo getPrimi()
* d) metodo getSecondi()
* d) metodo radianti() // converte l'angolo in radianti -> a1.radianti()->1.57
*/

#include <iostream>
#include <math.h>
#include <string>

#define PI 3.14

class Angolo
{
  private:
    int gradi;
    int primi;
    int secondi;

  public:
    Angolo() { gradi = 0; primi = 0; secondi = 0; }
    Angolo(int g, int p, int s) { gradi = g; primi = p; secondi = s; }

    int getGradi() { return gradi; }
    int getPrimi() { return primi; }
    int getSecondi() { return secondi; }

    void set(int g, int p, int s);

    double radianti();
};

void Angolo::set(int g, int p, int s)
{
    secondi = (s >= 0) ? s : 1;

    primi = (p >= 0) ? p : 1;

    gradi = (g >= 0) ? g : 1;

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

// Radians = ( Degrees + Minutes / 60 + Seconds / 3600​ ) × ( π / 180 )

double Angolo::radianti()
{
    double radianti = (gradi + primi / 60 + secondi / 3600) * ( PI / 180 );
    return radianti;
}

int main()
{
    Angolo a1;
    Angolo a2(12, 50, 120);

    std::cout << "Angolo: " << a1.getGradi() << "° " << a1.getPrimi() << "' " << a1.getSecondi() << "''" << std::endl;

    std::cout << "Angolo: " << a2.getGradi() << "° " << a2.getPrimi() << "' " << a2.getSecondi() << "''" << std::endl;

    a2.set(12, 50, 120);

    std::cout << "Angolo: " << a2.getGradi() << "° " << a2.getPrimi() << "' " << a2.getSecondi() << "''" << std::endl;
    
    std::cout << "Angolo in radianti: " << a2.radianti() << std::endl;

    return 0;
}
