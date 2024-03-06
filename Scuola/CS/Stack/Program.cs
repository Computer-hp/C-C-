using System;

public class Stack
{
	public List<int> Elements { get; }
	public int Top { get; private set; } = -1;
	public int Capacity { get; private set; }


	public Stack()
	{
		Elements = new List<int>();
		Capacity = 0;
	}

	public Stack(int capacity)
	{
		Elements = new List<int>();
		Capacity = capacity;
	}


	public void Push(int value)
	{
		Top++;

		if (IsEmpty() || IsFull())
			Elements.Add(value);

		else
			Elements[Top] = value;
	}


	public int Pop()
	{
		if (IsEmpty())
			return -1;

		int value = Elements[Top--];
		Elements.Remove(value);

		return value;
	}


	public int Peek()
	{
		if (IsEmpty())
			return -1;

		return Elements[Top];
	}


	public bool IsEmpty()
	{
		if (Top == -1)
			return true;

		return false;
	}


	public bool IsFull()
	{
		if (Top == Capacity)
			return true;

		return false;
	}
}