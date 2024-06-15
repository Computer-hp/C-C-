#include "node.h"


Node::Node(long long int key)
{
	this->key = key;
	count = 1;
	left = nullptr;
	right = nullptr;
}