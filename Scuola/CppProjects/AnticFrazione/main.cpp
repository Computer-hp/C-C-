#include <iostream>
#include <math.h>
#include <string>

using namespace std;

/*
CLASSE FRAZIONE PER LA GESTIONE DI UNA FRAZIONE DI DUE INTERI POSITIVI O
NEGATIVI
deve essere gestita la situazione del denominatore nullo che non deve essere accettata
ESEMPIO f1= 5/7 ; f2= - 3⁄4 ; f3 = 15/17 ecc...
SPECIFICHE MINIME :
 costruttore di default
 costruttore con parametri (gestione dell’eventuale denominatore nullo)
 metodo stampa formato numeratore/denominatore
 metodo per il calcolo reale della frazione f1=1/2 -> 0.5;
 operator per la somma operator+ f3=f1+f2;
 operator per la moltiplicazione operator * f3=f1*f2;
 operator per la differenza : operator - f3=f1 - f2;
 operator per la divisione operator/ f3=f1/f2;
 metodo per la SEMPLIFICAZIONE della frazione f1.semplifica() (ricarica sullo
stesso oggetto la frazione semplificata ai minimi termini) ex : se f1=26/70 la chiamata
di f1.semplifica() porta f1 a 13/35)

ALTRI METODI A DISCREZIONE DELLO STUDENTE
Esempio UGUAGLIANZA ex : f1.eguals(f2) ->boolean
Esempio MAGGIORE ex : f1.isgreat(f2) -> boolean
Esempio MINORE ex : f1.isless(f2) -> boolean
*/

class Frazione
{
  private:
    int numerator, denominator;

  public:
    Frazione() { numerator = 1; denominator = 1; }
    Frazione(int num, int den);

    string print();

    double calculateFraction();

    void simplify();

    Frazione operator + (Frazione f2);
    Frazione operator - (Frazione f2);
    Frazione operator * (Frazione f2);
    Frazione operator / (Frazione f2);

    int calculateLCM(int n1, int n2);

    int getNumerator()
    {
        return numerator;
    }

    void setNumerator(int value)
    {
        numerator = value;
    }

    int getDenominator()
    {
        return denominator;
    }

    void setDenominator(int value)
    {
        denominator = value;
    }
};

Frazione::Frazione(int num, int den)
{
    setNumerator(num);
    setDenominator(den);
}

string Frazione::print()
{
    return '\n' + to_string(numerator) + " / " + to_string(denominator) + '\n';
}

double Frazione::calculateFraction()
{
    return (double)numerator / (double)denominator;
}

int Frazione::calculateLCM(int n1, int n2)
{
    n1 = abs(n1);
    n2 = abs(n2);

    while (n1 != 0 && n2 != 0)
    {
        if (n1 > n2) n1 %= n2;

        else n2 %= n1;
    }

    return n1;
}

void Frazione::simplify()
{
    int mcd = 0;

    mcd = calculateLCM(numerator, denominator);

    if (mcd != 0)
    {
        numerator /= mcd;
        denominator /= mcd;
    }
}

Frazione Frazione::operator + (Frazione f2)
{
    Frazione result;

    result.denominator = denominator * f2.denominator;

    result.numerator = (result.denominator / denominator) * numerator  +  (result.denominator / f2.denominator) * f2.numerator;

    result.simplify();

    return result;

}

Frazione Frazione::operator - (Frazione f2)
{
    Frazione result;

    result.denominator = denominator * f2.denominator;

    result.numerator = (result.denominator / denominator) * numerator  -  (result.denominator / f2.denominator) * f2.numerator;

    result.simplify();

    return result;
}

Frazione Frazione::operator * (Frazione f2)
{
    Frazione result;

    result.denominator = denominator * f2.denominator;

    result.numerator = numerator * f2.numerator;

    result.simplify();

    return result;
}

Frazione Frazione::operator / (Frazione f2)
{
    Frazione result;

    result.numerator = numerator * f2.denominator;

    result.denominator = denominator * f2.numerator;

    result.simplify();

    return result;
}

int main()
{
    Frazione f1(-2,4), f2(1,3);

    cout << f1.print(); cout << f2.print();

    cout << f1.calculateFraction() << '\n';

    cout << f2.calculateFraction() << '\n';

    f1.simplify(); f2.simplify();

    cout << "\nSimplified f1: " << f1.print();
    cout << "\nSimplified f1: " << f2.print();

    Frazione f3;

    f3 = f1 + f2;

    cout << "\nSomma: " << f3.print();

    f3 = f1 - f2;

    cout << "\nSottrazione: " << f3.print();

    f3 = f1 * f2;

    cout << "\nMoltiplicazione: " << f3.print();

    f3 = f1 / f2;

    cout << "\nDivisione: " << f3.print();
}