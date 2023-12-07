/*
/// creazione di una classe "stringa" per la gestione di
/// una stringa come oggetto
/// NON SI USA LA CLASSE <string> !!! (lo scopo è proprio creare questa classe)
/// Si puo' solo usare la libreria <string.h>

/// costruttore
stringa();

/// metodi
stringa reverse(void);   // inverte al stringa ex ciao->oaic
void set();   // permette di inserire una stringa nuova nell'oggetto
void stampa();  // permette di mostrare a video l'oggetto
int cerca(char c);  // restituisce la prima posizione del carattere "c" dentro la stringa oppure -1
int conta(char c);  // restituisce la quantità di caratteri "c" dentro la stringa oppure, altrimenti 0
void toUpper() ;   // trasforma la stringa tutta in maiuscolo
void toLower() ;   // trasforma la stringa tutta in minuscolo
void toFlip() ;   // inverte maiuscole e minuscole della stringa
bool palindroma();   // restituisce true se la stringa è palindroma (ex: "otto")
/// "false" se NON è palindroma (ex:"ciao") NB limitiamo il caso a singole parole

/// operator
int operator ~ () ;   // restiutuisce la lunghezza della stringa
stringa operator +(striga s2) // permette di fare s3=s1+s2
bool operator == (stringa s2);
*/

#include <iostream>
#include <string.h>

const int LWR_UPR_Subtrahend = 32;

class Stringa
{
  private:
    char *str;

  public:
    Stringa() {str = nullptr;}
    Stringa(char *s) {str = s;}

    void reverse();
    void set(const char *stringa);
    void stampa();
    int cerca(char c);
    int conta(char c);
    void toUpper();
    void toLower();
    void toFlip();
    bool palindroma();

    int operator ~();
    Stringa operator + (Stringa s2);
    bool operator == (Stringa s2);
};

void Stringa::reverse()
{
    for (int i = 0; i < strlen(str) / 2; i++)
    {
        int back = strlen(str) - i - 1;

        char tmp = this->str[i];

        str[i] = str[back];
        str[back] = tmp;
    }
}

void Stringa::set(const char *stringa)
{
    delete[] str;

    str = new char[strlen(stringa) + 1]; // Allocate memory for the string
    strcpy(str, stringa);
}

void Stringa::stampa()
{
    std::cout << str << std::endl;
}

int Stringa::cerca(char c)
{
    int i = 0;

    while (i < strlen(str))
    {
        if (str[i] == c)
            return i;
        i++;
    }

    return -1; 
}

int Stringa::conta(char c)
{
    int i = 0, counter = 0;

    while (i < strlen(str))
    {
        if (str[i] == c)
            counter++;
        i++;
    }

    return counter;
}

void Stringa::toUpper()
{
    for (int i = 0; i < strlen(str); i++)
    {
        if (str[i] >= 'a' && str[i] <= 'z')
        {
            str[i] = str[i] - LWR_UPR_Subtrahend;
        }
    }
}
void Stringa::toLower()
{
    for (int i = 0; i < strlen(str); i++)
    {
        if (str[i] >= 'A' && str[i] <= 'Z')
        {
            str[i] = str[i] + LWR_UPR_Subtrahend;
        }
    }
}
void Stringa::toFlip()
{
    for (int i = 0; i < strlen(str); i++)
    {
        if (str[i] >= 'A' && str[i] <= 'Z')
            str[i] = str[i] + LWR_UPR_Subtrahend;

        else if (str[i] >= 'a' && str[i] <= 'z')
            str[i] = str[i] - LWR_UPR_Subtrahend;
    }
}
bool Stringa::palindroma()
{
    for (int i = 0; i < strlen(str) / 2; i++)
    {
        int back = strlen(str) - i - 1;

        if (str[i] != str[back])
            return false;
    }
    
    return true;
}

int Stringa::operator ~()
{
    int i;

    for (i = 0; str[i] != '\0'; i++);

    return i;
}

void setString(char *str, int start, int end, const char* tmp)
{
    for (int i = start; i < end; i++)
    {
        str[i] = tmp[i - start];
    }
}

Stringa Stringa::operator + (Stringa s2)
{
    int totalLength = strlen(this->str) + strlen(s2.str);

    char *str = new char[totalLength + 1];

    setString(str, 0, strlen(this->str), this->str);

    setString(str, strlen(this->str), totalLength, s2.str);

    Stringa result;

    result.set(str);

    return result;
}

bool Stringa::operator == (Stringa s2)
{
    for (int i = 0; i < strlen(str); i++)
    {
        if (str[i] != s2.str[i])
            return false;
    }
    
    return true;
}

int main()
{
    Stringa s;

    s.set("Hello");
    s.stampa();

    s.toFlip();

    s.stampa();

    return 0;
}