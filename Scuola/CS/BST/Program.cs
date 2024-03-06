public class BSTNode
{
	public int Value { get; set; }
	public BSTNode Left { get; set; }
	public BSTNode Right { get; set; }


	public BSTNode(int value)
	{
		Value = value;
		Left = Right = null;
	}
}


public class BST
{
	private BSTNode root;

	public BST() { root = null; }

	public BST(int value)
	{
		root = new BSTNode(value);
	}


	public void Insert(int value)
	{
		root = InsertRec(root, value);;
	}


	private BSTNode InsertRec(BSTNode root, int value)
	{
		if (root == null)
		{
			root = new BSTNode(value);
			return root;
		}
		else if (value < root.Value)
			root.Left = InsertRec(root.Left, value);

		else if (value > root.Value)
			root.Right = InsertRec(root.Right, value);

		return root;
	}


	public void PreOrder(BSTNode root)
	{
		if (root == null) return;

		Console.Write($" {root.Value}");
		PreOrder(root.Left);
		PreOrder(root.Right);
	}


	public void InOrder(BSTNode root)
	{
		if (root == null) return;

		InOrder(root.Left);
		Console.Write($" {root.Value}");
		InOrder(root.Right);
	}


	public void PostOrder(BSTNode root)
	{
		if (root == null) return;

		PostOrder(root.Left);
		PostOrder(root.Right);
		Console.Write($" {root.Value}");
	}


	public void Print()
	{
		PreOrder(root);
		Console.Write("\n\n\n\n");
		InOrder(root);
		Console.Write("\n\n\n\n");
		PostOrder(root);
	}
}



public class Program
{
	static void Main()
	{
		BST tree = new(4);
		BST tree2 = new(5);

		for (int i = 0; i < 100; i++)
			tree.Insert(i);

		tree.Print();
		
	}
}