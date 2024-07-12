//https://acmp.ru/index.asp?main=task&id_task=535

# include <iostream>
# include <vector>
# include <algorithm>
# include <set>

std::vector < long long> splitByDigits(long long x)
{
    auto digits = std::vector < long long> ();
    while (x > 0)
    {
        digits.push_back(x % 10ll);
        x /= 10ll;
    }
    return digits;
}

long long sum(long long a, long long b)
{
    auto d1 = splitByDigits(a);
    auto d2 = splitByDigits(b);
    long long result = 0;
    long long multiplier = 0;

    for (int i = 0; i < std::max(d1.size(), d2.size()); i++)
    {
        auto s = ((i < d1.size()) ? d1[i] : 0) + ((i < d2.size()) ? d2[i] : 0);
        result += s * (long long)pow(10ll, multiplier);

if (s < 10)
{
    multiplier++;
}
else
{
    multiplier += 2;
}
    }
 
    return result;
}
 
int main()
{
    long long a, b, c;

    std::cin >> a >> b >> c;
    auto res = std::set < long long> ();

    res.insert(sum(sum(a, b), c));
    res.insert(sum(sum(a, c), b));
    res.insert(sum(sum(b, c), a));

    if (res.size() == 1)
    {
        std::cout << "NO" << std::endl;
        std::cout << *res.begin();
    }
    else
    {
        std::cout << "YES" << std::endl;

        for (auto o : res)
        {
    std::cout << o << ' ';
}
    }
}