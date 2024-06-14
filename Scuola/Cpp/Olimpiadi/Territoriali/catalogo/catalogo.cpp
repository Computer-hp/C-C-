#include <iostream>
#include <fstream>
#include "bst.h"


using namespace std;


void add(long long int id);
void remove(long long int id);
int count(long long int id);


BST bst;


int main() 
{
    ios::sync_with_stdio(false);

    ifstream cin("input0.txt");
    ofstream cout("output0.txt");

    int Q;
    cin >> Q;

    for (int i = 0; i < Q; i++)
    {
        char t;
        long long int id;
        cin >> t >> id;

        if (t == 'a') 
            add(id);
            
        else if (t == 't') 
            remove(id);

        else if (t == 'c') 
            cout << count(id) << '\n';
    }

    return 0;
}


void add(long long int id)
{
    // if (bst.search(id)) return;  in teoria bisogna mettere anche gli stessi numeri

    bst.insert(id);


    /*

    bool found = false;

    for (auto &v2 : v)
        if (!v2.empty() && v2[0] == id)
        {
            v2.push_back(id);
            found = true;
            break;
        }

    if (found) return;
        
    vector<long long int> new_v2;
    new_v2.push_back(id);

    auto it = find_if(v.begin(), v.end(), [id](const vector<long long int> &v2) 
    {
        return v2.empty() || v2[0] > id;
    });

    if (it != v.end())  v.insert(it, new_v2);

    else                v.push_back(new_v2);

    */
}


void remove(long long int id)
{
    bst.delete_node(id);

    /*auto it = find_if(v.begin(), v.end(), [id](const vector<long long int> &v2)
    {
        return v2[0] == id;
    });

    if (it != v.end())
        it -> pop_back();*/
}


int count(long long int id)
{
    return bst.get_count(id);

    /*
    int counter = 0;

    for (const auto &v2 : v)
    {
        if (v2[0] != id)
            continue;

        counter += count(v2.begin(), v2.end(), id);
        break;
    }

    return counter;
    */
}