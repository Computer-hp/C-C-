let n = prompt("Inserisci un numero" );
let a = [0,1,2,3,4,5,6,7,8,9];
let strings = [{
    0: "zero",
    1: "uno",
    2: "due",
    3: "tre",
    4: "quattro",
    5: "cinque",
    6: "sei",
    7: "sette",
    8: "otto",
    9: "nove",
},
{
    10: "dieci",
    11: "undici",
    12: "dodici",
    13: "tredici",
    14: "quattordici",
    15: "quindici",
    16: "sedici",
    17: "diciassette",
    18: "diciotto",
    19: "dicianove",
    20: "venti",
    30: "trenta",
    40: "quaranta",
    50: "cinquanta",
    60: "sessanta",
    70: "settanta",
    80: "ottanta",
    90: "novanta",
},
{
    100: "cento",
},
{
    1000: "mille",
},
{
    10000: "mila",
},
{
    1000000: "milione"
}];
let nStringa = "";

tostring(n,strings,nStringa);

function tostring(n,strings,nStringa){
let counter = 0;
let l = 0;
let counter2 = 0;
counter2 =  counterNumbers(n,counter,l);
if ( counter2 != 0){
let key = Number(counterNumbers(n,counter,l));
nStringa = nStringa + strings[counter2-1][key];
toString(n,strings,nStringa);
}
}
function counterNumbers(n,counter,l){
    if (counter == n.length)
        return counter;
    else
        counter++;
        counterNumbers(n,counter,l+1);
}