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

int getRowSum(vector<int> &W, int *position, int N)
{
    int row_sum = 0, i, spaces = 0;

    for (i = *position; i < N; i++)
    {
        if (W[i] == -1)
        {
            *position = i + 1;
            break;
        }

        row_sum += W[i];
        spaces++;
    }

    if (i + 1 < N)
        return row_sum + W[i + 1] + spaces - 1;
    
    return row_sum + spaces - 1;
}

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

    int sum = 0, position = 0, j = 0;

    // finds the value of K_min or K1

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

        if (provisory_sum > sum)
            sum = provisory_sum;
        
        cout << "\nSpaces: " << spaces - 1 << "\n";
        cout << "\nprovisory_sum: " << provisory_sum << "\n";
    }

    // K_MIN
    K1 = sum;

    int row_sum, main_position = 0, save_main_position = -1;

    while (main_position != save_main_position)
    {
        save_main_position = main_position;

        row_sum = getRowSum(W, &main_position, N);

        int compare_sum, compare_position = 0;

        for (int j = 0; j < N; j++)
        {
            compare_sum = getRowSum(W, &compare_position, N);

            /*K2 = (row_sum < compare_sum && K2 < compare_sum) ? compare_sum //: row_sum;

            K2 = (compare_sum < row_sum && K2 < row_sum) ? row_sum;*/

            if (row_sum < compare_sum && K2 < compare_sum)
                K2 = compare_sum;
            else if (compare_sum < row_sum && K2 < row_sum)
                K2 = row_sum;
        }
    }


    cout << "\nCase #" << t << ": " << K1 << " " << K2 << "\n";
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
