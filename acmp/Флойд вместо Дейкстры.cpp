//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=32&id_topic=52&id_problem=1022

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
            dist[vertex][edgeData.first] = edgeData.second;
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
    int n, s, t;
    cin >> n >> s >> t;
    s--; t--;
    vector<vector<pair<int, int>>> graph(n);

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            int weight;
            cin >> weight;
            if (weight >= 0)
            {
                graph[i].push_back({ j,weight });
            }
        }
    }

    auto dist = floyd(graph, n);

    cout << (dist[s][t] < INF ? dist[s][t] : -1);
}