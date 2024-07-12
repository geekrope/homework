//https://acmp.ru/index.asp?main=task&id_task=795

#include <iostream>
#include <vector>
#include <cmath>

using namespace std;

int main()
{
    int n;
    cin >> n;
    vector<int> awards = vector<int>(n, 0);

    for (int i = 0; i < n; i++)
    {
        int award;
        cin >> award;
        awards[i] = award;
    }

    int a, b, k;
    cin >> a >> b >> k;

    int m = -1;

    int initialSpins = a % k == 0 ? (a / k - 1) : (a / k);

    for (int i = 0; (a + i * k) <= b && i <= n; i++)
    {
        auto award = awards[(initialSpins + i) % n];
        m = max(award, m);
    }

    for (int i = 0; abs(-a - i * k) <= b && i <= n; i++)
    {
        auto ind = (-initialSpins - i) % n;

        if (ind < 0)
        {
            ind = n + ind;
        }

        m = max(awards[ind], m);
    }

    std::cout << m;
}