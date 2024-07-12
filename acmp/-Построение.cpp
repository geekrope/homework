// https://informatics.msk.ru/mod/statements/view.php?chapterid=1363#1
// https://acmp.ru/asp/do/index.asp?main=task&id_course=2&id_section=20&id_topic=46&id_problem=601

#include <iostream>
#include <vector>
#include <random>
#include <string>

using namespace std;

struct Node
{
    int value, priority, size;
    Node* left; Node* right;

    Node(int value, Node* left, Node* right)
    {
        this->value = value;
        this->left = left;
        this->right = right;
        this->size = 1;
        this->priority = rand();
    }

    ~Node()
    {
        if (left)
            delete[] left;
        if (right)
            delete[] right;
    }
};

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

int getSize(Node* node)
{
    return node == nullptr ? 0 : node->size;
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
    else
    {
        if (node1->priority < node2->priority)
        {
            node1->right = merge(node1->right, node2);

            updateSize(node1);

            return node1;
        }
        else
        {
            node2->left = merge(node1, node2->left);

            updateSize(node2);

            return node2;
        }
    }
}

pair<Node*, Node*> split(Node* node, int value)
{
    if (node == nullptr)
    {
        return { nullptr, nullptr };
    }
    else
    {
        if (node->value < value)
        {
            auto splitRes = split(node->right, value);
            node->right = splitRes.first;

            updateSize(node);

            return { node , splitRes.second };
        }
        else
        {
            auto splitRes = split(node->left, value);
            node->left = splitRes.second;

            updateSize(node);

            return { splitRes.first , node };
        }
    }
}

bool exists(Node* root, int value)
{
    if (root == nullptr)
    {
        return false;
    }
    else
    {
        return exists(root->left, value) || exists(root->right, value);
    }
}

Node* insert(Node* root, int value)
{
    if (exists(root, value))
    {
        throw "exists";
    }
    else
    {
        auto splitRes = split(root, value);
        auto newNode = new Node(value, nullptr, nullptr);

        return merge(merge(splitRes.first, newNode), splitRes.second);
    }
}

Node* erase(Node* root, int value)
{
    auto split1 = split(root, value);
    auto split2 = split(root, value + 1);

    return merge(split1.first, split2.second);
}

Node* findByIndex(Node* root, int index)
{
    if (root == nullptr)
    {
        throw "err";
        return nullptr;
    }
    else if (getSize(root->right) > index)
    {
        return findByIndex(root->right, index);
    }
    else if (getSize(root->right) == index)
    {
        return root;
    }
    else
    {
        return findByIndex(root->left, index - getSize(root->right) - 1);
    }
}

int findIndex(Node* root, int value)
{
    if (root == nullptr)
    {
        throw "err";
        return -1;
    }
    else if (root->value == value)
    {
        return getSize(root->right);
    }
    else
    {
        if (root->value > value)
        {
            return findIndex(root->left, value) + getSize(root->right) + 1;
        }
        else
        {
            return findIndex(root->right, value);
        }
    }
}

int main()
{
    int n;
    vector<int> out;
    cin >> n;
    Node* root = nullptr;

    for (int i = 0; i < n; i++)
    {
        int c, v;
        cin >> c >> v;

        if (c == 1)
        {
            if (root == nullptr)
            {
                root = new Node(v, nullptr, nullptr);
            }
            else
            {
                root = insert(root, v);
            }
 
            out.push_back(findIndex(root, v));
        }
        else
        {
            root = erase(root, findByIndex(root, v)->value);
        }
    }

    for (auto o : out)
    {
        cout << o << endl;
    }
}