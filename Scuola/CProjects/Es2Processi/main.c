#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <unistd.h>
#include <sys/wait.h>
#include <time.h>

// Numero Per Riga
#define NPR 30
// Grandezza vettore
#define MAX 100

#define RANGE 100

void stampaVettore(int v[], int dim, char *commento);
void caricaVettore(int v[], int dim);
int bubbleSort(int v[], int dim);

int main() {

    /*
    int v[DIM];
    int v1[DIM/2]; // padre
    int v2[DIM/2]; // figlio

    caricaVettore(v, MAX);
    stampaVettore(v, MAX, "VETTORE ORIGINALE");


    for (int i = 0; i < DIM/2; i++)
        v1[i] = v[i];
    for (int i = DIM/2 + 1; i < DIM; i++)
        v2[j] = v[i];

    int id = fork();

    if (id == 0) {
        // siamo nel figlio
        printf("\n\nSONO IL FIGLIO");
        int scambiFiglio = bubbleSort(v2, DIM/2);
        stampaVettore(v2, DIM/2, "VETTORE ORDINATO DEL FIGLIO");
        printf("\n\nScambi effettuati dal figlio: %d\n", scambiFiglio);

    }
    else {
        // siamo nel padre
        printf("\n\nSONO IL PADRE");
        int scambiPadre = bubbleSort(v2, DIM/2);
        stampaVettore(v2, DIM/2, "VETTORE ORDINATO DEL FIGLIO");
        printf("\n\nScambi effettuati dal figlio: %d\n", scambiPadre);
    }
    */

    int v[MAX];
    caricaVettore(v, MAX);
    stampaVettore(v, MAX, "VETTORE ORIGINALE");

    int scambi = bubbleSort(v, MAX);

    stampaVettore(v, MAX, "VETTORE ORDINATO");

    printf("\n\n Numero degli scambi: %d", scambi);

    return 0;
}

void stampaVettore(int v[], int dim, char *commento)
{
    printf("\n%p", *commento);

    for (int i = 0; i < dim; i++)
    {
        if (!(i % NPR == 0))
            printf("\n");

        printf("\n%d \t", v[i]);
    }
}

void caricaVettore(int v[], int dim)
{
    srand(time(NULL));

    for (int i = 0; i < dim; i++)
    {
        v[i] = (rand() % RANGE) + 1;
    }
}

int bubbleSort(int v[], int dim)
{
    int tmp, conta = 0;

    for (int i = 0; i < dim; i++)
    {
        for (int j = 0; j < dim; j++)
        {
            if (v[j] > v[j + 1])
            {
                tmp = v[j];
                v[j]= v[j + 1];
                v[j + 1] = tmp;
            }
        }
    }

    return conta;
}