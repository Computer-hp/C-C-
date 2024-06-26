#include <vector> 
#include <algorithm>

using namespace std;

int lower_bound(vector<long long> &v, int left, int right, long long p);

vector<long long> auto_stops;


void inizia() {
    return;
}

void apri(long long p) 
{
    auto it = lower_bound(auto_stops.begin(), auto_stops.end(), p);
    auto_stops.insert(it, p);
}

void chiudi(long long p) 
{
    auto it = lower_bound(auto_stops.begin(), auto_stops.end(), p);
    
    if (it != auto_stops.end() && *it == p) auto_stops.erase(it);
}

long long chiedi(long long p)
{
    if (auto_stops.size() == 0) return -1;

    int min_idx = lower_bound(auto_stops, 0, auto_stops.size(), p);

    if (min_idx == 0 || min_idx == auto_stops.size()) return auto_stops[min_idx];

    int left = abs(auto_stops[min_idx - 1] - p);
    int right = abs(auto_stops[min_idx] - p);

    if (left < right) return min_idx - 1;

    return min_idx;
}


int lower_bound(vector<long long> &v, int left, int right, long long p)
{
    if (left >= right) return left;

    int mid = left + (right - left) / 2;

    if (v[mid] < p) return lower_bound(v, mid + 1, right, p);

    return lower_bound(v, left, mid, p);
}