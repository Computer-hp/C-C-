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

int getK1(vector<int> &W, int N)
{
    int sum = 0, position = 0, j = 0;

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
    }
    return sum;
}

int getRowSum(vector<int> &W, int &position, int N)
{
    int row_sum = 0, i, spaces = 0;

    for (i = position; i < N; i++)
    {
        if (W[i] == -1)
            break;
        
        row_sum += W[i];
        spaces++;
    }
    position = i + 1;
    
    return (row_sum + W[i + 1] + spaces - 1);
}

int getK2(vector<int> &W, int K2, int counter, int last_aCapo)
{
    int row_sum, main_position = 0, start = 0;

    while (main_position < last_aCapo)
    {
        row_sum = getRowSum(W, main_position, last_aCapo);

        int compare_sum = 0, compare_position = 0, counter2 = 0;

        while (counter2 < counter)
        {
            compare_sum = getRowSum(W, compare_position, last_aCapo);

            if (row_sum >= compare_sum && K2 >= compare_sum)
                K2 = compare_sum;
        
            else if (compare_sum >= row_sum && K2 >= row_sum)
                K2 = row_sum;

            counter2++;
        }
    }
    return K2;
}

void solve(int t) {
    
    int N;
    cin >> N;

    vector<int> W(N);

    for (int i = 0; i < N; i++) 
    {
        cin >> W[i];
    }

    int K1 = getK1(W, N);

    int last_aCapo = 0, counter = 0;

    for (int i = N; i > 0; i--)
    {
        if (W[i] == -1)
        {
            last_aCapo = i;
            break;
        }
    }

    for (int i = 0; i < N; i++)
    {
        if (W[i] == -1)
            counter++;
    }

    int start = 0;
    int K2 = getRowSum(W, start, last_aCapo);

    if (counter != 1)
        K2 = getK2(W, K2, counter, last_aCapo);

    cout << "Case #" << t << ": " << K1 << " " << K2 << "\n";
}

int main() {

    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);

    int T;
    cin >> T;

    for (int t = 1; t <= T; t++) 
    {
        solve(t);
    }

    return 0;
}
