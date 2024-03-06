using System;

public class LinkedList
{
	private Node head;
	public int Count { get; private set; }


	public LinkedList()
	{
		head = null;
		Count = 0;
	}


	public LinkedList(int size)
	{
		head = null;
		Count = size;
	}


	public int this[int index]
	{
		get
		{
			if (index < 0 || index > Count)
				throw new IndexOutOfRangeException();		

			var tmpNode = head;

			for (int i = 0; i < index; i++)
				tmpNode = tmpNode.NextNode;

			return tmpNode.Value;
		}

		set
		{
			if (index < 0 || index > Count)
				throw new IndexOutOfRangeException();

			var tmpNode = head;

			for (int i = 0; i < index; i++)
				tmpNode = tmpNode.NextNode;

			tmpNode.Value = value;
		}
	}


	public void Add(int value)
	{
		var newNode = new Node(value);
		Count++;

		if (head == null)
		{
			head = newNode;
			return;
		}

		var tmpNode = head;

		while (tmpNode.NextNode != null)	
			tmpNode = tmpNode.NextNode;

		tmpNode.NextNode = newNode;
	}


	public void RemoveAt(int index)
	{
		if (index < 0 || index > Count)
			throw new IndexOutOfRangeException();

		var tmpNode = head;
		Count--;

		for (int i = 0; i < index - 1; i++)
			tmpNode = tmpNode.NextNode;

		tmpNode.NextNode = tmpNode.NextNode.NextNode;
	}
}


public class Node
{
	public int Value { get; set; }
	public Node NextNode { get; set; }

	public Node(int value)
	{
		Value = value;
	}
}