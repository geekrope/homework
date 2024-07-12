//https://acmp.ru/index.asp?main=task&id_task=132

#include <iostream>
#include <set>
#include <vector>

using namespace std;

vector<long long> dijkstra(vector<vector<pair<int, int>>> graph, int start)
{
    const long long INF = -1;
    const long long size = graph.size();

    int current = start;
    vector<long long> dist(size, INF);
    vector<bool> used(size);

    dist[start] = 0;

    while (true)
    {
        used[current] = true;

        for (auto adjecent : graph[current])
        {
            auto vertexIndex = adjecent.first;
            auto edgeWeight = adjecent.second;
            auto currentDist = dist[vertexIndex];
            auto alternativeDist = dist[current] + edgeWeight;

            auto newDist = currentDist == INF ? alternativeDist : min(currentDist, alternativeDist);
            dist[vertexIndex] = newDist;
        }

        int minDist = INF;
        for (int index = 0; index < size; index++)
        {
            if (!used[index] && (dist[index] < minDist || minDist == -1) && dist[index] != INF)
            {
                current = index;
                minDist = dist[index];
            }
        }

        if (minDist == INF)
        {
            break;
        }
    }

    return dist;
}

int main()
{
    int n, s, f;
    cin >> n >> s >> f;
    s--; f--;
    vector<vector<pair<int, int>>> graph(n);

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            int w;
            cin >> w;
            if (w > 0)
            {
                graph[i].push_back({ j, w });
            }
        }
    }

    auto dist = dijkstra(graph, s);

    cout << dist[f];
}