#include <iostream>
#include <string>
#include <vector>

using namespace std;

/*
N = lunghezza parole e numero parole con a capo (-1)

K = numero caratteri per andare a capo

Trovare Kmin e Kmax



prima trovo la somma della prima riga prima di -1
poi la salvo in sum
salvo la parola dopo il -1

faccio la somma della seconda riga
salvo in priviosory_sum
uso o salvo la parola dopo il -1

comparo la somma con la provisory_sum


*/

void solve(int t) {
    int N;
    cin >> N;

    vector<int> W(N);

    bool firstLastWord = false;

    int provisory_aCapo = 0, aCapo = 0;

    // metto i numeri in ciascuna cella dell array
    for (int i = 0; i < N; i++) {
        cin >> W[i];
    
        /*if (W[i] == -1 && i + 1 < N && firstLastWord && W[i + 1] != 0)
        {
            aCapo = W[i + 1];
            firstLastWord = false;
        }*/
    }

    // aggiungi codice...
    

    int K1, K2, provisory_K1 = 0;
    int sum = 0, provisory_sum = 0;

    int j = 0;

    int provisory_K2 = 0;

    while (j < N){

        // somma riga
        for (int i = j; W[i] != -1; i++)
        {
            sum += W[i] + 1;
            j++;
        }
/*
if provisory_sum <= sum :
    if primaParolaACapo <= seconda :
        allora K2 = provisory_sum + seconda

    elif primaParolaACapo > seconda :
        provisory_sum + primaParolaACapo


else :
    if primaParolaACapo < seconda :
        allora K2 = provisory_sum + seconda

    elif primaParolaACapo > seconda :
        provisory_sum + primaParolaACapo
*/
        j += 2; // to skip the -1 cell

        if (provisory_K2 != NULL)
        {
            //if (provisory_sum <= sum)
            K2 = getK2(W, j, provisory_sum, aCapo);
        }
        else
            provisory_K2 == K2;

        provisory_sum = sum;

        aCapo = W[j];  // prendo la parola a capo
    }


    cout << "Case #" << t << ": " << K1 << " " << K2 << "\n";
}

int getK2(vector<int> W, int j, int provisory_sum, int aCapo)
{
    if (aCapo <= W[j])
        return provisory_sum + W[j];
    else
        return provisory_sum + aCapo;
}

int main() {
    // se preferisci leggere e scrivere da file
    // ti basta decommentare le seguenti due righe:

    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);

    int T;
    cin >> T;

    for (int t = 1; t <= T; t++) {
        solve(t);
    }

    return 0;
}
