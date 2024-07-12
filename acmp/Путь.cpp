//https://acmp.ru/index.asp?main=task&id_task=127

#include <iostream>
#include <vector>
#include <map>
#include <queue>

using namespace std;

vector<int> BFS(vector<vector<int>> graph, int size, int start)
{
    vector<int> dist(size, -1);
    queue<int> suspended;

    dist[start] = 0;

    suspended.push(start);

    while (!suspended.empty())
    {
        auto current = suspended.front();
        suspended.pop();

        for (int adjecent : graph[current])
        {
            if (dist[adjecent] == -1)
            {
                dist[adjecent] = dist[current] + 1;
                suspended.push(adjecent);
            }
        }
    }

    return dist;
}

int main()
{
    int n;
    cin >> n;
    vector<vector<int>> graph(n);

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            int has;
            cin >> has;

            if (has == 1)
            {
                graph[i].push_back(j);
                graph[j].push_back(i);
            }
        }
    }

    int u, v;
    cin >> u >> v;
    u--; v--;
    auto dist = BFS(graph, n, u);

    cout << dist[v];
}