#include <vector> 
#include <algorithm>

using namespace std;

int my_binary_search(vector<long long> &v, int low, int high, int min_idx, long long p);


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

    int length = auto_stops.size();
    int min_idx = my_binary_search(auto_stops, 0, length - 1, length - 1, p);
    return auto_stops[min_idx];
}


int my_binary_search(vector<long long> &v, int low, int high, int min_idx, long long p)
{
    if (low >= high) return min_idx;

    int mid = low + (high - low) / 2;

    long long left = abs(v[mid] - p);
    long long right = abs(v[mid + 1] - p);

    if (left == right)
    {
        int min_idx_left = my_binary_search(v, low, mid, mid, p);
        int min_idx_right = my_binary_search(v, mid + 1, high, mid + 1, p);

        if (abs(v[min_idx_left] - p) < abs(v[min_idx_right] - p)) return min_idx_left;

        if (abs(v[min_idx_right] - p) > abs(v[min_idx_left] - p)) return min_idx_right;

        return max(min_idx_left, min_idx_right);
    }

    if (left < right && left < v[min_idx])
        return my_binary_search(v, low, mid, mid, p);

    if (right < left && right < v[min_idx])
        return my_binary_search(v, mid + 1, high, mid + 1, p);

    return min_idx;
}