//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=20&id_topic=44&id_problem=290

#include <iostream>
#include <string>
#include <vector>

using namespace std;

int main()
{
    int n, cur, m;

    cin >> n >> cur;

    auto prefix = new long long int[n];
    prefix[0] = cur;

    for (int index = 1; index < n; index++)
    {
        cin >> cur;
        prefix[index] = prefix[index - 1] + cur;
    }

    cin >> m;

    auto out = new long long int[m];

    for (int i = 0; i < m; i++)
    {
        int f, s;
        cin >> f >> s;
        f--; s--;

        out[i] = prefix[s] - ((f - 1) >= 0 ? (prefix[f - 1]) : 0);
    }

    for (int i = 0; i < m; i++)
    {
        cout << out[i] << endl;
    }
}