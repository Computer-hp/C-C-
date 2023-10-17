#include <stdio.h>
#include <stdlib.h>

void ordine(int n, int **numeri, int l, int length, int counter, int matrixcounter, int volte){
    if(n > 0){
    volte = counter-n;
    // prima riga
    for(int i = matrixcounter-l; i < length-volte; i++){
        numeri[volte][i] = n;
    }
    // prima colonna
    for(int j = matrixcounter - l; j < length - volte; j++){
        numeri[j][volte] = n;
    }
    // ultima riga
    for(int i = l - 1; i > matrixcounter - l; i--){
        numeri[l-1][i] = n;
        }
    // ultima colonna
    for(int j = l - 1; j > matrixcounter - l; j--){
        numeri[j][l-1] = n;
    }
    ordine(n - 1, numeri, l - 1, length, counter, matrixcounter, volte);
    }
    return;
}


int main(){
    int n;
    printf("Inserisci lungehzza righe & colonne matrice: ");
    scanf("%d",&n);
    int l = 2*n-1;
    int **numeri = (int **)malloc(l*sizeof(int *));

   for(int i = 0; i<l; i++){
     numeri[i] = (int *)malloc(l*sizeof(int));
   }

    int length = l;
    printf("Lunghezza righe e colonne matrice: %d\n", length);

    int counter = n;
    int matrixcounter = l;
    int volte = 0;
    ordine(n, numeri, l, length, counter, matrixcounter, volte);

    // stampa
       for(int i=0; i<l; i++){
            for(int j=0; j<l; j++){
            printf("%d ",numeri[i][j]);
        }
        printf("\n");
        }
    
    // free malloc
    for (int i = 0; i < l; i++) {
        free(numeri[i]);
    }
    free(numeri);

    return 0;
}

