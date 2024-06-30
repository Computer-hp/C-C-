#include <vector> 
#include <algorithm>

using namespace std;

void inizia();
void apri(long long p);
void chiudi(long long p);
long long chiedi(long long p);


vector<long long> auto_stops;


void inizia() 
{
    return;
}

void apri(long long p) 
{
    auto it = std::lower_bound(auto_stops.begin(), auto_stops.end(), p);
    auto_stops.insert(it, p);
}

void chiudi(long long p) 
{
    auto it = std::lower_bound(auto_stops.begin(), auto_stops.end(), p);
    
    if (it != auto_stops.end()) auto_stops.erase(it);
}

long long chiedi(long long p)
{
    if (auto_stops.size() == 0) return -1;

    auto min_idx = std::lower_bound(auto_stops.begin(), auto_stops.end(), p);

    if (min_idx == auto_stops.end()) return auto_stops[auto_stops.size() - 1];

    if (min_idx != auto_stops.begin())
    {
        if (*(min_idx - 1) == p) return *(min_idx - 1);

        if (std::abs(*(min_idx - 1) - p) < std::abs(*(min_idx) - p)) return *(min_idx - 1);
    }

    return *min_idx;
}