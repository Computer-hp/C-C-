#include <iostream>
#include <fstream>
#include "bst.h"


using namespace std;


void add(long long int id);
void remove(long long int id);
long long int count(long long int id);


BST bst;


int main() 
{
    ios::sync_with_stdio(false);

    ifstream cin("input1.txt");
    ofstream cout("output1.txt");

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
    bst.insert(id);
}


void remove(long long int id)
{
    bst.delete_node(id);
}


long long int count(long long int id)
{
    return bst.get_count(id);
}