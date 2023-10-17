#include <stdio.h>
#include <stdlib.h>

void fillMatrix(int** matrix, int l, int n) {
    int i, j;
    for (i = 0; i < l; i++) {
        for (j = 0; j < l; j++) {
            matrix[i][j] = n;
        }
    }
}

int main() {
    int l, n;
    printf("Enter the length of the matrix: ");
    scanf("%d", &l);

    int** matrix = (int**)malloc(l * sizeof(int*));
    int i;
    for (i = 0; i < l; i++) {
        matrix[i] = (int*)malloc(l * sizeof(int));
    }

    printf("Enter the value to fill the matrix with: ");
    scanf("%d", &n);

    fillMatrix(matrix, l, n);

    // Print the matrix
    int j;
    for (i = 0; i < l; i++) {
        for (j = 0; j < l; j++) {
            printf("%d ", matrix[i][j]);
        }
        printf("\n");
    }

    // Free the memory
    for (i = 0; i < l; i++) {
        free(matrix[i]);
    }
    free(matrix);

    return 0;
}
/*
Here, we first take in the length of the matrix from the user using scanf, 
and allocate memory for the matrix using malloc. We then take in the value n 
that the user wants to fill the matrix with.
The fillMatrix function takes in the matrix, l, and n as arguments, 
and uses a nested for loop to iterate over each element in the matrix and fill it with n.
Finally, we print the filled matrix, and then free the allocated memory using free.
*/





