/*
Si scriva un programma in linguaggio C che, data una matrice quadrata NxN, stampi
gli elementi di tale matrice secondo un ordinamento a spirale, partendo dalla cornice più
esterna e procedendo verso l’interno

*****
 ** *
 ** *   
 *  *
 ****
*/
#include <stdio.h>
#include <stdlib.h>

void fill(char **matrix, int volte, int l, int rip, int end)
{
    if (volte == l - 1)
        return;
    else
    {
        // x
        if (rip % 2 == 0 && rip != 0){
            l -= 1;
        
        for (int i = l - end; i < l; i++)
        {
            if (volte % 2 == 0)
            matrix[volte][i] = "*";
        }
        // y
        for (int i = l - end; i < l; i++)
        {
            if (volte % 2 == 0)
            matrix[i][l] = "*";
        }

        for (int i = l - 1; i > 0; i--)
        {
            if (volte % 2 == 0)
            matrix[volte][i] = "*";
        }

        fill(matrix, volte + 1, l, rip + 1, end -1);
        }
        else {
            fill(matrix, volte + 1, l, rip + 1, end - 1);
        }
    }

    return;
}

int main()
{
    int l, carattere = 0;
    printf("Inserisci lunghezza lato: ");
    scanf("%i", &l);
    char **matrix = (char**)malloc((l+1)*sizeof(char*));

    for (int i = 0; i < l; i++){
        matrix[i] = (char*)malloc((l+1)*sizeof(char));
    }
    /*
    prima riga full, ultima colonna full,
    ultima riga tranne prima cella full
    si sale su
    */
    int volte = 0;
    fill(matrix, volte, l, 0, l);

    return 0;
}