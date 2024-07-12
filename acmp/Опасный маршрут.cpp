//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=32&id_topic=52&id_problem=1029

#include <iostream>
#include <vector>

using namespace std;

const double INF = 2;

vector<vector<double>> floyd(vector<vector<pair<int, double>>> graph, int size)
{
    vector<vector<double>> dist(size, vector<double>(size, INF));

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
                double initialDistance = dist[start][end];
                double alternativeDistance = (dist[start][connection] < INF && dist[connection][end] < INF) ? (dist[start][connection] + dist[connection][end] - dist[start][connection] * dist[connection][end]) : INF;
                dist[start][end] = min(initialDistance, alternativeDistance);
            }
        }
    }

    return dist;
}

int main()
{
    int n, m, f, s;
    cin >> n >> m >> f >> s;
    f--; s--;
    vector<vector<pair<int, double>>> graph(n);

    for (int i = 0; i < m; i++)
    {
        int first, second, weight;
        cin >> first >> second >> weight;
        first--; second--;
        graph[first].push_back({ second,weight / 100.0 });
        graph[second].push_back({ first ,weight / 100.0 });
    }

    auto dist = floyd(graph, n);

    cout << dist[f][s];
}