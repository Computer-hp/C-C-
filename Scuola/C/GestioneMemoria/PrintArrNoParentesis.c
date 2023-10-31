/* Stampare una Matrice senza usare le parentesi */
#include <stdio.h>
#include <stdlib.h>

void input(int *p, int i, int l){
    if (i < l){
    printf("Inserisci un numero ");
    scanf("%d", p);
    input(p + 1, i + 1, l);
    }
    return;
}

void print(int *p, int i, int l){
    if (i < l){
    printf("Arr pos %d: %d\n", i, *p);
    print(p + 1, i + 1, l);
    }
    return;
}

int main(){
    int l;
    printf("Inserisci la lunghezza dell'array ");
    scanf("%d", &l);
    // int **matrix = (int **)malloc(l*sizeof(int *));
    int *arr = (int *)malloc(l*sizeof(int));
    // int arr[l];
    int i = 0;
    input(arr, i, l);
    print(arr, i, l);

    // empty(arr);
    free(arr);

    return 0;
}