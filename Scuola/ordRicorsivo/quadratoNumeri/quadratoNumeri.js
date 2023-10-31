/*
    dato un numero n(1<=n<=1000) stampare la scala di n-1 per formare un quadrato
    n=3
    33333
    32223
    32123
    32223
    33333
    
    2n-1

    prendo un numero, n = 4
    determino la lunghezza del lato 2*4-1=7
    faccio una funzione a che stampa la prima riga, 7 volte 4
    chiamo la funzione b che stampa ai lati il 4  e all'interno 4 volte 3
    chiamo la funzione c che stampa ai lati 4 ai lati dopo il 4 il 3 e all'interno 2
    chiamo la funzione d che stampa 4321234
    poi ritornando dalla funzione rifaccio questo al contrario
    
*/
// dichiarazione numero, lunghezza quadrato (l), array (numeri) di lunghezza l;
let n = Number(prompt("Inserisci un numero: "));
let l = n*2 -1
let numeri = new Array(l);
// creazione matrice tramite for, in ogni cella metto l'array di lunghezza l
for (let i=0; i<numeri.length; i++){
    numeri[i]=new Array(l);
}
// chiamo la funzione firstcol per trasportare l'array, il numero, la lunghezza
firstcol(numeri,n,l);

function firstcol(numeri,n,l){
/* esempio matrice:
0 0 0 0 i
0 
0
0
j
*/
/* dichiaro un contatore che servirà per stabilire il 
    valore di v (volte ---> servirà per riempire la riga in alto e la colonna a sinistra della matrice)
    
    0 0 0 0 i
    0 
    0
    0
    j
*/
let counter = n;
let matrixcounter = l;
let v = 0;
feel(numeri,n,counter,v,l,matrixcounter);
    // stampa
    for(let i=0; i<numeri.length; i++){
        
        for(let j=0; j<numeri.length; j++){
            print(numeri[j][i]);
        }
        document.write("<br>");
    }

}
function feel(numeri,n,counter,v,l,matrixcounter){
    if (n>0){
        v = counter-n;
        // serve per riempire la prima riga in alto, v è asse y  e  i è asse x
        for(let i=matrixcounter-l; i<numeri.length-v; i++){
            numeri[v][i] = n;
        }
        // serve per riempire la prima colonna a sinistra, j è asse y  e  v è asse x
        for(let j=matrixcounter-l; j<numeri.length-v; j++){
                numeri[j][v] = n;
        }
        // serve per riempire l'ultima riga, l-1 è asse y  e  i è asse x
        for(let i=l-1; i>matrixcounter-l; i--){
            numeri[l-1][i] = n;
        }
        // serve per riempire la prima colonna a destra, j è asse y  e  l-1 è asse x
        for(let j=l-1; j>matrixcounter-l; j--){
                numeri[j][l-1] = n;
        }
        /* richiamo la funzione feel per poter riempire la 2° riga e colonna:
            1° 1° 1° 1° 1°
            1° 2° 2° 2° 1°
            1° 2° 3° 2° 1°
            1° 2° 2° 2° 1°
            1° 1° 1° 1° 1°

            Poi si richiama la funzione finché n è > 0, perché bisogna ottenere al centro del quadrato il numero 1.
        */

        return feel(numeri,n-1,counter,v,l-1,matrixcounter);
    }else
        return numeri;

    }
    function print(numero){
        document.write(numero + " ");
    }