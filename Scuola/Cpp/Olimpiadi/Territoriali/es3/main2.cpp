#include <iostream>
#include <string>
#include <vector>

using namespace std;

/*
    N   lunghezza muro

    Q   numero colori

    L   n° metri che ciascun colore può occupare (ordine primo ultimo)
*/


int get_equal_color_position(vector<int> &v, int color_to_add, char* right_or_left_side)
{
	for (int i = 0; i < v.size(); i++)
	{
		if (v[i] == color_to_add)
		{
			if (i > 0 && v[i - 1] > 1)
			{
				*right_or_left_side = 'l';
				return i;
			}
			else if (i < v.size() - 1 && v[i + 1] > 1)
			{
				*right_or_left_side = 'r';
				return i;
			}
		}
	}

	return -1;
}



void get_min_color_length(vector<int> &v, int *position_bigger_min_color, int color_to_add)
{
    int bigger_min_color = v[0];

    for (int i = 0; i < v.size(); i++)
    {
        if (v[i] < bigger_min_color && v[i] > color_to_add)
        {
			bigger_min_color = v[i];
			*position_bigger_min_color = i; 
        }
    }
}



void put_color_in_vector(vector<int> &v, int color_to_add)
{
	vector<int> tmp_vector;

	int position_bigger_min_color = -1;
	int position_min_color = -1;

	get_min_color_length(v, &position_bigger_min_color, color_to_add);

	if (position_bigger_min_color != -1)
	{
		v[position_bigger_min_color] -= color_to_add;
		v.insert(v.begin() + position_bigger_min_color + 1, color_to_add);
		return;
	}

	char right_or_left_side;
	int equal_color_position = get_equal_color_position(v, color_to_add, &right_or_left_side);

	if (right_or_left_side == 'l')
	{
		v[equal_color_position] = 1;
		v[equal_color_position - 1] -= 1;		
		v.insert(v.begin() + equal_color_position, color_to_add);
		return;
	}
	else if (right_or_left_side == 'r')
	{
		v[equal_color_position] = 1;
		v[equal_color_position + 1] -= 1;		
		v.insert(v.begin() + equal_color_position + 1, color_to_add);
		return;
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


void print_vector(vector<int> &v)
{
	cout << "\n\tVector: ";
	
	for (int i = 0; i < v.size(); i++)
	{
		cout << v[i] << " ";
	}
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
			print_vector(used_colors);
            continue;
        }
		put_color_in_vector(used_colors, L[i]);
		print_vector(used_colors);
	}

	int result = 0;

	for (int i : used_colors)
		result++;

    cout << "Case #" << t << ": " << result << endl;
}



int main() {

    freopen("input2.txt", "r", stdin);
    freopen("output2.txt", "w", stdout);

    int T;
    cin >> T;

    for (int t = 1; t <= T; t++) 
    {
        solve(t);
    }

    return 0;
}