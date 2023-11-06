/*
12 
Scrivere una funzione in linguaggio C per
sistema operativo Linux o Windows avente il se-
guente prototipo

float prodottoScalare (int N, float v[], float w[]);

dove N rappresenta la dimensione dei vettori
v e w e che restituisca il prodotto scalare tra v
e w, cioé la somma di prodotti tra gli elementi
corrispondenti dei due vettori; la funzione deve
massimizzare le prestazioni nel caso di esecu-
zione con due CPU disponibili utilizzando due
thread.
*/
#include <stdio.h>
#include <unistd.h>
#include <pthread.h>
#include <stdlib.h>
#include <time.h>

#define N_THREAD 2


struct Valori {
  
  float *v, *w, result;

  int start, end;
};

typedef struct Valori Valori;

void *calcolo(Valori *var)
{
    for (int i = var->start; i < var->end; i++)
    {
        var->result += var->v[i] * var->w[i];
    }
}

float prodottoScalare(int N, float v[], float w[])
{
    pthread_t thread[N_THREAD];
    
    Valori var[N_THREAD];
    
    float result = 0;
    
    for (int i = 0; i < N_THREAD; i++)
    {
        var[i].v = v;
        var[i].w = w;
        var[i].result = 0;
        
        if (i == 0)
            var[i].start = 0;
        
        else
            var[i].start = N / 2;

        var[i].end = N / (-(i - 2));

        printf("\n%i\n", var[i].end);
        
        pthread_create(&thread[i], NULL, (void * (*)(void *))calcolo, var);
    }
    
    for (int i = 0; i < N_THREAD; i++)
    {
        pthread_join(thread[i], NULL);
        result += var[i].result;
    }
    
    return result;
}

void caricaVettore(int N, float v[])
{
    srand(time(NULL));
    
    for (int i = 0; i < N; i++)
    {
        v[i] = (float)rand() / RAND_MAX * 100.00 + 1.00; // da 1 a 100
    }
}

int main()
{
    int N = 5;

    float v[N], w[N];

    caricaVettore(N, v);
    caricaVettore(N, w);

    // start timer
    clock_t start_time = clock();

    printf("Il prodotto scalare e' = %.2lf", prodottoScalare(N, v, w));

    // end timer
    clock_t end_time = clock(); // Stop the timer
    double elapsed_time = (double)(end_time - start_time) / CLOCKS_PER_SEC;
    
    printf("\nElapsed time: %.2lf seconds\n", elapsed_time);

    return 0;
}