#include <iostream>

using namespace std;

class EnglishTimer
{
  private:
    int ore, minuti, secondi;

  public:
    EnglishTimer() { ore = 0; minuti = 0; secondi = 0; }
    EnglishTimer(int o, int m, int s);

    void set(int o, int m, int n);
    void reset();
    void print();

    EnglishTimer operator+(EnglishTimer t2);
    EnglishTimer operator-(EnglishTimer t2);
    EnglishTimer operator>(EnglishTimer t2);
    EnglishTimer operator<(EnglishTimer t2);
    bool operator==(EnglishTimer t2);

    int getOre()
    {
        return ore;
    }

    int getMinuti()
    {
        return minuti;
    }

    int getSecondi()
    {
        return secondi;
    }
};


EnglishTimer::EnglishTimer(int o, int m, int s)
{
    set(o, m, s);
}


void EnglishTimer::set(int o, int m, int s)
{
    secondi = (s < 0) ? 0 : s;
    minuti = (m < 0) ? 0 : m;
    ore = (o < 0) ? 0 : o;

    if (secondi > 59)
    {
        minuti += secondi / 60;
        secondi %= 60;
    }

    if (minuti > 59)
    {
        ore += minuti / 60;
        minuti %= 60;
    }

    if (ore < 24)
        return;

    ore %= 24;
}

void EnglishTimer::reset()
{
    secondi = 0;
    minuti = 0;
    ore = 0;
}

void EnglishTimer::print()
{
    char zero = (ore < 10) ? '0' : '\0';

    if (ore <= 12)
        printf("\nTime: %c%i : %i : %i a.m.\n", zero, this->ore, this->minuti, this->secondi);
    else
        printf("\nTime: %c%i : %i : %i p.m.\n", zero, this->ore - 12, this->minuti, this->secondi);
    
}


EnglishTimer EnglishTimer::operator+(EnglishTimer t2)
{
    EnglishTimer result;

    int newSecondi = this->secondi + t2.secondi;
    int newMinuti = this->minuti + t2.minuti;
    int newOre = this->ore + t2.ore;

    result.set(newOre, newMinuti, newSecondi);

    return result;
}

EnglishTimer EnglishTimer::operator-(EnglishTimer t2)
{
    EnglishTimer result;

    int newSecondi = this->secondi - t2.secondi;
    int newMinuti = this->minuti - t2.minuti;
    int newOre = this->ore - t2.ore;

    result.set(newOre, newMinuti, newSecondi);

    return result;
}



// t1 > t2

EnglishTimer EnglishTimer::operator>(EnglishTimer t2)
{
    if (this->ore > t2.ore) return *(this);
    else return t2;
    
    if (this->minuti > t2.minuti) return *(this);
    else return t2;

    if (this->secondi > t2.secondi) return *(this);
    
    return t2;
}

// t1 < t2

EnglishTimer EnglishTimer::operator<(EnglishTimer t2)
{
    if (this->ore < t2.ore) return *(this);
    else return t2;
    
    if (this->minuti < t2.minuti) return *(this);
    else return t2;

    if (this->secondi < t2.secondi) return *(this);
    
    return t2;
}

bool EnglishTimer::operator==(EnglishTimer t2)
{
    if (this->ore != t2.ore)
        return false;
    
    if (this->minuti != t2.minuti)
        return false;

    if (this->secondi != t2.secondi)
        return false;

    return true;
}

int main()
{
    EnglishTimer t1(24, 58, 25);
    EnglishTimer t2(24, 58, 25);
    EnglishTimer t3(0,0,0);

    t1.print();

    t3 = t1 + t2;   

    cout << "\nsum: ";
    t3.print();

    cout << "\nsubtraction: ";
    t3 = t1 - t2;

    t3.print();
    
    t3 = t1 > t2;

    t3.print();

    t3 = t1 < t2;

    t3.print();

    if (t1 == t2)
        cout << "\nt1 == t2" << '\n';

    t3.print();

    return 0;
}