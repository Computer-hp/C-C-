#include <stdio.h>
#include <string.h>
#include <unistd.h>
#include <sys/wait.h>

#define MAXASCII 255

int main()
{
    FILE *fp;

    char parole[30];

    int numerParole = 0;

    int v[MAXASCII];

    int fd[2];

    int pid, status;

    fp = fopen("Mattarella.txt", "r");

    if (!fp)
    {
        printf("\nERRORE di apertura file txt");
        return -1;
    }

    if (pipe(fd) < 0)
    {
        printf("\nERRORE di creazione pipe");
        return -1;
    }

    pid = fork();

    if (pid < 0)
    {
        printf("\nERRORE di creazione dei processi");
        return -1;
    } 
    if (pid > 0)
    {
        close(fd[0]);

        do {
            char parola[0] = "\0";
            fscanf(fp, "%s", parola);
            write(fd[1], parola, strlen(parola));

            numerParole++;
            printf("parole n. %i : %s\n", ++numerParole, parola);

        } while (!feof(fp));

        fclose(fp);
        close(fd[1]);
        wait(&status);

        
        // sono nel processo padre
        /*
        lettura del 
        a) chiusura della pipe fd[0];
        b) ciclo sul file per la lettura parola x parola
        c) scrittura nelle pipe di ogni parola letta
        d) conteggio delle parole scritte nelle pipe (2065) +- 1
        */

    } else { // pid == 0
        // sono nel processo figlio
        /*
        a) chiusura della pipe fd[0];
        b) ciclo di lettura della pipe (read(...))
        c) conteggio delle lettere per ogni parola letta dalla pipe --> uso del vettore v[255]
        d) stampa dei conteggi A....Z, a......z
        */
    }

    pid = fork();
}