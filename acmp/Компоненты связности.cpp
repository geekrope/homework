//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=21&id_topic=50&id_problem=1004

#include <iostream>
#include <vector>
#include <stack>

using namespace std;

vector<vector<int>> findComponents(vector<vector<int>> graph)
{
    const int size = graph.size();
    vector<bool> used(size, false);
    vector<vector<int>> components;
    components.reserve(size);

    for (int vertex = 0; vertex < size; vertex++)
    {
        if (!used[vertex])
        {
            int start = vertex;

            vector<int> component;
            stack<int> suspended;

            used[start] = true;
            component.reserve(size);
            suspended.push(start);
            component.push_back(start);

            while (suspended.size() > 0)
            {
                auto current = suspended.top();
                suspended.pop();

                for (auto adjecent : graph[current])
                {
                    if (!used[adjecent])
                    {
                        used[adjecent] = true;
                        suspended.push(adjecent);
                        component.push_back(adjecent);
                    }
                }
            }

            components.push_back(component);
        }
    }

    return components;
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    int n, m;
    cin >> n >> m;
    vector<vector<int>> graph(n);

    bool has = false;

    for (int i = 0; i < m; i++)
    {
        int s, f;
        cin >> s >> f;
        s--; f--;
        graph[s].push_back(f);
        graph[f].push_back(s);
    }

    auto components = findComponents(graph);

    cout << components.size() << endl;

    for (auto comp : components)
    {
        cout << comp.size() << endl;
        for (auto vert : comp)
        {
            cout << vert + 1 << ' ';
        }
        cout << endl;
    }
}