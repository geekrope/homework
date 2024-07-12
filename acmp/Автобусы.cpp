//https://acmp.ru/index.asp?main=task&id_task=134

# include <iostream>
# include <set>
# include <vector>
# include <climits>

using namespace std;

const int INF = 10001;

vector<int> dijkstra(vector<vector<vector<int>>> graph, int start)
{
    const int size = graph.size();

    vector<int> arrival(size, INF);
    vector<bool> used(size);
    set < pair < int, long long>> unused;

    arrival[start] = 0;
    unused.insert({ 0, start });

    while (unused.size() > 0)
    {
        auto current = (*unused.begin()).second;
        used[current] = true;
        unused.erase(unused.begin());

        for (auto adjecent : graph[current])
        {
            auto vertexIndex = adjecent[0];
            auto departureTime = adjecent[1];
            auto arrivalTime = adjecent[2];

            if (arrival[current] <= departureTime)
            {
                auto currentArrivalTime = arrival[vertexIndex];
                auto alternativeArrivalTime = arrivalTime;

                auto newArrival = currentArrivalTime == INF ? alternativeArrivalTime : min(currentArrivalTime, alternativeArrivalTime);
                auto vertexItterator = unused.find({ arrival[vertexIndex], vertexIndex });

        if (!used[vertexIndex])
        {
            if (vertexItterator != unused.end())
            {
                unused.erase(vertexItterator);
            }
            unused.insert({ newArrival, vertexIndex });
        }

        arrival[vertexIndex] = newArrival;
    }
}
    }
 
    return arrival;
}
 
int main()
{
    int n, d, v, m;
    cin >> n >> d >> v >> m;
    d--; v--;
    vector<vector<vector<int>>> graph(n); //vertex, departureTime, arrivalTime

    for (int i = 0; i < m; i++)
    {
        int departurePoint, arrivalPoint, departureTime, arrivalTime;
        cin >> departurePoint >> departureTime >> arrivalPoint >> arrivalTime;
        departurePoint--; arrivalPoint--;
        graph[departurePoint].push_back({ arrivalPoint, departureTime, arrivalTime });
    }
 
    auto arrival = dijkstra(graph, d);

cout << (arrival[v] == INF ? -1 : arrival[v]);
}