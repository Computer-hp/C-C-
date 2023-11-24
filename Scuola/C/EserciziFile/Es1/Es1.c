/* 
Dato un file F, si realizzi  un programma che, acquisita una parola P da standard input, 
ricerchi P all'interno del file dato e stampi sul dispositivo standard di output il numero 
delle occorrenze di P nel file F.
 */
#include <stdio.h>
#include <string.h>

int main()
{
    FILE *f;

    // contatore occorrenze
    int counter = 0;
    // array stringhe in cui salvo stringa in input
    char input_string[100], file_content[100]; // array stringhe in cui verrà salvato il contenuto del file F.txt

    /*
    per creare un file
    f = fopen("C:\\Users\\AnticHp\\Desktop\\AnticGit\\DanielA\\C\\EserciziFile\\Es1\\F.txt")", "r");
    */

    // apro il file
    f = fopen("F.txt", "r");

    if (f == NULL){
        printf("Il file non e' stato aperto.");
        return 1;
    }

    printf("Inserisci una parola: ");
    scanf("%s", input_string);

    // salvo la prima stringa del file F.txt nella stringa file_content
    fscanf(f, "%s", file_content);

    // finche' il while non giunge alla fine del file, EOF --> siccome appartiene al file diventa feof()
    while (!feof(f))
    {
        if (strcmp(input_string, file_content) == 0){
            printf("\nHo trovato %s", input_string);
            counter++;
        }
        // continuo a salvare le stringhe del file nel char file_content
        fscanf(f, "%s", file_content);
    }
    
    printf("\nTrovata %s %d volte \n", input_string, counter);

    fclose(f);

    return 0;
}