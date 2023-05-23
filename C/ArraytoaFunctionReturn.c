#include <stdio.h>

/* function to modify array */
// int *modifyArray(int arr[], int n) {
//     for (int i = 0; i < n; i++) {
//         arr[i] = arr[i] * 2;
//     }
//     return arr;
// }

int main() {
    int myArray[5];
    int size = sizeof(myArray) / sizeof(myArray[0]);

    // prompt numbers
    for(int i=0; i<size; i++){
        printf("Insert a number: ");
        scanf("%d", &myArray[i]);
    }
    
    /* print original array */
    printf("Original array: ");
    for (int i = 0; i < size; i++) {
        printf("%d ", myArray[i]);
    }
    printf("\n");

    /* call function to modify array */
    // int *modifiedArray = modifyArray(myArray, size);

    /* print modified array */
    printf("Modified array: ");
    for (int i = 0; i < size; i++) {
        printf("%d ", myArray[i]);
    }
    printf("\n");

    return 0;
}
