class Graph:
	def __init__(self):
		self.matrix = [[]]
	
	def get_nodes(self):
		return (nodes for nodes in range(0, len(self.matrix))) # don't know if works

	def insert_node(self, node):
		return None

	def insert_edge(self, node_a, node_b, value = 0):
		return None

	def get_adjacent_nodes(self, node):
		return None
	
	def size(self):
		return None

# ----------------------------------------------------------------

N_OF_NODES = 10
EDGES = [[0, 1], [1, 2], [2, 0], [2, 1], [3, 2], [4, 5], [5, 4]]
g = Graph()

for node_a, node_b in EDGES:
	g.insert_node(node_a)
	g.insert_node(node_b)
	g.insert_edge(node_a, node_b)

