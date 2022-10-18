﻿using System.Collections;

namespace StackList
{
	public class StackList<T> : IEnumerable<T>
	{
		public enum StackPositions
		{
			Top = 0,
			Bottom
		}

		public StackPositions StackPosition { get; set; }

		private T[] Stack;

		public int Count { get { return Stack.Length; } }

		public StackList(int capacity = 0)
		{
			Stack = new T[capacity];
		}

		public void ResizeStack()
		{
			T[] array = new T[Count + 1];

			for (int index = 0; index < Count; index++)
			{
				array[index + 1] = Stack[index];
			}

			Stack = array;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			foreach(T item in Stack)
			{
				yield return item;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Stack.GetEnumerator();
		}

		public void Push(T item)
		{
			ResizeStack();
			Stack[0] = item;
		}

		public T Peek()
		{
			ThrowIndexOutOfRangeException(0);
			return Stack[0];
		}

		public void Pop()
		{
			Stack = Stack.Take<T>(new Range(1, Count)).ToArray<T>();
		}

		public T this[int index]
		{
			get
			{
				ThrowIndexOutOfRangeException(index);
				return (T)Stack[index];
			}
		}

		public T Get(StackPositions position)
		{
			dynamic item = String.Empty;

			switch(position)
			{
				case StackPositions.Top:
					if (Stack[0] != null)
					{
						item = Stack[0];
					}
					else
					{
						ThrowIndexOutOfRangeException(0);
					}
					break;
				case StackPositions.Bottom:
					if (Stack[Count - 1] != null)
					{
						item = Stack[Count - 1];
					}
					else
					{
						ThrowIndexOutOfRangeException(Count - 1);
					}
					break;
			}

			return item;
		}

		public T Get(int index)
		{
			ThrowIndexOutOfRangeException(index);
			return Stack[index];
		}

		private void ThrowIndexOutOfRangeException(int index)
		{
			if (Stack[index] == null)
			{
				throw new IndexOutOfRangeException();
			}
		}
	}
}