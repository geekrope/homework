//https://acmp.ru/index.asp?main=task&id_task=138

# include <iostream>
# include <vector>

using namespace std;

const long long INF = INT_MAX;

vector<long long> ford(vector<vector<int>> graph, int size, int start)
{
    vector < long long> dist(size, INF);

    dist[start] = 0;

    for (int k = 0; k < size - 1; k++)
    {
        for (auto edge : graph)
        {
            auto from = edge[0];
            auto to = edge[1];
            auto weight = edge[2];
            auto alternativeDist = dist[from] < INF ? weight + dist[from] : INF;

            if (alternativeDist < INF && alternativeDist < dist[to])
            {
                dist[to] = alternativeDist;
            }
        }
    }

    return dist;
}

int main()
{
    int n, m;
    cin >> n >> m;
    vector<vector<int>> graph(m);

    for (int i = 0; i < m; i++)
    {
        int f, s, w;
        cin >> f >> s >> w;
        f--; s--;
        graph[i] = { f,s,w};
    }

    auto dist = ford(graph, n, 0);

    for (auto d : dist)
    {
        cout << (d < INF ? d : 30000) << ' ';
    }
}