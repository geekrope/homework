#include <iostream>
#include <string>
#include <vector>

using namespace std;

void A()
{
	int n, m, k = 0;

	cin >> n >> m >> k;

	vector<vector<long long int>> prefixes(m + 1, vector<long long int>(n + 1, 0));

	for (int j = 1; j <= n; j++)
	{
		for (int i = 1; i <= m; i++)
		{
			int current;

			cin >> current;

			prefixes[i][j] = prefixes[i][j - 1] + prefixes[i - 1][j] - prefixes[i - 1][j - 1] + current;
		}
	}

	for (int i = 0; i < k; i++)
	{
		int y1, x1, y2, x2;

		cin >> y1 >> x1 >> y2 >> x2;

		cout << prefixes[x2][y2] - prefixes[x2][y1 - 1] - prefixes[x1 - 1][y2] + prefixes[x1 - 1][y1 - 1] << endl;
	}
}

void B()
{
	int n, cur, m;

	cin >> n >> cur;

	auto prefix = new long long int[n];
	prefix[0] = cur;

	for (int index = 1; index < n; index++)
	{
		cin >> cur;
		prefix[index] = prefix[index - 1] + cur;
	}

	cin >> m;

	auto out = new long long int[m];

	for (int i = 0; i < m; i++)
	{
		int f, s;
		cin >> f >> s;
		f--; s--;

		out[i] = prefix[s] - ((f - 1) >= 0 ? (prefix[f - 1]) : 0);
	}

	for (int i = 0; i < m; i++)
	{
		cout << out[i] << endl;
	}
}

int main()
{
	A();
}