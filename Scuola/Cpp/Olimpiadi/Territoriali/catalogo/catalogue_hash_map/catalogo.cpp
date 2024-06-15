#include <iostream>
#include <fstream>
#include <unordered_map>

using namespace std;

class NumberCatalogue {
private:
    std::unordered_map<long long int, long long int> catalogue;

public:
    void add(long long int number) {
        catalogue[number]++;
    }

    void remove(long long int number) {
        auto it = catalogue.find(number);
        if (it != catalogue.end()) {
            if (it->second > 1) {
                it->second--;
            } else {
                catalogue.erase(it);
            }
        }
    }

    int count(long long int number) const {
        auto it = catalogue.find(number);
        return (it != catalogue.end()) ? it->second : 0;
    }
};

NumberCatalogue catalogue;

static int Q, i;
static long long int id;
static char t;

void aggiungi(long long id);
void togli(long long id);
int conta(long long id);


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

    cin.close();
    cout.close();

    bst.print();

    return 0;
}


void aggiungi(long long int id)
{
    catalogue.add(id);
}


void togli(long long int id)
{
    catalogue.remove(id);
}


int conta(long long int id)
{
    return catalogue.count(id);
}