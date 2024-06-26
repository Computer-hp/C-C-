#include <iostream>
#include <cassert>
#include <utility>
#include <vector>
#include <fstream>
#include "autogrill.cpp"

using namespace std;

void inizia();
void apri(long long p);
void chiudi(long long p);
long long chiedi(long long p);


int main() 
{
    ios::sync_with_stdio(false);

    ifstream cin("input.txt");
    ofstream cout("output.txt");

    int Q;
    cin >> Q;

    inizia();

    for (int i = 0; i < Q; i++)
    {
        long long p;
        char t;
        cin >> t >> p;

        if (t == 'a') apri(p);

        else if (t == 'c') chiudi(p);

        else cout << chiedi(p) << endl;
    }

    cin.close();
    cout.close();

    return 0;
}
