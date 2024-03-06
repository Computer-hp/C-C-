using System;

class Queue
{
	public List<int> Elements { get; }
	public int Front { get; private set; } = -1;
	public int Rear { get; private set; } = -1;
	public int Capacity { get; private set; }
	public int Size { get; private set; }


	public Queue()
	{
		Capacity = 0;
	}


	public Queue(int capacity)
	{
		Capacity = capacity;
	}


	public void Enqueue(int value)
	{
		if (Front == -1)
			Front++;

		Rear++;

		if (IsEmpty() || IsFull())
		{
			Elements.Add(value);
			return;
		}
		
		Elements[Rear] = value;
	}


	public int Dequeue()
	{
		if (IsEmpty())
			return;

		var element = Elements[Front];
		Elements.RemoveAt(Front);

		if (Front == Rear)
			Front = Rear = -1;
		
		else
			Front++;

		return element;
	}


	public int Peek()
	{
		if (IsEmpty())
			return;

		return Elements[Front];
	}


	public bool IsEmpty()
	{
		if (Front == -1)
			return true;

		return false;
	}


	public bool IsFull()
	{
		if (Rear == Capacity - 1)
			return true;

		return false;
	}
}