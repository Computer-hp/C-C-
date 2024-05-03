using System;
using System.Collections.Generic;

namespace MySpace
{
	class DictGraph
	{
		public Dictionary<int, Dictionary<int, int>> nodes { get; private set; }

		public DictGraph()
		{
			nodes = new Dictionary<int, Dictionary<int, int>>();
		}


		public ICollection<int> GetNodes()
		{
			return nodes.Keys;
		}


		public void InsertNode(int node)
		{
			if (!nodes.ContainsKey(node))	
				nodes[node] = new Dictionary<int, int>();
		}


		public void InsertEdge(int nodeA, int nodeB, int value = 0)
		{
			InsertNode(nodeA);
			InsertNode(nodeB);
			nodes[nodeA][nodeB] = value;
		}


		public Dictionary<int, int> GetAdjacentNodes(int sourceNode)
		{
			if (!nodes.ContainsKey(sourceNode))	
				return null;

			return nodes[sourceNode];
		}


		public int Size()
		{
			return nodes.Count;
		}


		public void BFS(DictGraph graph, int node, ref List<int> visitedNodes)
		{
			Queue<int> queue = new();
			visitedNodes.Add(node);
			queue.Enqueue(node);

			while (queue.Count > 0)
			{
				node = queue.Dequeue();

				if (!graph.nodes.ContainsKey(node))
					continue;

				foreach (var neighbour in graph.nodes[node])
					if (!visitedNodes.Contains(neighbour.Key))
					{
						visitedNodes.Add(neighbour.Key);
						queue.Enqueue(neighbour.Key);
					}
			}
		}


		public void DFS(DictGraph graph, int node, ref bool[] visitedNodes)
		{
			visitedNodes[node] = true;	

			foreach (var u in graph.nodes[node])
				if (!visitedNodes[u.Key])
					DFS(graph, u.Key, ref visitedNodes);
		}


		public static List<int> FindNeighboursOfNeighbours(DictGraph graph, int node)
		{
			List<int> tmpList = new();

			foreach (var neighbours in graph.nodes[node])
				foreach (var neighbour_of_neighbour in graph.nodes[neighbours.Key])
					if (neighbour_of_neighbour.Key != node)
						tmpList.Add(neighbour_of_neighbour.Key);
			
			return tmpList;
		}
	}
}