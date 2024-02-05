#include <iostream>
#include <string>
#include <vector>

using namespace std;

        //  y         x
// baskets [n_basket, elements_inside_the_basket]

void move(vector<string> &baskets, int source_basket, int destination_basket)
{
    string source_basket_content = baskets[source_basket];
    int source_basket_length = source_basket_content.size() - 1;

    baskets[destination_basket] += source_basket_content[source_basket_length];

    baskets[source_basket].erase(source_basket_length);
}


void find(vector<string> &baskets, int destination_basket, int item_position, string &answer)
{
    answer += (baskets[destination_basket][item_position]);
}


void solve(int t) 
{
    int N, M, Q;
    cin >> N >> M >> Q;

    string S;  // oggetti nel cestino '0'
    cin >> S;

    string answer;

    vector<string> baskets;
    baskets.resize(M);
    baskets[0] += (S);

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