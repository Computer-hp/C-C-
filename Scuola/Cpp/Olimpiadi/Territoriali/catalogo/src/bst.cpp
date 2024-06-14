#include "bst.h"

using namespace std;


BST::BST()
{
	root = nullptr;
}


void BST::insert(long long int key)
{
	root = insert(root, key);
}

Node* BST::insert(Node *node, long long int key)
{
	if (node == nullptr) return new Node(key);

	if (key < node->key)  node->left = insert(node->left, key);

	else if (key > node->key)  node->right = insert(node->right, key);

	else node->count++;

	return node;
}


bool BST::search(long long int key)
{
	return (search(root, key) != nullptr);
}

Node* BST::search(Node *node, long long int key)
{
	if (node == nullptr || node->key == key) return node;

	if (key < node->key) return search(node->left, key);

	return search(node->right, key);
}


void BST::delete_node(long long int key)
{
	root = delete_node(root, key);
}

Node* BST::delete_node(Node *root, long long int key)
{
	if (root == nullptr) return root;

	if (key < root->key) root->left = delete_node(root->left, key);
	
	else if (key > root->key) root->right = delete_node(root->right, key);

	else
	{
		if (root->count > 1)
		{
			root->count--;
			return root;
		}

		if (root->left == nullptr)
		{
			Node *temp = root->right;
			delete root;
			return temp;
		}

		if (root->right == nullptr)
		{
			Node *temp = root->left;
			delete root;
			return temp;
		}

		Node *temp = min_value_node(root->right);
		root->key = temp->key;
		root->count = temp->count;
		root->right = delete_node(root->right, temp->key);
	}

	return root;
}

Node* BST::min_value_node(Node *node)
{
	Node *current = node;
	
	while (current && current->left != nullptr)
		current = current->left;

	return current;
}


long long int BST::get_count(long long int key)
{
	Node *node = search(root, key);

	if (node != nullptr) return node->count;

	return 0;
}