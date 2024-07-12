//https://acmp.ru/index.asp?main=task&id_task=562

#include <iostream>
#include <vector>

using namespace std;

const long long INF = INT_MAX;

vector<vector<long long>> floyd(vector<vector<pair<int, int>>> graph, int size)
{
    vector<vector<long long>> dist(size, vector<long long>(size, INF));

    for (int vertex = 0; vertex < graph.size(); vertex++)
    {
        for (int edge = 0; edge < graph[vertex].size(); edge++)
        {
            auto edgeData = graph[vertex][edge];
            dist[vertex][edgeData.first] = min((long long)edgeData.second, dist[vertex][edgeData.first]);
        }
    }

    for (int connection = 0; connection < size; connection++)
    {
        for (int start = 0; start < size; start++)
        {
            for (int end = 0; end < size; end++)
            {
                long long initialDistance = dist[start][end];
                long long alternativeDistance = (dist[start][connection] < INF && dist[connection][end] < INF) ? (dist[start][connection] + dist[connection][end]) : INF;
                dist[start][end] = min(initialDistance, alternativeDistance);
            }
        }
    }

    return dist;
}

int main()
{
    int n, m;
    cin >> n >> m;
    vector<vector<pair<int, int>>> graph(n);

    for (int i = 0; i < m; i++)
    {
        int f, s;
        cin >> f >> s;
        f--; s--;
        graph[f].push_back({ s,0 });
        graph[s].push_back({ f,1 });
    }

    auto dist = floyd(graph, n);
    auto max = INT_MIN;

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (dist[i][j] > max && dist[i][j] < INF)
            {
                max = dist[i][j];
            }
        }
    }

    cout << max;
}