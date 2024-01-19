#include <iostream>
#include <string>
#include <vector>

using namespace std;

/*
    N   lunghezza muro

    Q   numero colori

    L   n° metri che ciascun colore può occupare (ordine primo ultimo)


*/

int get_sum_of_colors_behind(vector<int> &v, int l)
{
    int sum = 0;

    for (int i = 0; i < l; i++)
    {
        sum += v[i];
    }

    return sum;
}


void solve(int t) 
{
    int N, Q;
    cin >> N >> Q;    //   N  lunghezza muro,  Q   numero colori

    vector<int> L(Q);
	int start = 0;

    for (int i = 0; i < Q; i++) 
    {
        cin >> L[i];

		if (L[i] == N)
            start = i;
    }

    // aggiungi codice...


    vector<int> used_colors;
    used_colors.push_back(L[start]);


    for (int i = start + 1; i < Q; i++)
    {
        int white_spaces = N - get_sum_of_colors_behind(used_colors, i);

        if (white_spaces >= L[i])
        {
            used_colors.push_back(L[i]);
            continue;
        }

		int j = used_colors.size();
        int r = -(white_spaces - L[i]);

        cout << "\n r = " << r << endl;

        while (j - 1 >= 0)   // non sono sicuro di questa condizione
        {
			used_colors[j - 1] -= r;

			if (used_colors[j - 1] == 0)
			{
				used_colors[j - 1] = L[i];
				break;
			}

			if (used_colors[j - 1] < 0)
			{
				r = used_colors[j - 1];      // salvo il nuovo resto da togliere al numero ancora prima
				used_colors.pop_back();  	 // rimuove l'elemento che non si vede più dal nuovo colore
				j--;
			}
			else
			{
				used_colors.push_back(L[i]);
				break;
			}

        }


    }


	int result = 0;

	for (int i = 0; i < used_colors.size(); i++)
	{
		result++;

		//cout << "number " << used_colors[i] << endl;
	}

    cout << "Case #" << t << ": " << result << endl;
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