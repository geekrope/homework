//https://acmp.ru/index.asp?main=task&id_task=133

# include <iostream>
# include <set>
# include <vector>

using namespace std;

vector<long long> dijkstra(vector<vector<pair<int, int>>> graph, int start)
{
    const long long INF = INT_MAX;
    const long long size = graph.size();

    vector < long long> dist(size, INF);
    vector<bool> used(size);
    set < pair < int, long long>> unused;

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
    cin >> n;
    vector<int> prices(n);
    vector<vector<pair<int, int>>> graph(n);

    for (int i = 0; i < n; i++)
    {
        int price;
        cin >> price;

        prices[i] = price;
    }

    cin >> m;

    for (int i = 0; i < m; i++)
    {
        int first, second;
        cin >> first >> second;
        first--; second--;
        graph[first].push_back({ second, prices[first] });
graph[second].push_back({ first, prices[second] });
    }
 
    auto dist = dijkstra(graph, 0);

cout << (dist[n - 1] == INT_MAX ? -1 : dist[n - 1]);
}