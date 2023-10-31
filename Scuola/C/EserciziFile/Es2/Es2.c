/* 
Siano dati due file contenenti testo. Si realizzi un programma che individui e scriva 
un terzo file "risultati.txt",, nel file verranno sritte le parole che compaiono 
in entrambi i file (stessa parola che si ripete va scritta una volta).
 */

#include <stdio.h>
#include <string.h>
#define MAX 100

int main()
{
    FILE *f1, *f2, *f3;

    char stringFile1[MAX], stringFile2[MAX];

    f1 = fopen("compare1.txt", "r");
    f2 = fopen("compare2.txt", "r");
    f3 = fopen("result.txt", "w");

    int state = 0;

    while (fscanf(f1, "%s", stringFile1) > 0)
    {
        // The rewind() function sets the file pointer at the beginning of the stream.
        rewind(f2);

        while (fscanf(f2, "%s", stringFile2) > 0 && !state)
        {
            if (strcmp(stringFile1, stringFile2) == 0){
                state = 1;
                fprintf(f3, "%s ", stringFile1);
            }
        }
    }
    fclose(f1);
    fclose(f2);
    fclose(f3);

    return 0;
}