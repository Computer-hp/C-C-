#include <stdio.h>
#include <string.h>

int main()
{
    char stringa[10];
    scanf("%s", stringa);
    // length of the string
    int length = strlen(stringa);
    printf("Lunghezza stringa: %d\n", length); // \n ---> per andare a capo
    int counter = 0;
    
    int counterMax = 0;

    char letterMax;
    char letter;

    for (int i=0; i<length; i++){
        counter = 0;
        for (int j=0; j<length; j++){
            if (stringa[i] == stringa[j]){
                counter++;
                letter = stringa[i];
                
            } else {
                letter = stringa[j];
                if (j-1 >= 0){
                if (stringa[j] != stringa[j-1] && stringa[j] != stringa[i]){   
                    counter = counterMax - 1;
                } else{
                    counter++;
                }
            }else{
                counter++;
            }
            if (counterMax < counter){
                counterMax = counter;
                letterMax = letter;
                printf(" %d ", counterMax);
                printf(" %c ", letterMax);
            }
        }
        }
}
    printf("Lettera maggiormente ripetuta: %c", letterMax);
    return 0;
}
// strcat(str1,str2)