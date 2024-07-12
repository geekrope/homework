//https://acmp.ru/asp/do/index.asp?main=task&id_course=3&id_section=24&id_topic=164&id_problem=1065

#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main()
{
    int n;
    cin >> n;
    vector<int> prizes(n);

    for (int i = 0; i < n; i++)
    {
        int worth;
        cin >> worth;
        prizes[i] = worth;
    }

    int currMax = max(prizes[0], prizes[1]);
    int prevMax = min(prizes[0], prizes[1]);

    for (int k = 2; k < n; k++)
    {
        cout << prevMax << ' ';

        if (prizes[k] > currMax)
        {
            prevMax = currMax;
            currMax = prizes[k];
        }
        else if (prizes[k] > prevMax)
        {
            prevMax = prizes[k];
        }
    }

    cout << prevMax << ' ';
}