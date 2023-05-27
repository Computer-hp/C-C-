#include <stdio.h>

int main()
{
    // puntatore di tipo file
    FILE* f;
    // per stampare le righe del file .txt
    char riga[50];
    // per aprire il file
    f = fopen("prova.txt", "r");

    if (f == NULL){
        printf("Il file non è stato aperto correttamente.");
        return 1;
    }
    while (!feof(f))
    {
        fgets(riga, 50, f);
        printf("%s", riga);
    }
    

    // per chiudere il file
    fclose(f);
    return 0;
}