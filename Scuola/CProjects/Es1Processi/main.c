#include <stdio.h>
#include <sys/types.h>
#include <unistd.h>
#include <sys/wait.h>

/*
 Tutto il codice viene eseguito da due processi a partire dal fork(), per cui printf("End!")
 viene eseguito due volte.
 Fare più fork() e vedere quanti ne stampa
 */

int main() {
    // indirizzo del processo, lo divide in padre e figlio

    int id = fork();

    if (id < 0){
        printf("Errore di creazione processo\n");
        return -1;
    }

    if (id == 0) {
        sleep(3); // delay 5 secondi
        printf("\nCiao sono il figlio, l'id = %d mentre il getpid() = %d\n", id, getpid());
    }
    else
        printf("\nCiao sono il padre, l'id = %d mentre il getpid() = %d\n", id, getpid());

    // il processo padre aspetta che finisca il primo processo figlio.
    wait(NULL); // viene eseguito da tutti e due i processi.

    printf("\nEnd!");


    return 0;
}
