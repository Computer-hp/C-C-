#include <stdio.h>
#include <unistd.h>
#include <sys/wait.h>

#define MAXCHAR 100

int main()
{
    int fd[2];  // 0 per 'read', 1 per 'write'

    pipe(fd);

    int id = fork();

    if (id < 0)
        return -1;

    if (id == 0)
    {
        close(fd[0]);
        char str[MAXCHAR];
        
        printf("Inserisci un messaggio ");
        scanf("%s", str);

        write(fd[1], str, sizeof(char) * 100);
        close(fd[1]);
        
    } 
    else {
        close(fd[1]);
        char str[MAXCHAR];
        read(fd[0], str, sizeof(str));
        printf("Dal processo padre\n Il figlio mi ha inviato questo dato: %s", str);
        close(fd[0]);

    }

    return 0;
}