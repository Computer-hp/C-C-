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

void put_color_in_vector(vector<int> &v, int color_to_add)
{
    int start = 0, end 0;

    int max_sum_of_colors = 0;

    int min_color_length = 0;

    while (true)
    {
        int sum_of_colors = 0;

        int i = start;

        while (sum_of_colors >= color_to_add && i < v.size())
        {
            sum_of_colors += v[i];
            i++;
        }

        end = i;
        
        //  smallest number     |||  color > color_to_add and the smallest of that type
        int position_min_color = -1, position_bigger_min_color = -1;

        get_min_color_length(v, start, end, &position_min_color, &position_bigger_min_color, color_to_add);

        if (position_bigger_min_color != -1)
        {
            v[position_bigger_min_color] -= color_to_add;
            return;
        }


    }
}




void get_min_color_length(vector<int> &v, int start, int end, int *position_min_color, int *position_bigger_min_color, int color_to_add)
{
    int min_color = v[start], bigger_min_color = -1;

    for (int i = start; i <= end; i++)
    {
        if (v[i] < min_color)
        {
            min_color = v[i];
            *position_min_color = i;

            if (min_color > color_to_add)
            {
                bigger_min_color = provisory_min;
                *position_bigger_min_color = i; 
            }
        }
    }

    return provisory_min;
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
	
	put_color_in_vector();

	/*

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
	*/
        
    }


	int result = 0;

	for (int i : used_colors)
		result++;

    cout << "Case #" << t << ": " << result << endl;
}


int main() 
{

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
