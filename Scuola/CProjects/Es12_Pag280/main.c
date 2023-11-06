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
#include <sys/wait.h>
#include <pthread.h>
#include <stdlib.h>
#include <time.h>

#define N_THREADS 2
#define N_PROCESSES 2

struct Valori
{
    float *v;
    float *w;
    float result;
    int start;
    int end;
};

typedef struct Valori Valori;

void caricaVettore(float array[], int dim)
{
    srand(time(NULL));

    for (int i = 0; i < dim; i++)
    {
        array[i] = (float)rand() / RAND_MAX * 100  + 1; // da 1 a 100
    }

}

void *calcolo(Valori *valori)
{
    for (int i = valori->start; i < valori->end; i++)
    {
        valori->result += valori->v[i] * valori->w[i];
    }


}


float prodottoScalare(float v[], float w[], int dim)
{
    float result = 0;

    Valori valori[N_THREADS];
    
    pthread_t threads[N_THREADS];

    for (int i = 0; i < N_THREADS; i++)
    {
        valori[i].v = v;
        valori[i].w = w;
        valori[i].result = 0;
        
        if (i == 0) 
            valori[i].start = 0;
        else 
            valori[i].start = dim / 2;

        valori[i].end = dim / (-(i - 2));

        pthread_create(&threads[i], NULL, (void * (*)(void *))calcolo, valori);
    }


    // pthread_join();
    for (int i = 0; i < N_THREADS; i++)
    {
        pthread_join(threads[i], NULL);
        result += valori[i].result;
    }

    return result;
}


int main()
{
    int dim = 5;

    float v[dim], w[dim];


    caricaVettore(v, dim);
    caricaVettore(w, dim);

    float result;

    int fd[2], sum = 0;

    if (pipe(fd) == -1)
    {
        printf("Errore creazione pipe");
        return -1;
    }

    // start timer
    clock_t start_time = clock();

    int id = fork();

    if (id < 0)
    {
        printf("\nErrore creazione processo");
        return -1;
    }

    if (id == 0)
    {
        close(fd[0]);

        result = prodottoScalare(v, w, dim);

        write(fd[1], &result, sizeof(result));

        close(fd[1]);

    } else
    {
        close(fd[1]);

        read(fd[0], &result, sizeof(result));
        printf("\nIl prodotto scalare = %.2lf", result);

        close(fd[0]);

        // end timer
        clock_t end_time = clock(); // Stop the timer
        double elapsed_time = (double)(end_time - start_time) / CLOCKS_PER_SEC;
        
        printf("\nElapsed time: %.2lf seconds\n", elapsed_time);
    }
    wait(NULL);

    return 0;
}