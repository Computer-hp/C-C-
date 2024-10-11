#include <string>
#include <iostream>
#include <fstream>
#include <cstdio>

using namespace std;

int main() 
{
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);

    int T, t;
    cin >> T;

    for (t = 1; t <= T; t++) {
        int N, F, C;
        
        int nf, nc;

        cin >> N >> F >> C;


        int counter = 0;
        int film_sum = 0;

        while (film_sum + F <= N)
        {
          film_sum += F;
          counter++;
        }          

        nf = counter;

        int songs_sum = 0;
        counter = 0;
        
        while (songs_sum + C <= N - film_sum)
        {  
          songs_sum += C;
          counter++;
        }
        
        nc = counter;

        cout << "Case #" << t << ": " << nf << " " << nc << endl;
    }
}

