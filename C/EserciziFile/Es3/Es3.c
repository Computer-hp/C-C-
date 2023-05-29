/* Si realizzi un programma che costruisca un file binario di nome "anagrafe" contenente le informazioni 
relative ai cittadini di un comune italiano.
Ogni record del file "anagrafe" rappresenta una persona abitante nel comune e contiene le seguenti informazioni:
Nome: una stringa che rappresenta il nome della persona;
Cognome: una stringa che rappresenta il cognome della persona;
Data di nascita: un insieme di tre interi che rappresenta la data di nascita;
Luogo di nascita: una stringa che rappresenta la località di nascita;
Indirizzo: una stringa che rappresenta l'indirizzo della persona;
Codice Fiscale: una sequenza di 16 caratteri che individua univocamente il cittadino.
Il programma dovrà riempire il file "anagrafe" con informazioni immesse dall'utente attraverso 
lo standard input e, successivamente, visualizzarne il contenuto. */

#include <stdio.h>

struct cittadino
{
    char nome[50];
    char cognome[50];
    char l_nascita[50];
    char indirizzo[80];
    char CF[16];
};

struct cittadino creaCittadino()
{
    // definisco lo struct nel quale salverò i dati del cittadino
    struct cittadino c;
    // Nome
    printf("Nome: ");
    scanf("%s", c.nome);
    // Cognome
    printf("Cognome: ");
    scanf("%s", c.cognome);
    // Luogo di nascita
    printf("Luogo di nascita: ");
    scanf("%s", c.l_nascita);
    // Indirizzo
    printf("Indirizzo: ");
    scanf("%s", c.indirizzo);
    // Codice Fiscale
    printf("Codice Fiscale: ");
    scanf("%s", c.CF);

    // faccio ritornare lo struct c
    return c;
}

void stampaCittadino (struct cittadino c)
{
    printf("%s\t", c.nome);
    printf("%s\t", c.cognome);
    printf("%s\t", c.l_nascita);
    printf("%s\t", c.indirizzo);
    printf("%s\n", c.CF);
}

int main()
{
    FILE *f;
    char file[] = "C:\\Users\\AnticHp\\Desktop\\AnticGit\\DanielA\\C\\EserciziFile\\Es3\\anagrafe.dat";
    // creazione file nella cartella Es3
    f = fopen(file, "w+");
    
    if (f == NULL) {
        printf("Errore durante l'apertura del file\n");
        return 1;
    }

    int state = 1;
    struct cittadino c;

    do {
        c = creaCittadino();
        fwrite(&c, sizeof(struct cittadino), 1, f);
        stampaCittadino(c);
        printf("\nVuoi inserire informazioni su un altro cittadino (0 - 1): ");
        scanf("%d", &state);
    } while(state == 1);

    fclose(f);

    return 0;
}