/*********************************************************************************************************************************************************


Supponiamo di voler creare una classe Veicolo che rappresenta un veicolo generico. La classe deve avere un costruttore di base
e un costruttore con parametri che inizializza i campi della classe. 

Property richieste:

tipo: il tipo di veicolo (es. automobile, moto, bicicletta, ecc.).
velocita: la velocità attuale del veicolo.

Inoltre, la classe deve avere i seguenti metodi:

accelera(velocita): aumenta la velocità del veicolo di velocita km/h.
decelera(velocita): diminuisce la velocità del veicolo di velocita km/h.
ferma(): ferma il veicolo.
get_velocita(): restituisce la velocità attuale del veicolo.
get_tipo(): restituisce il tipo di veicolo (es. automobile, moto, bicicletta, ecc.).
__str__(): restituisce una stringa che descrive il veicolo.
Inoltre, la classe Veicolo deve essere una classe base per le seguenti classi derivate:

Automobile: rappresenta un’automobile. Ha un campo aggiuntivo numero_porte che indica il numero di porte
dell’automobile.

Moto: rappresenta una motocicletta. Ha un campo aggiuntivo cilindrata che indica la cilindrata della 
motocicletta.


********************************************************************************************************************************************************/


#include <iostream>
#include <iomanip>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

string doubleToString(double value) 
{
    ostringstream stream;
    stream << fixed << setprecision(2) << value;
    return stream.str();
}

class Veicolo
{
  private:
    string tipo;
    double velocita;
    double velocitaMax;

  public:
    Veicolo() { tipo = "Yugo"; velocita = 50; velocitaMax = 90; }
    Veicolo(string &tipo, double velocita, double velocitaMax);

    string getTipo() { return tipo; }
    double getVelocita() { return velocita; }

    void accelera(double velocita);

    void decelera(double velocita);

    bool limiteVelocita(double velocita);

    string print();

};


Veicolo::Veicolo(string &tipo, double velocita, double velocitaMax)
{
    this->tipo = tipo;
    this->velocita = velocita;
    this->velocitaMax = velocitaMax;
}

bool Veicolo::limiteVelocita(double velocita)
{
    if (this->velocita + velocita >= 0 && this->velocita + velocita < velocitaMax)
        return true;

    return false;
}

void Veicolo::accelera(double velocita)
{
    if (limiteVelocita(velocita))
    {
        this->velocita += velocita;
        return;
    }
    this->velocita = velocitaMax;

    cout << "\nIl veicolo ha raggiunto la velocita' massima" << '\n';
}


void Veicolo::decelera(double velocita)
{
    if (limiteVelocita(-velocita))
    {
        this->velocita -= velocita;
        return;
    }

    this->velocita = 0;
    cout << "\nIl veicolo e' fermo" << '\n';
}

string Veicolo::print()
{
    return tipo + ", velocita' attuale " + doubleToString(velocita) + " km/h, velocita' massima " + doubleToString(velocitaMax) + " km/h";
}

class Automobile : public Veicolo
{
  private:
    int numeroPorte;


  public:
    Automobile() : Veicolo() { numeroPorte = 4; }
    Automobile(string tipo, int velocita, int velocitaMax, int numeroPorte) : Veicolo(tipo, velocita, velocitaMax)
    {
        setNumeroPorte(numeroPorte);
    }

    void setNumeroPorte(int value)
    {
      if (value < 2)
      {
          numeroPorte = 2;
          return;
      }
      
      if (value > 6)
      {
          numeroPorte = 6;
          return;
      }

      numeroPorte = value;
    }

};


class Moto : public Veicolo
{
  private:
    int cilindrata;
    vector<int> cilindrateDisponibili = { 50, 125, 200, 250, 400, 500, 1000};

  public:
    Moto() : Veicolo() { cilindrata = 125; }
    Moto(string tipo, int velocita, int velocitaMax, int cilindrata) : Veicolo(tipo, velocita, velocitaMax)
    {

    }

    void setCilindrata(int value)
    {
      if (find(cilindrateDisponibili.begin(), cilindrateDisponibili.end(), value) != cilindrateDisponibili.end())
      {
        cilindrata = value;
        return;
      }
      
      srand(time(NULL));

      int random = rand() % cilindrateDisponibili.size(); 

      cilindrata = cilindrateDisponibili[random];
    }
};


int main()
{
    Veicolo v;

    cout << v.getTipo() << '\n';
    cout << v.getVelocita() << '\n';

    v.accelera(50);
    cout << v.getVelocita() << '\n';

    v.decelera(100);

    cout << v.getVelocita() << '\n';

    cout << "Veicolo 1:  " << v.print();


    Automobile a("Audi A5", 100, 300, 4);

    cout << "\nVeicolo 2:  " << a.print();
}