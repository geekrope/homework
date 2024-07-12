//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=32&id_topic=54&id_problem=1037

#include <iostream>
#include <set>
#include <vector>

using namespace std;

pair<vector<long long>, vector<int>> dijkstra(vector<vector<pair<int, int>>> graph, int start)
{
    const long long INF = -1;
    const long long size = graph.size();

    int current = start;
    vector<int> parent(size, -1);
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

            if (currentDist == INF || alternativeDist < currentDist)
            {
                dist[vertexIndex] = alternativeDist;
                parent[vertexIndex] = current;
            }
            else
            {
                dist[vertexIndex] = currentDist;
            }
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

    return { dist ,parent };
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

    auto data = dijkstra(graph, s);
    vector<int> path;

    if (data.first[f] == -1)
    {
        cout << -1;
    }
    else
    {
        int current = f;

        while (current != s)
        {
            path.push_back(current);
            current = data.second[current];
        }
        path.push_back(s);

        for (int i = path.size() - 1; i >= 0; i--)
        {
            cout << path[i] + 1 << ' ';
        }
    }
}