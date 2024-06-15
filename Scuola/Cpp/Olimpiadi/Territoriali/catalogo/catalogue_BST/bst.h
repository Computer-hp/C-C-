#ifndef BST_H
#define BST_H

#include "node.h"


class BST
{
public:
	BST();
	void insert(long long int key);
	bool search(long long int key);
	void delete_node(long long int key);
	long long int get_count(long long int key);

private:
	Node *root;

	Node* insert(Node *node, long long int key);
	Node* search(Node *node, long long int key);
	Node* min_value_node(Node *node);
	Node* delete_node(Node *root, long long int key);
};

#endif