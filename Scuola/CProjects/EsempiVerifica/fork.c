#include <stdio.h>
#include <unistd.h>
#include <sys/wait.h>

int main()
{
    int id = fork();

    if (id < 0)
    {
        printf("Errore creazione processo");
        return -1;
    }
        
    if (id == 0)
    {   
        printf("\nCiao sono il figlio, l'id = %d mentre il getpid() = %d\n", id, getpid());
    } else
    {
        sleep(3);
        printf("\nCiao sono il padre, l'id = %d mentre il getpid() = %d\n", id, getpid());
    }

    wait(NULL);

    return 0;
}