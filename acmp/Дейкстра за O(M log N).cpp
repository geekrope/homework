//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=32&id_topic=54&id_problem=1038

#include <iostream>
#include <set>
#include <vector>

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
    int n, m, s;
    cin >> n >> m >> s;
    vector<vector<pair<int, int>>> graph(n);

    for (int i = 0; i < m; i++)
    {
        int l, r, w;
        cin >> l >> r >> w;

        graph[l].push_back({ r, w });
        graph[r].push_back({ l, w });

    }

    auto dist = dijkstra(graph, s);

    for (auto d : dist)
    {
        cout << (d == INT_MAX ? 2009000999 : d) << ' ';
    }
}