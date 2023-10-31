#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

struct args
{
    int argomento1;
    int argomento2;
};


void *funzioneStampa(char *string)
{
    printf("%s\n", string);

}

void *somma(struct args* a)
{
    // la freccia indica al puntatore di puntare all'argomento 1/2.

    int somma = a->argomento1 + a->argomento2;
    printf("%i\n", somma);
}

int main()
{
    pthread_t myThread;

    struct args arg1;

    arg1.argomento1 = 10;
    arg1.argomento2 = 12;

    printf("i\n", arg1.argomento1);

    pthread_create(&myThread, NULL, somma, &arg1);

    // avvio il 1° thread              funzione        argomenti della funzione
    pthread_create(&myThread, NULL, funzioneStampa, "Ciao dal primo thread");

    // 2° thread
    pthread_create(&myThread, NULL, funzioneStampa, "Ciao dal secondo thread");


    pthread_join(myThread, NULL);



    return 0;
}