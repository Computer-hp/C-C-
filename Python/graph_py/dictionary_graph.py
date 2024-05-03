class Graph:

	#			  <---			  dictionary_1			   --->
	# graph  -->  { source_node : { destination_node : cost } }
	# 							  <---    dictionary_2   --->

	def __init__(self):
		self.nodes = {}  # the graph (dictionary_1) will have as 'keys' source nodes of the graph.
						 # the keys will point to dictionary_2.
	   					 # dictionary_2 will have as 'keys' destination nodes and as 'value' the cost to get to them.


	# returns the keys of dictionary 'nodes'
	def get_nodes(self):
		return self.nodes.keys()
	
	def insert_node(self, node):
		if node not in self.nodes:
			self.nodes[node] = {}  # we create a new dict for the new node, which is also a new key.

	def insert_edge(self, node_a, node_b, value = 0):	# value == 0 beacuse it's an 'int'
		self.insert_node(node_a)
		self.insert_node(node_b)
		self.nodes[node_a][node_b] = value  # we enter into dictionary_1 --> 'nodes'.
											# we enter into value of the key 'node_a', it's value is another dictionary (dictionary_2)
											# inside dictionary_2 we assign to it's key the variable 'value'
	

	# returns dictionary2 --> { nodes : value }, which is poited by the 'node' passed as parameter
	def get_adjacent_nodes(self, node):
		if node in self.nodes:
			return self.nodes[node]		

	# returns number of 'nodes' in dictionary1
	def size(self):
		return len(self.nodes)	

# --------------------------------------------------------------------------------------

import pandas as pd

N_OF_NODES = 10
EDGES = [[0, 1], [1, 2], [2, 0], [2, 1], [3, 2], [4, 5], [5, 4]]
g = Graph()

for node_a, node_b in EDGES:
	g.insert_edge(node_a, node_b)

'''
print("nodes = " + str(g.get_nodes()))
print("adjacent_nodes = " + str(g.get_adjacent_nodes(2)))
print("graph size = " + str(g.size()))
'''

print("\nnodes = " + str(list(g.get_nodes())))
print("adjacent_nodes = " + str(g.nodes))
print("graph size = " + str(g.size()))

# ----------------------------------------------------------------------------------------

print("\n##################################################")

# BFS examples

import bfs
import random

visited_nodes = []
nodes = list(g.get_nodes())

bfs.bfs(g.nodes, nodes[random.randint(0, g.size() - 1)], visited_nodes)
print("\nvisited_nodes = " + str(visited_nodes))  # output: [0, 1, 2]  --> some nodes are missing because they are not connected with others

# ----------------------------------------------------------------------------------------

print("\n##################################################")

# BFS