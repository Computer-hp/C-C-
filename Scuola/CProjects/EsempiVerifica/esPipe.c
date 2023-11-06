#include <stdio.h>
#include <unistd.h>
#include <sys/wait.h>
#include <stdlib.h>
#include <time.h>

#define LENGTH 5

void caricaVettore(int v[])
{
    srand(time(NULL));

    for (int i = 0; i < LENGTH; i++)
    {
        v[i] = rand() % 100 + 1;
    }
}

int main()
{
    int fd[2];

    if (pipe(fd) < 0)
    {
        printf("\nErrore creazione pipe");
        return -1;
    }

    int id = fork();

    if (id < 0)
    {
        printf("\nErrore fork");
        return -1;
    }
    if (id == 0)
    {
        close(fd[0]);

        int v[LENGTH];

        caricaVettore(v);

        write(fd[1], v, sizeof(v));

        close(fd[1]);


    }
    else
    {
        close(fd[1]);

        int v[LENGTH];

        read(fd[0], v, sizeof(v));

        for (int i = 0; i < LENGTH; i++)
        {
            printf("%i\n", v[i]);
        }

        close(fd[0]);
    }

    return 0;
}