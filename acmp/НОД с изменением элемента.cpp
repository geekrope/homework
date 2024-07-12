//https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=20&id_topic=47&id_problem=607

#include <iostream>
#include <vector>

using namespace std;

struct Node
{
    int value, priority, size, value2;
    Node* left;
    Node* right;

    Node(int value, Node* leftChild = nullptr, Node* rightChild = nullptr)
    {
        this->value = value;
        this->priority = rand();
        this->size = 1;
        this->left = leftChild;
        this->right = rightChild;
        this->value2 = value;
    }
};

int gcd(int a, int b)
{
    if (a == 0)
    {
        return b;
    }
    if (b == 0)
    {
        return a;
    }

    return gcd(b, a % b);
}

void updateValue(Node* root)
{
    auto val1 = root->left == nullptr ? 0 : root->left->value2;
    auto val2 = root->right == nullptr ? 0 : root->right->value2;

    root->value2 = gcd(gcd(val1, val2), root->value);
}

void print(Node* root)
{
    if (root == nullptr)
    {
        return;
    }
    else
    {
        print(root->left);
        cout << root->value << ' ';
        print(root->right);
    }
}

int getSize(Node* root)
{
    return root == nullptr ? 0 : root->size;
}

void updateSize(Node* root)
{
    root->size = getSize(root->left) + getSize(root->right) + 1;
}

Node* merge(Node* node1, Node* node2)
{
    if (node1 == nullptr)
    {
        return node2;
    }
    else if (node2 == nullptr)
    {
        return node1;
    }
    else if (node1->priority < node2->priority)
    {
        node1->right = merge(node1->right, node2);
        updateValue(node1);
        updateSize(node1);

        return node1;
    }
    else
    {
        node2->left = merge(node1, node2->left);
        updateValue(node2);
        updateSize(node2);

        return node2;
    }
}

pair<Node*, Node*> split(Node* root, int i)
{
    if (root == nullptr)
        return { nullptr, nullptr };
    if (getSize(root) <= i)
        return { root, nullptr };
    if (i == 0)
        return { nullptr, root };

    if (i <= getSize(root->left))
    {
        auto split1 = split(root->left, i);
        root->left = split1.second;
        updateValue(root);
        updateSize(root);

        return { split1.first, root };
    }
    else
    {
        auto split1 = split(root->right, i - getSize(root->left) - 1);
        root->right = split1.first;
        updateValue(root);
        updateSize(root);

        return { root, split1.second };
    }
}

Node* push(Node* root, int value)
{
    auto newNode = new Node(value);

    return merge(root, newNode);
}

Node* insert(Node* root, int value, int i)
{
    auto spl = split(root, i);
    auto newNode = new Node(value);

    return merge(merge(spl.first, newNode), spl.second);
}

Node* erase(Node* root, int i)
{
    auto spl1 = split(root, i);
    auto spl2 = split(spl1.second, i - getSize(spl1.first) + 1);

    return merge(spl1.first, spl2.second);
}

Node* update(Node* root, int i, int val)
{
    root = erase(root, i);
    root = insert(root, val, i);

    return root;
}

int query(Node* root, int l, int r)
{
    auto spl1 = split(root, l);
    auto spl2 = split(spl1.second, r - l + 1);
    auto answ = spl2.first->value2;

    root = merge(spl1.first, merge(spl2.first, spl2.second));

    return answ;
}

int main()
{
    int n, m;
    std::cin >> n;

    Node* root = nullptr;

    for (int i = 0; i < n; i++)
    {
        int val;
        std::cin >> val;

        if (root == nullptr)
        {
            root = new Node(val);
        }
        else
        {
            root = push(root, val);
        }
    }
    std::cin >> m;
    vector<int> out;

    for (int i = 0; i < m; i++)
    {
        char command;
        std::cin >> command;

        if (command == 'g')
        {
            int l, r;
            std::cin >> l >> r;
            l--; r--;

            out.push_back(query(root, l, r));
        }
        else
        {
            int i, v;
            std::cin >> i >> v;
            i--;

            root = update(root, i, v);
        }
    }

    for (int o : out)
    {
        std::cout << o << endl;
    }
}