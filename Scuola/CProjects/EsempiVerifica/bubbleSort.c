#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h>
#include <sys/wait.h>

#define NPR 20
#define MAX 100
#define RANGE 100

void caricaVettore(int v[], int dim)
{
	srand(time(NULL));

	for (int i = 0; i < dim; i++)
	{
		v[i] = rand() % RANGE + 1;
	}
}

void stampaVettore(int v[], int dim, char* commento)
{
	printf("\n%s", commento);

	for (int i = 0; i < dim; i++)
	{
		if (i % 20 == 0)
			printf("\n");

		printf("%d\t", v[i]);
	}
	printf("\n");
}

void bubbleSort(int v[], int dim)
{
	for (int i = 0; i < dim; i++)
	{
		for (int j = 0; j < dim - 1; j++)
		{
			if (v[j] > v[j + 1])
			{
				int tmp = v[j];
				v[j] = v[j + 1];
				v[j + 1] = tmp;
			}
		}
	}
}

int main()
{
	int fd[2];

	pipe(fd);

	int v[MAX];

	int id = fork();

	if (id < 0)
		printf("Errore creazione processo");
	
	if (id == 0) 
	{
		close(fd[0]);

		caricaVettore(v, MAX);
		stampaVettore(v, MAX, "VETTORE ORIGINALE");

		sleep(3); // Il processo padre quando esegue il metodo read(), attende finchè il figlio non ha finito di scrivere nel fd[1].
		
				  // Il figlio, dato che attende 3 secondi, il processo padre, che esegue prima il metodo read() rispetto al metodo write()

				  // del figlio, aspetta che questo finisca di eseguire il metodo write(), cioè dopo 3 secondi
		
		write(fd[1], v, sizeof(v));

		close(fd[1]);
	} else 
	{
		close(fd[1]);

		read(fd[0], v, sizeof(v));

    	bubbleSort(v, MAX);
    	stampaVettore(v, MAX, "VETTORE ORDINATO");

		close(fd[0]);
	}

	wait(NULL);
}
