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

/*int getK2(vector<int> &W, int j, int N, int provisory_sum, int aCapo)
{
    if (j < N - 1)
        if (aCapo <= W[j])
            return provisory_sum + W[j];

    return provisory_sum + aCapo;
}*/

void solve(int t) {
    
    int N;
    cin >> N;

    vector<int> W(N);

    int K1 = 0, K2 = 0;

    // metto i numeri in ciascuna cella dell array
    for (int i = 0; i < N; i++) 
    {
        cin >> W[i];
    }

    // aggiungi codice...

    int SUM = 0, position = 0;


    // finds the value of K_min or K1
    int j;

    while (j < N)
    {
        int provisory_sum = 0;
        int spaces = 0;

        for (j = position; j < N; j++)
        {
            if (W[j] == -1)
            {
                position = j + 1;
                break;
            }
            provisory_sum += W[j];
            spaces++;
        }
        provisory_sum += spaces - 1;

        if (provisory_sum > SUM)
            SUM = provisory_sum;
        
        cout << "Spaces: " << spaces - 1 << "\n";
    }

    // K_MIN
    K1 = SUM;

    int save_first_sum = -1, provisory_sum = 0, spaces = 0;

    for (int i = 0; i < N; i++)
    {
        if (W[i] != -1)
        {
            provisory_sum += W[i];
            spaces++;
        }
        else
        {
            provisory_sum += (W[i + 1] - 1) + spaces - 1; // word after the -1 + spaces that were counter before
            spaces = -1;

            // if there isn't saved the sum of the row before -1
            if (save_first_sum == -1)
                save_first_sum = provisory_sum;
            else
            {
                if (save_first_sum < provisory_sum && K2 < provisory_sum)
                    K2 = provisory_sum;

                if (provisory_sum < save_first_sum && K2 < save_first_sum)
                    K2 = save_first_sum;

                save_first_sum = -1;
            }

            provisory_sum = 0;

        }
    }
    

    if (K2 < provisory_sum && save_first_sum < provisory_sum)
        K2 = provisory_sum + spaces - 1;

    if (provisory_sum < save_first_sum && K2 < save_first_sum)
        K2 = save_first_sum + spaces - 1;

    /*int aCapo = 0;

    int provisory_sum = 0;

    int j = 0;

    while (j < N){

       int sum = 0, counter = -1;

        for (int i = j; W[i] != -1 && i < N; i++)
        {
            sum += W[i];
            j++;
            counter++; // spazi in mezzo ai caratteri
        }
        sum += counter;

        if (j < N - 1)
            j += 1; // to skip the -1 cell

        if (provisory_K2 != -1)
        {
            K2 = getK2(W, j, N, provisory_sum, aCapo);

            //
            if (sum < provisory_sum && K1 < provisory_sum)
                K1 = provisory_sum;
            else if (provisory_sum < sum && K1 < sum)
                K1 = sum;
        }
        else
            provisory_K2 = K2;

        provisory_sum = sum;

        aCapo = W[j];  // prendo la parola a capo
    }*/


    cout << "Case #" << t << ": " << K1 << " " << K2 << "\n";
}

int main() {

    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);

    int T;
    cin >> T;

    for (int t = 1; t <= T; t++) {
        solve(t);
    }

    return 0;
}
