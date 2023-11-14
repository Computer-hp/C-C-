#include <iostream>
#include <math.h>
#include <string>

using namespace std;

class Forza {

private: //attributi
    float x;
    float y;
    //1)
    //2) metodi normali
    //3)operator

public:
    //costruttore overloading SI
    Forza() { x = y = 0; }; //costruttore di default
    Forza(float xx, float yy);

    float modulo();
    string stampa();

    Forza operator+(Forza f2);
    Forza operator*(float k);

};

Forza Forza::operator+(Forza f2){ //non operator : _ , . :: ;
    Forza f3;

    f3.x = this->x+f2.x;
    f3.y = this->y+f2.y;

    return f3;
}

string Forza::stampa(){
    string s = "";
    s = "("+ to_string(x)+";"+ to_string(y)+")";

    return s;
}

Forza::Forza(float xx, float yy){
    x=xx;
    y=yy;
}

float Forza::modulo(){
    return sqrt(pow(x, 2)+pow(y, 2));
}

Forza Forza::operator*(float k){
    return Forza(k*this->x, k*this->y);
}

int main() {
    Forza f2(10, 20);

    Forza f1(4, 3);
    Forza f3;

    f3 = f1 + f2;

    cout<< "\n\npiù normale "<< (4+5);

    int k;
    cout<<"\n\nInserire la var k : ";
    cin>> k;

    f3 = f1*k; //i programmi eseguono programma da dx a sx
               //non si può scrivere k*f1

    cout<< "\nil modulo di f1 è : "<<f1.modulo();
    cout<< "\nLa forza f1 vale : "<<f1.stampa();
    cout<< "\nLa forza f2 vale : "<<f2.stampa();
    cout<< "\nLa forza f3 vale : "<<f3.stampa();

    return 0;
}
