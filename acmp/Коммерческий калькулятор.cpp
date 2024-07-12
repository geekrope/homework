//https://acmp.ru/index.asp?main=task&id_task=262

#include <iostream>
#include <set>
#include <iomanip>
#include <map>
#include <cmath>

void add(std::set<long long>* s, std::map<long long, long long>* m, long long val)
{
    if (s->find(val) == s->end())
    {
        s->insert(val);
        m->insert({ val,1 });
    }
    else
    {
        (*m)[val]++;
    }
}

long long next(std::set<long long>* s, std::map<long long, long long>* m)
{
    auto first = *s->begin();

    if ((*m)[first] > 1)
    {
        (*m)[first]--;
    }
    else
    {
        s->erase(first);
        m->erase(first);
    }

    return first;
}

int main() {
    long long n;
    std::cin >> n;
    std::set<long long> prices;
    std::map<long long, long long> pricesCount;
    double fee = 0;

    for (long long i = 0; i < n; i++)
    {
        long long inp;
        std::cin >> inp;
        add(&prices, &pricesCount, inp);
    }

    while (n > 1)
    {
        auto sum = next(&prices, &pricesCount) + next(&prices, &pricesCount);
        fee += sum * 0.05;
        add(&prices, &pricesCount, sum);
        n--;
    }

    std::cout << std::fixed;
    std::cout << std::setprecision(2);
    std::cout << fee;
}