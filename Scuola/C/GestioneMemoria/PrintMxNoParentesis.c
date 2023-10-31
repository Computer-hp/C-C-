#include <stdio.h>
#include <stdlib.h>
    
void inputCelle(int **p, int j, int l, int n){
    if (j < l){
    // printf("Inserisci un numero ");
    // scanf("%d", p);
    *(*p + j) = n;
    inputCelle(p, j + 1, l, n);
    }
    
    return;
}

void input(int **p, int i, int l, int n){
    if (i < l){
    // printf("Inserisci un numero ");
    // scanf("%d", p);
    int j = 0;
    inputCelle(p, j, l, n);
    input(p + 1, i + 1, l, n);
    }
    return;
}

void printCelle(int **p, int j, int l, int n){
    if (j < l){
    printf("%d ", *(*p + j));
    printCelle(p, j + 1, l, n);
    }
    return;
}

void print(int **p, int i, int l, int n){
    printf("\n");
    if (i < l){
    // printf("Arr pos %d: %d\n", i, *p);
    int j = 0;
    printCelle(p, j, l, n);
    print(p + 1, i + 1, l, n);
    }
    return;
}

int main(){
    int l, n;
    printf("Inserisci la lunghezza dell'array ");
    scanf("%d", &l);

    printf("\nInserisci il numero con cui vuoi riempire la matrice ");
    scanf("%d", &n);

    // int **matrix = (int **)malloc(l*sizeof(int *));
    int **arr = (int **)malloc(l*sizeof(int *));
    for (int i = 0; i < l; i++){
        arr[i] = (int *)malloc(l*sizeof(int));
    }
    
    int i = 0;
    input(arr, i, l, n);
    print(arr, i, l, n);

    // empty(arr);
    for (int i = 0; i < l; i++){
        free(arr[i]);
    }
    free(arr);


    return 0;
}