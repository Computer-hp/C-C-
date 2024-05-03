using System;

namespace MySpace
{
	class MatGraph
	{
		private int[,] adjMatrix;

		public MatGraph(int nNodes)
		{
			adjMatrix = new int[nNodes, nNodes];
		}	


		public void AddEdge(int i, int j)
		{
			adjMatrix[i, j] = 1;
			adjMatrix[j, i] = 1;
		}

		public void RemoveEdge(int i, int j)
		{
			adjMatrix[i, j] = 0;
			adjMatrix[j, i] = 0;
		}


		public bool HasEdge(int i, int j)
		{
			return adjMatrix[i, j] == 1 ? true : false;
		}


		public List<int> GetAdjacentNodes(int i)
		{
			List<int> adjacentNodes = new List<int>();

			for (int j = 0; j < Size(); ++i)
				if (adjMatrix[i, j] > 0)
					adjacentNodes.Add(j);
			
			return adjacentNodes;
		}


		public int Size()
		{
			return adjMatrix.GetLength(0);
		}
	}
}