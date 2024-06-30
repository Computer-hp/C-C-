#include <bits/stdc++.h>

using namespace std;


int solve()
{
    int N;
    cin >> N;
    vector<int> v(N);

    for (int &x : v)
        cin >> x;

    int result = -1;

	int max = *max_element(v.begin(), v.end());  // '*max' if you want the value.

	bool state = false;
	int i = 2;

	while (!state)
	{
		int temp_min_num_candy = max * i;

		for (int j = 0; j < N; j++)
		{
			if (temp_min_num_candy % v[j] != 0) break;
			
			if (j == N - 1)
			{
				result = temp_min_num_candy;
				state = true;
			}
		}

		i++;
	}

    return result;
}

const char *file = "input.txt";

int main()
{
	freopen(file, "r", stdin);
    freopen("output.txt", "w", stdout);

    int T;
    cin >> T;

    for (int i = 0; i < T; i++)
	{
        cout << "Case #" << i + 1 << ": " << solve() << '\n';
    }

    return 0;
}