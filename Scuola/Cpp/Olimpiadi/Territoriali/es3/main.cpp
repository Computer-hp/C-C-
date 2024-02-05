#include <iostream>
#include <string>
#include <vector>
#include <fstream>

using namespace std;

/*
    N   lunghezza muro

    Q   numero colori

    L   n° metri che ciascun colore può occupare (ordine primo ultimo)


*/

void find_colors_to_remove(vector<int> &v, int color_to_put_length)
{
	int start, tmp_start = 0;
	
	int end, tmp_end = 0;
	
	int max_number_of_visible_colors = -1;

	while (tmp_end != v.size() - 1)
	{
		int length_of_colors_to_remove = 0;
		int i = tmp_start;

		while (length_of_colors_to_remove > color_to_put_length && i < v.size()) 	// [4, 4]  da aggiungere 4
		{																		 	// v_f = [2, 4, 2]
			length_of_colors_to_remove += v[i];
			i++;
		}

		tmp_end = i;

		int remaining_colors = 0;

		for (int j = 0; j < v.size(); j++)
		{
			if (j < start || j > end)
				remaining_colors++;
		}

		if (max_number_of_visible_colors < remaining_colors)
		{
			start = tmp_start;
			end = tmp_end;
			max_number_of_visible_colors = remaining_colors++;;
		}

		tmp_start++;
	}
}


int get_sum_of_colors_behind(vector<int> &v)
{
    int sum = 0;

    for (int i = 0; i < v.size(); i++)
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
        int white_spaces = N - get_sum_of_colors_behind(used_colors);

        if (white_spaces >= L[i])
        {
            used_colors.push_back(L[i]);
            continue;
        }

        // bisogna capire quali colori possono essere colorati sopra in modo
        // da avere il massimo dei colori nel muro

		int j = used_colors.size();
        int r = -(white_spaces - L[i]);

        while (j - 1 >= 0)
        {
			used_colors[j - 1] -= r;

            if (used_colors[j - 1] < 0)
			{
				r = -(used_colors[j - 1]);      // salvo il nuovo resto da togliere al numero ancora prima
				used_colors.pop_back();  	 // rimuove l'elemento che non si vede più dal nuovo colore
                j--;
                continue;
			}

			if (used_colors[j - 1] == 0)
            {
                if (used_colors.size() < N - 1)
                {
                    used_colors[j - 1] = 1;
                    used_colors.push_back(L[i]);
                }
                else
                {
                    used_colors[j - 1] = L[i];
                }
            }
            else    
                used_colors.push_back(L[i]);


            break;
        }

        if (t != 2)
            continue;

        ifstream in_f("colors.txt");
        ofstream out_f("colors.txt", std::ios::app);

        in_f.seekg(0, ios::end);

        out_f << "\ntry " << t << endl;
        in_f.seekg(0, ios::end);
        string line = "";

        for (int element : used_colors)
        {
            line += (to_string(element) + ", ");
        }
        out_f << line << endl << endl;
        in_f.close();
        out_f.close();
        
    }


	int result = 0;

	for (int i : used_colors)
		result++;

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
