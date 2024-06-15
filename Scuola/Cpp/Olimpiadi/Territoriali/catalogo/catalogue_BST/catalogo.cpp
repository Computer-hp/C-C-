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

    ifstream cin("ex.txt");
    ofstream cout("out.txt");

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


int count(long long int id)
{
    return bst.get_count(id);
}