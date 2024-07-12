//https://acmp.ru/index.asp?main=task&id_task=140

#include <iostream>
#include <vector>
#include <stack>

using namespace std;

const long long INF = INT_MAX;

vector<int> DFS(vector<vector<int>> graph, int size)
{
    vector<bool> used(size, false);
    vector<int> answ;

    while (true)
    {
        int start = -1;
        for (int vert = 0; vert < size; vert++)
        {
            if (!used[vert])
            {
                start = vert;
                break;
            }
        }

        if (start == -1)
        {
            break;
        }
        else
        {
            stack<int> susp;

            susp.push(start);
            answ.push_back(start);

            while (!susp.empty())
            {
                auto curr = susp.top();
                used[curr] = true;
                susp.pop();

                for (auto vert : graph[curr])
                {
                    if (!used[vert])
                    {
                        susp.push(vert);
                    }
                }
            }
        }
    }

    return answ;
}

bool containsNegativeCycle(vector<vector<int>> graph, int size, int start)
{
    vector<long long> dist(size, INF);

    dist[start] = 0;

    for (int k = 0; k < size; k++)
    {
        for (auto edge : graph)
        {
            auto from = edge[0];
            auto to = edge[1];
            auto weight = edge[2];

            if (dist[from] < INF && dist[from] + weight < dist[to])
            {
                dist[to] = dist[from] + weight;

                if (k == size - 1)
                {
                    return true;
                }
            }
        }
    }

    return false;
}

int main()
{
    int n;
    cin >> n;
    vector<vector<int>> graph;
    vector<vector<int>> graph2(n);

    bool has = false;

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            int w;
            cin >> w;
            if (w < 100000)
            {
                graph.push_back({ i, j, w });
                graph2[i].push_back(j);
            }
        }
    }


    vector<int> vertices = DFS(graph2, n);

    for (auto vert : vertices)
    {
        has = has || containsNegativeCycle(graph, n, vert);
    }

    cout << (has ? "YES" : "NO");
}