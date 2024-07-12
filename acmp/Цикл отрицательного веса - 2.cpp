//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=32&id_topic=53&id_problem=1034

#include <iostream>
#include <vector>
#include <stack>
#include <algorithm>

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

vector<int> findNegativeCycle(vector<vector<int>> graph, int size, int start)
{
    vector<int> dist(size, INF);
    vector<int> pred(size, -1);
    vector<int> cycle;
    int vertexOnCycle = -1;

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
                pred[to] = from;
                dist[to] = dist[from] + weight;

                if (k == size - 1)
                {
                    vertexOnCycle = from;
                    break;
                }
            }
        }
    }

    cycle.reserve(size);

    if (vertexOnCycle != -1)
    {
        auto current = vertexOnCycle;
        auto flag = true;
        cycle.push_back(current);

        while (flag)
        {
            current = pred[current];
            cycle.push_back(current);

            if (current == vertexOnCycle)
            {
                flag = false;
            }

        }
    }

    reverse(cycle.begin(), cycle.end());

    return cycle;
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
        auto cycle = findNegativeCycle(graph, n, vert);

        if (cycle.size() > 0)
        {
            cout << "YES" << endl << cycle.size() << endl;
            for (int v : cycle)
            {
                cout << (v + 1) << ' ';
            }

            return 0;
        }
    }

    cout << "NO";

    return 0;
}