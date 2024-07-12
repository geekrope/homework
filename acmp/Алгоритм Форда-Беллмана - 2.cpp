//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=32&id_topic=53&id_problem=1033

#include <iostream>
#include <vector>

using namespace std;

const long long INF = INT_MAX;

vector<long long> ford(int size, int start)
{
    vector<long long> dist(size, INF);

    dist[start] = 0;
    bool f = true;

    while (f)
    {
        f = false;
        for (int from = 0; from < size; from++)
        {
            for (int to = from + 1; to < size; to++)
            {
                auto weight = (179 * (from + 1) + 719 * (to + 1)) % 1000 - 500;

                if (dist[from] < INF && weight + dist[from] < dist[to])
                {
                    dist[to] = weight + dist[from];
                    f = true;
                }
            }
        }
    }

    return dist;
}

int main()
{
    int n;
    cin >> n;

    auto dist = ford(n, 0);

    cout << dist[n - 1];
}