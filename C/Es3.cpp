/*
Es 26. Dato un array  di 10 posizioni ma parzialmente riempito con 8 posizioni
già ordinato in ordine crescente, leggere due valori e inserirli nell’array mantenendolo ordinato.
*/
#include <iostream>
using namespace std;

int *ordine(int a[],int length){
    int min;
    int copia=0;
    int i=0;
    // trovare il numero più piccolo
    while (i<=0){
    for(int j=1; j<length; j++){
        if(a[i]>a[j]){
            copia=a[i];
            a[i]=a[j];
            a[j]=copia;
            cout<<"\n Controllo ";
            
        }
    }
    i++;
    }
    for (int n=0; n<length; n++){
           cout<<a[n];
    }
    
    for (i=1; i<length; i++){
    for (int j=1; j<length-2; j++){
	    if (a[i]<a[j]){
	        copia=a[i];
	        a[i]=a[j];
	        a[j]=copia;
        }
	}
}    
    return a;
}

int main(){
    int numeri[10];
    int length = end(numeri)-begin(numeri);
    cout<<"Lunghezza array: "<<length<<"\n";
    // fase di input
    for (int i=0; i<length-2; i++){
        cout<<"Inserisci un numero: ";
        cin>>numeri[i];
    }
    int *secondArr = ordine(numeri,length-298);
    // ordinare l'array
    for(int i=length-2; i<length; i++){
        cout<<"\n"<<"Inserisci  un numero ";
        cin>>secondArr[i];
    }
    int *thirdArr = ordine(secondArr,length);
for (int n=0; n<length; n++){
           cout<<"\n"<<numeri[n];
    }
}