#include <stdio.h>
#include <stdlib.h>

void firstCell(int** first, int l, int i){
    if (i < l){
        *(*first + i) = 1;
        firstCell(first, l, i + 1);
    }
    return;
}

void fillMatrix(int** matrix, int l, int i) {
    if (i < l){
    **matrix = 1;
    firstCell(matrix, l, i);
    fillMatrix(matrix + 1, l, i + 1);
    }
    return;
}

int main() {
    int l = 2, n = 5;
    // printf("Enter the length of the matrix: ");
    // scanf("%d", &l);

    int** matrix = (int**)malloc(l * sizeof(int*));

    for (int i = 0; i < l; i++) {
        matrix[i] = (int*)malloc(l * sizeof(int));
    }

    // printf("Enter the value to fill the matrix with: ");
    // scanf("%d", &n);

    for (int i = 0; i < l; i++) {
        for (int j = 0; j < l; j++) {
            matrix[i][j] = n;
        }
    }
    int i = 0;
    fillMatrix(matrix, l, i);

    // Print the matrix
    for (int i = 0; i < l; i++) {
        for (int j = 0; j < l; j++) {
            printf("%d ", matrix[i][j]);
        }
        printf("\n");
    }

    // Free the memory
    for (int i = 0; i < l; i++) {
        free(matrix[i]);
    }
    free(matrix);

    return 0;
}





