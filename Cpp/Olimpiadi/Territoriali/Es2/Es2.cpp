#include <iostream>
#include <string>
#include <vector>

using namespace std;

/*
N = lunghezza parole e numero parole con a capo (-1)

K = numero caratteri per andare a capo

Trovare Kmin e Kmax

almeno un a capo !!!

almeno due righe



prima trovo la somma della prima riga prima di -1
poi la salvo in sum
salvo la parola dopo il -1

faccio la somma della seconda riga
salvo in priviosory_sum
uso o salvo la parola dopo il -1

comparo la somma con la provisory_sum


*/

int getK2(vector<int> &W, int j, int N, int provisory_sum, int aCapo)
{
    if (j < N - 1)
        if (aCapo <= W[j])
            return provisory_sum + W[j];

    return provisory_sum + aCapo;
}

void solve(int t) {
    int N;
    cin >> N;

    vector<int> W(N);

    int K1 = 0, K2 = 0, provisory_K2 = -1;

    // metto i numeri in ciascuna cella dell array
    for (int i = 0; i < N; i++) {
        cin >> W[i];
    }

    // aggiungi codice...
    
    int aCapo = 0;

    int provisory_sum = 0;

    int j = 0;

    while (j < N){

       int sum = 0, counter = -1;

        for (int i = j; W[i] != -1 && i < N; i++)
        {
            sum += W[i];
            j++;
            counter++;
        }
        sum += counter;

        cout << "\nSomma: " << sum << "\n";

        if (j < N - 1)
            j += 1; // to skip the -1 cell

        if (provisory_K2 != -1)
        {
            K2 = getK2(W, j, N, provisory_sum, aCapo);


            if (sum < provisory_sum && K1 < provisory_sum)
                K1 = provisory_sum;
            else if (provisory_sum < sum && K1 < sum)
                K1 = sum;
        }
        else
            provisory_K2 = K2;

        provisory_sum = sum;

        aCapo = W[j];  // prendo la parola a capo
    }


    cout << "Case #" << t << ": " << K1 << " " << K2 << "\n";
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
