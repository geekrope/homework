//https://acmp.ru/index.asp?main=task&id_task=148

#include <iostream>

int gcd(int a, int b)
{
    if (a == 0)
    {
        return b;
    }
    return gcd(b % a, a);
}

int main()
{
    int a, b;
    std::cin >> a >> b;
    std::cout << gcd(a, b);
}