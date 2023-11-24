/*
y = ax^2 + bx + c

Classe parabola, proprietà
private a, b, c

*/

#include <iostream>
#include <math.h>
#include <string.h>

typedef struct Radici
{
    float x1;
    float x2;
    bool ok;  // ok = true -> radici sono accettabili
              // else -> radici non sono accettabili

} Radici;



class Parabola
{
    private:
        float a;
        float b;
        float c;
    
    public:
        Parabola() { a = 1; b = 0; c = 0;};
        Parabola(float a1, float b1, float c1);
        Radici soluzione();
        std::string stampa();
    
};

Parabola::Parabola(float a1, float b1, float c1)
{
    a = ((a1 == 0) ? 1 : a1);
    b = b1;
    c = c1;
}

Radici Parabola::soluzione()
{
    float delta = pow(b, 2) - 4 * a * c;

    Radici r;

    if (delta < 0)
    {
        r.x1 = r.x2 = -1;
        r.ok = false;
    }
    
    r.x1 = (-b + sqrt(delta)) / (2 * a);
    r.x2 = (-b - sqrt(delta)) / (2 * a);

    return r;
}

std::string Parabola::stampa()
{
    std::string s = "";

    s = "y = " + std::to_string(a) + "x^2 " + std::to_string(b) + "x " + std::to_string(c);

    return s;
}

int main()
{
    Parabola p(1, 4, 2);;

    Radici r;

    std::cout << "\n\nParabola p : " << p.stampa();

    r = p.soluzione();

    if (r.ok)
    {
        std::cout << "\n\nSoluzione della parabola : " << r.x1;
        std::cout << "\n\nSoluzione della parabola : " << r.x2;
    }
    else
        std::cout << "\n\nNon esistono soluzioni reali" << std::endl;

    return 0;
}

