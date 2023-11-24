/*
Leggere un array di interi con 8 numeri e dire quali e quanti di questi sono primi.
*/
#include <iostream>
using namespace std;

int main(){
int numeri[8];
int i;
int numeriPrimi[8];
int quantP = 0;
int length = end(numeri)-begin(numeri);
int divisore = 2;
int j=0;

for (i=0; i<length; i++){
cout<<"Inserisci un numero: ";
cin>>numeri[i];
}
// riempio l'array di 0
for (i=0; i<length; i++){
numeriPrimi[i] = 0;
}

for (i = 0; i < length; i++){
    if (numeri[i]%divisore != 0){
        if (i-1 < 0){
        quantP++;
        numeriPrimi[j]=numeri[i];
        j++;
    }   else {
            if (numeri[i-1] != numeri[i]){
            quantP++;
            numeriPrimi[j]=numeri[i];
            j++;
            }
        }
    }  
      
}
cout<<"La quantita' di numeri primi che hai inserito e': "<<quantP;
j=0;
cout<<"\nI nuemri primi che hai inserito sono: ";
while ( j<length && numeriPrimi[j] != 0){
cout<<"\n"<<numeriPrimi[j];
j++;
}
return 0;

}
