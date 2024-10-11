#include <iostream>
#include <string>
#include <vector>

using namespace std;

        //  y         x
// baskets [n_basket, elements_inside_the_basket]


void add_item_to_basket(string &baskets, size_t position_destination_basket, char item_to_add)
{
    int i = (int)position_destination_basket + 1;

    while (!isdigit(baskets[i]) && i < baskets.size())
        i++; 

    baskets.insert(i, 1, item_to_add);
}


char get_and_remove_last_item_of_basket(string &baskets, size_t position_source_basket)
{
    char result;
    int i = (int)position_source_basket + 1;

    while (!isdigit(baskets[i]) && i < baskets.size())
    {
        result = baskets[i];
        i++; 
    }        

    baskets.erase(i - 1);

    return result;
}


void move(string &baskets, int source_basket, int destination_basket)
{
    size_t position_source_basket = baskets.find(to_string(source_basket));
    size_t position_destination_basket = baskets.find(to_string(destination_basket));

    if (position_source_basket == std::string::npos)
    {
        cout << "\nError occured while searching for the source basket\n";
        return;
    } 
    
    if (position_destination_basket == std::string::npos)
    {
        cout << "\nError occured while searching for the destination basket\n";
        return;
    }

    char item_to_add = get_and_remove_last_item_of_basket(baskets, position_source_basket);

    add_item_to_basket(baskets, position_destination_basket, item_to_add);
}



void find(string &baskets, int destination_basket, int item_position, string &answer)
{
    size_t position_first_item_in_destination_basket = baskets.find(to_string(destination_basket));

    if (position_first_item_in_destination_basket == std::string::npos)
    {
        cout << "\nError occured while searching for the destination basket\n";
        return;
    } 

    answer += (baskets[position_first_item_in_destination_basket + item_position + 1]);
}


void solve(int t) 
{
    int N, M, Q;
    cin >> N >> M >> Q;

    string S;  // oggetti nel cestino '0'
    cin >> S;

    string answer;

    string baskets;

    for (int i = 0; i < M; i++)
        baskets += to_string(i + 1);

    baskets.insert(1, S);

    for (int i = 0; i < baskets.size(); i++)
    {
        cout << "_" << baskets[i] << "_";
    }

    for (int i = 0; i < Q; i++) 
	{
        char type;
        int a, b;
        cin >> type >> a >> b;

        if (type == 's') 
            move(baskets, a, b);

		else 
            find(baskets, a, b, answer);
    }


    cout << "Case #" << t << ": " << answer << endl;
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

