queue = []

def bfs(graph, node, visited_nodes):
	visited_nodes.append(node)
	queue.append(node)

	while queue:  # while !queue.emtpy()
		node = queue.pop(0)

		for neighbour in graph[node]:
			if neighbour not in visited_nodes:
				visited_nodes.append(neighbour)
				queue.append(neighbour)