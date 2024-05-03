using System;

namespace MySpace
{
	class Program
	{
		private const int N_OF_NODES = 10;

		private static readonly List<Tuple<int, int>> EDGES = new List<Tuple<int, int>>
		{
			Tuple.Create(0, 1), 
			Tuple.Create(1, 2), 
			Tuple.Create(2, 0), 
			Tuple.Create(2, 1), 
			Tuple.Create(3, 2), 
			Tuple.Create(4, 5), 
			Tuple.Create(5, 4)
		};


		static void Main()
		{
			// var g = new List_Graph(10);
			var g = new DictGraph();

			foreach (var t in EDGES)
				g.InsertEdge(t.Item1, t.Item2);

			var nodes = g.GetNodes();

			Console.WriteLine();
			foreach (var k in nodes)
				Console.Write($"{k}, ");

			Console.WriteLine('\n');
			var dictOfNodes = g.GetAdjacentNodes(2);

			foreach (var e in dictOfNodes)
				Console.Write($"{e.Key}, ");


			// ##########################################

			var result = DictGraph.FindNeighboursOfNeighbours(g, 2);

			Console.WriteLine("\nneighbours of neighbours: ");
			foreach (var e in result)
				Console.Write($"{e}, ");
		}
	}
}