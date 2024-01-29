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
    int start = 0, end = 0;

    int max_sum_of_colors = 0;

    int min_color_length = 0;

    vector<int> tmp_colors_on_wall;

    while (end < v.size())
    {
        tmp_colors_on_wall.clear();
        tmp_colors_on_wall.reserve(v.size());
        tmp_colors_on_wall.insert(tmp_colors_on_wall.end(), v.begin(), v.end());

        int sum_of_colors = 0;

        int i = start;

    // problem:  what to do when the first color is == to 'color_to_add'
        while (sum_of_colors < color_to_add && i < v.size())
        {
            sum_of_colors += v[i];
            i++;
        }

        end = i;
        
        if (end - start == 1 && end + 1 < v.size())
            sum_of_colors += v[end + 1];

        //  smallest number and < then color_to_add     
        int position_min_color = -1;
        int position_bigger_min_color = -1; //  color > color_to_add and the smallest of that type

        get_min_color_length(tmp_colors_on_wall, start, end, &position_min_color, &position_bigger_min_color, color_to_add);

        if (position_bigger_min_color != -1)
        {
            tmp_colors_on_wall[position_bigger_min_color] -= color_to_add;

            // place left or right the color_to_add
            place_new_color_right_or_left(tmp_colors_on_wall, color_to_add, position_bigger_min_color);
            continue;
        }


        // have to use the smallest color between start && end

        int rest = -(v[position_min_color] - color_to_add); 

        if (rest == 0)
        {
            start++;
            continue;
        }

        // 1° problem: what to do when tmp_colors_on_wall[position_min_color] == 1, because it has to be removed
        place_new_color_at_the_best_side(tmp_colors_on_wall, color_to_add, position_min_color, rest);


// save number of colors at the right or left
// ...


        start++;
    }
}



/*
    places the color in the position where at the other side are more colors

    input:
        [4, 1, 1, 1, 1]   <----   3

    output:
        [1, 3, 1, 1, 1]

    error output:
        [4, 3, 1]  or  [4, 1, 3]
*/

void place_new_color_at_the_best_side(vector<int> &v, int color_to_add, int position_min_color, int rest)
{
    if (position_min_color == 0)
    {
        int right_index = check_colors_at_left_or_right(v, position_min_color, 1, rest);  // right side

        // place the color at the index returned
        v.erase(v.begin() + position_min_color, v.begin() + right_index + 1);   // '+ 1' to include the color at position 'position_min_color'.
        v.insert(v.begin() + position_min_color, color_to_add);

        return;
    }

    if (position_min_color == v.size() - 1)
    {
        int left_index = check_colors_at_left_or_right(v, position_min_color, -1, rest); // left side

        // place the color at the index returned
        v.erase(v.begin() + left_index, v.begin() + position_min_color + 1);
        v.insert(v.begin() + left_index, color_to_add);
        return;
    }

    int right_index = check_colors_at_left_or_right(v, position_min_color, 1, rest);  // right side
    int left_index  = check_colors_at_left_or_right(v, position_min_color, -1, rest); // left side
    



}



/*

    Finds, presuming that right_or_left_index is >= '0' && < 'vector (wall) size()' ,

    the position where the color to add has to be placed, returning the index.

    input:
            [3, 4, 7]    <----   5

    output:
            [1, 5, 1, 7]
                ^
                |
                |
            index where color long '5' has been placed

*/

int check_colors_at_left_or_right(vector<int> &v, int position_min_color, int plus_or_minus, int rest)
{
    int right_or_left_index = position_min_color + plus_or_minus;
    int right_or_left_rest;

    while (right_or_left_index >= 0 && right_or_left_index < v.size())
    {
        right_or_left_rest = v[right_or_left_index] - rest;

        if (right_or_left_rest >= 0)
            return right_or_left_index;

        right_or_left_index += plus_or_minus;

        rest = -right_or_left_rest; 
    }

    return -1;  // the color is to big for putting it on the wall from 'start' to 'end'
}


void place_new_color_right_or_left(vector<int> &v, int color_to_add, int position_bigger_min_color)
{
    if (position_bigger_min_color == 0)
    {
        v.insert(v.begin() + 1, color_to_add);
        return; 
    }
    
    if (position_bigger_min_color == v.size() - 1)
    {
        v.insert(v.end() - 1, color_to_add);
        return; 
    }

    int end = v.size() - 1;
    int left_colors_index = position_bigger_min_color - 1;
    int right_colors_index = position_bigger_min_color + 1;

    // checks only one number, one on the left and the other on the right
    int left_color_position = position_bigger_min_color - 1;
    int right_color_position = position_bigger_min_color + 1;

    if (v[left_color_position] > v[right_color_position])
        v.insert(v.begin() + left_color_position, v[left_color_position]); 
    
    else
        v.insert(v.begin() + right_color_position, v[right_color_position]);
}



void get_min_color_length(vector<int> &v, int start, int end, int *position_min_color, int *position_bigger_min_color, int color_to_add)
{
    int min_color = v[start], bigger_min_color = v[start];

    for (int i = start; i <= end; i++)
    {
        if (v[i] < min_color)
        {
            min_color = v[i];
            *position_min_color = i;

            if (min_color > color_to_add && min_color < bigger_min_color)
            {
                bigger_min_color = min_color;
                *position_bigger_min_color = i; 
            }
        }
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
	
    	put_color_in_vector(used_colors, L[i]);
    }


	int result = 0;

	for (int i : used_colors)
		result++;

    std::cout << "Case #" << t << ": " << result << endl;
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
