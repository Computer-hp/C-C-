#include <iostream>
#include <string>
#include <vector>
#include <climits>


using namespace std;

void put_color_in_vector(vector<int> &v, int color_to_add, int wall_length);
int find_min_color_to_be_decreased(vector<int> &v, int color_to_add);
void put_color_in_best_position(vector<int> &v, int color_to_add);
void set_value_to_result_wall(vector<int> &result_wall, vector<int> &tmp_wall);
int get_sum_of_colors_behind(vector<int> &v);
void solve(int t);
void print_vector(vector<int> &v, string vector_name, int i);


int main() 
{
    freopen("muro_input_6.txt", "r", stdin);
    freopen("output.txt", "w", stdout);

    int T;
    cin >> T;

    for (int t = 1; t <= T; t++)
        solve(t);

    return 0;
}


void put_color_in_vector(vector<int> &v, int color_to_add, int wall_length)
{	
	if (color_to_add == wall_length - 1)
	{
		v.clear();
		v.insert(v.begin(), {1, color_to_add});
		return;
	}

	int pos_min_color_to_be_decreased = find_min_color_to_be_decreased(v, color_to_add);
	
	if (pos_min_color_to_be_decreased != -1)
	{
		v[pos_min_color_to_be_decreased] -= color_to_add;
		v.insert(v.begin() + pos_min_color_to_be_decreased + 1, color_to_add);

		return;
	}

	put_color_in_best_position(v, color_to_add);
}


/*
	finds the smallest color from which i can subtract
	colors to add color_to_add
*/

int find_min_color_to_be_decreased(vector<int> &v, int color_to_add)
{
	int smallest_color = INT_MAX;
	int pos_smallest_color = -1;

	for (int i = 0; i < v.size(); i++)
		if (v[i] < smallest_color && (v[i] > color_to_add))
		{
			pos_smallest_color = i;
			smallest_color = v[pos_smallest_color];
		}
		
	return pos_smallest_color;
}


void put_color_in_best_position(vector<int> &v, int color_to_add)
{
	int start = -1, end = 0;

	vector<int> tmp_wall;	
	vector<int> result_wall;

	while (end < v.size())
	{
		tmp_wall.clear();
		tmp_wall.insert(tmp_wall.end(), v.begin(), v.end());

		int i = ++start;
		int sum_of_colors = 0;

		while (sum_of_colors < color_to_add && i < tmp_wall.size())
			sum_of_colors += tmp_wall[i++];

		if (sum_of_colors < color_to_add)
			break;

		if (sum_of_colors == color_to_add)
		{
			if (i < tmp_wall.size())
				sum_of_colors += tmp_wall[i++];
			else
			{
				end = i;
				tmp_wall.insert(tmp_wall.begin() + start, color_to_add);
				tmp_wall.erase(tmp_wall.begin() + start + 1, tmp_wall.begin() + end);
				set_value_to_result_wall(result_wall, tmp_wall);
				continue;
			}	
		}	

		end = i; // after the last color used

		if (end - start == 1)
		{
			tmp_wall.insert(tmp_wall.begin() + start + 1, color_to_add);

			if ((tmp_wall[start] -= color_to_add) == 0)
				tmp_wall.erase(tmp_wall.begin() + start);

			set_value_to_result_wall(result_wall, tmp_wall);
			continue;
		}

		// sum_of_colors is necessarily > color_to_add
		int length_between_start_end = sum_of_colors - tmp_wall[start] - tmp_wall[end - 1];
		int rest_of_color_to_add = (tmp_wall[start] - 1 + length_between_start_end) - color_to_add;
		tmp_wall[start] = 1;

		if (length_between_start_end != 0)
			tmp_wall.erase(tmp_wall.begin() + start + 1, tmp_wall.begin() + end - 1);

		tmp_wall.insert(tmp_wall.begin() + start + 1, color_to_add);

		if ((tmp_wall[start + 2] += rest_of_color_to_add) == 0)
			tmp_wall.erase(tmp_wall.begin() + start + 2);

		set_value_to_result_wall(result_wall, tmp_wall);
	}	

	v.clear();
	v.insert(v.end(), result_wall.begin(), result_wall.end());
}


void set_value_to_result_wall(vector<int> &result_wall, vector<int> &tmp_wall)
{
	if (result_wall.size() == 0)
	{
		result_wall.insert(result_wall.end(), tmp_wall.begin(), tmp_wall.end());
		return;
	}
	
	if (result_wall.size() <= tmp_wall.size())
	{
		result_wall.clear();
		result_wall.insert(result_wall.end(), tmp_wall.begin(), tmp_wall.end());
	}
}


int get_sum_of_colors_behind(vector<int> &v)
{
    int sum = 0;

    for (int i = 0; i < v.size(); i++)
        sum += v[i];

    return sum;
}


void solve(int t) 
{
    int N, Q;
    cin >> N >> Q;

	vector<int> L(Q);
	int start = 0;

    for (int i = 0; i < Q; i++) 
    {
        cin >> L[i];

		if (L[i] == N) start = i;
    }


    vector<int> used_colors;
    used_colors.push_back(L[start]);

    for (int i = start + 1; i < Q; i++)
    {
        int white_spaces = N - get_sum_of_colors_behind(used_colors);

		if (white_spaces <= 0)
		{
			put_color_in_vector(used_colors, L[i], N);
		//	print_vector(used_colors, "used_colors", i);

			continue;
		}

		used_colors.push_back(L[i]);  // *()
		
		if (white_spaces >= L[i]) continue;

		int rest = L[i] - white_spaces;

		// white spaces are not enough

		/*
			Solution (probably):

			insert between each pair of color the new_color instead of *(pushing it back) immediately.

			subtract from left, right or both colors some meters, then count used_colors.size() [ FIND MAX_SIZE ].


		*/

		for (int j = used_colors.size() - 2; j >= 0; j--)
		{
			used_colors[j] -= rest;

			if (used_colors[j] > 0) break;

			if (used_colors[j] == 0)
			{
				used_colors.erase(used_colors.begin() + j);
				break;
			}

			rest = -used_colors[j];
			used_colors.erase(used_colors.begin() + j);
		}
    }

    cout << "Case #" << t << ": " << used_colors.size() << endl;
}


void print_vector(vector<int> &v, string vector_name, int i)
{
	cout << '\n' << vector_name << " " << i << " = ";

	for (int e : v)
		cout << e << " ";

	cout << '\n';
}