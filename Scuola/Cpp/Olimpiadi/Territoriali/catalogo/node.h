#ifndef NODE_H
#define NODE_H


class Node
{
public:
	long long int key, count;
	Node *left, *right;

	Node(long long int key);
};

#endif