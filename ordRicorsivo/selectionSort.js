


function selectionSort(numeri, inizio, fine){
    let posMinimo = 0;
if (inizio === fine){
    return;
} else{
    posMinimo = posizioneMinimo(numeri, inizio, fine);
    // alert(posMinimo);
    scambia(numeri, inizio, posMinimo);
    for (let i=0; i< numeri.length; i++){
        console.log(numeri[i]);
    }
    selectionSort(numeri, inizio+1, fine);
}
}



function posizioneMinimo(numeri, inizio, fine){
    // alert("inizio1 "+inizio);
if (inizio === fine)
    return inizio;
else{
    let posMinimoRestante = posizioneMinimo(numeri, inizio +1, fine); 
    // alert("inizio2 "+inizio);
    // alert(posMinimoRestante);
    if(numeri[inizio] < numeri[posMinimoRestante]){
        return inizio;
    }
    else
        return posMinimoRestante;
}
}

function scambia(numeri, inizio, posMinimo){
    let tmp = numeri[posMinimo];
    // alert(numeri[inizio]);
    numeri[posMinimo] = numeri[inizio];
    
    numeri[inizio] = tmp;
    // alert(numeri[inizio]);
    return;

}

let numeri = [];
let inizio = 0;
// riempio l'array
for (let i=0; i<100; i++){
    numeri[i] = Math.floor(Math.random() * 100000); 
    
}
let fine = numeri.length -1;

//document.write("Orginal array:" + numeri + "<br/>");
const dStart = new Date(); 
selectionSort(numeri, 0, numeri.length -1);
const dEnd = new Date(); 
let durataSelection = dEnd.getTime() - dStart.getTime();

//document.write("Sorted array (Selection Sort):"+ numeri + "<br/>");
document.write("Impiegato: "+ durataSelection + " milliseconds <br/>");