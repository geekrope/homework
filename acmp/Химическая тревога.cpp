//https://acmp.ru/index.asp?main=task&id_task=469

#include <iostream>
#include <set>
#include <vector>
#include <climits>

using namespace std;

vector<long long> dijkstra(vector<vector<pair<int, int>>> graph, int start)
{
    const long long INF = 2009000999;
    const long long size = graph.size();

    vector<long long> dist(size, INF);
    vector<bool> used(size);
    set<pair<int, long long>> unused;

    dist[start] = 0;
    unused.insert({ 0, start });

    while (unused.size() > 0)
    {
        auto current = (*unused.begin()).second;
        used[current] = true;
        unused.erase(unused.begin());

        for (auto adjecent : graph[current])
        {
            auto vertexIndex = adjecent.first;
            auto edgeWeight = adjecent.second;
            auto currentDist = dist[vertexIndex];
            auto alternativeDist = dist[current] + edgeWeight;

            auto newDist = currentDist == INF ? alternativeDist : min(currentDist, alternativeDist);
            auto vertexItterator = unused.find({ dist[vertexIndex], vertexIndex });

            if (!used[vertexIndex])
            {
                if (vertexItterator != unused.end())
                {
                    unused.erase(vertexItterator);
                }
                unused.insert({ newDist, vertexIndex });
            }

            dist[vertexIndex] = newDist;
        }
    }

    return dist;
}

int main()
{
    int n, m;
    cin >> n >> m;
    vector<vector<int>> area(n, vector<int>(m));
    vector<vector<pair<int, int>>> graph(n * m);

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            int contamination;
            cin >> contamination;
            area[i][j] = contamination;
        }
    }

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            int index = i * m + j;

            if (i > 0)
            {
                graph[index].push_back({ ((i - 1) * m + j), area[i - 1][j] });
                graph[((i - 1) * m + j)].push_back({ index, area[i][j] });
            }
            if (j > 0)
            {
                graph[index].push_back({ (i * m + j - 1), area[i][j - 1] });
                graph[(i * m + j - 1)].push_back({ index, area[i][j] });
            }
            if (i < n - 1)
            {
                graph[index].push_back({ ((i + 1) * m + j), area[i + 1][j] });
                graph[((i + 1) * m + j)].push_back({ index, area[i][j] });
            }
            if (j < m - 1)
            {
                graph[index].push_back({ (i * m + j + 1),area[i][j + 1] });
                graph[(i * m + j + 1)].push_back({ index,area[i][j] });
            }
        }
    }

    auto accumulated = dijkstra(graph, 0);

    cout << accumulated[m * n - 1] + area[0][0];
}