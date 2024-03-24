#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>


using namespace std;


void aggiungi(long long int id);
void togli(long long int id);
int conta(long long int id);


vector<vector<long long int>> v;


int main() 
{
    ios::sync_with_stdio(false);

    ifstream cin("input.txt");
    ofstream cout("output.txt");

    int Q;
    cin >> Q;

    for (int i = 0; i < Q; i++)
    {
        char t;
        long long int id;
        cin >> t >> id;

        if (t == 'a') 
            aggiungi(id);
            
        else if (t == 't') 
            togli(id);

        else if (t == 'c') 
            cout << conta(id) << '\n';
    }

    return 0;
}


void aggiungi(long long int id)
{
    bool found = false;

    for (auto &v2 : v)
        if (!v2.empty() && v2[0] == id)
        {
            v2.push_back(id);
            found = true;
            break;
        }


    if (found)
        return;
        
    vector<long long int> new_v2;

    new_v2.push_back(id);

    auto it = find_if(v.begin(), v.end(), [id](const vector<long long int> &v2) 
    {
        return v2.empty() || v2[0] > id;
    });

    if (it != v.end())  v.insert(it, new_v2);

    else                v.push_back(new_v2);
}


void togli(long long int id)
{
    auto it = find_if(v.begin(), v.end(), [id](const vector<long long int> &v2)
    {
        return v2[0] == id;
    });

    if (it != v.end())
        it -> pop_back();
}


int conta(long long int id)
{
    int counter = 0;

    for (const auto &v2 : v)
    {
        if (v2[0] != id)
            continue;

        counter += count(v2.begin(), v2.end(), id);
        break;
    }

    return counter;
}