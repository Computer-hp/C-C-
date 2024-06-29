#include <vector> 
#include <algorithm>

using namespace std;

void inizia();
void apri(long long p);
void chiudi(long long p);
long long chiedi(long long p);

int lower_bound(vector<long long> &v, int left, int right, long long p);

vector<long long> auto_stops;


void inizia() {
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

    int min_idx = lower_bound(auto_stops, 0, auto_stops.size(), p);

    // if (min_idx == 0 || min_idx == auto_stops.size()) return auto_stops[min_idx];

    // long long int left = abs(auto_stops[min_idx - 1] - p);
    // long long int right = abs(auto_stops[min_idx] - p);

    // if (left < right) return auto_stops[min_idx - 1];

    return auto_stops[min_idx];
}


int lower_bound(vector<long long> &v, int left, int right, long long p)
{
    // if (left >= right) return right;

    // int mid = left + (right - left) / 2;

    // // if (v[mid] == p) return mid;
    // // if (v[mid + 1] == p) return mid + 1;

    // if (v[mid] < p) return lower_bound(v, mid + 1, right, p);

    // return lower_bound(v, left, mid, p);

    if (left >= right) 
    {
        if (left == v.size()) return right - 1; // 'p' is greater than any element in the vector
        if (right == 0) return left; // 'p' is less than any element in the vector

        return (std::abs(v[left] - p) < std::abs(v[right - 1] - p)) ? left : (right - 1);
    }

    int mid = left + (right - left) / 2;

    if (v[mid] == p) return mid;

    if (v[mid] < p) 
    {
        if (mid + 1 < v.size() && std::abs(v[mid + 1] - p) < std::abs(v[mid] - p))
            return lower_bound(v, mid + 1, right, p);
        return mid;
    }

    if (mid - 1 >= 0 && std::abs(v[mid - 1] - p) < std::abs(v[mid] - p))
        return lower_bound(v, left, mid, p);
    return mid;
}