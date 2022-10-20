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

		public void ResizeAndReorderStack()
		{
			// Create a new array with 1 extra space.
			T[] array = new T[Count + 1];

			for (int index = 0; index < Count; index++)
			{
				/* Push the items forward by one, leaving the top of the stack
				 * empty, so it can be assigned-to later one.
				 */
				array[index + 1] = Stack[index];
			}

			Stack = array;
		}

		public T[] ResizeAndReorderStack(T[] array, int count)
		{
			T[] newArray = new T[count + 1];

			for (int index = 0; index < count; index++)
			{
				newArray[index + 1] = array[index];
			}

			return newArray;
		}

		/// <summary>
		/// Returns each item in the Stack.
		/// </summary>
		/// <returns>Each item in the Stack.</returns>
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			foreach (T item in Stack)
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
			ResizeAndReorderStack();
			Stack[0] = item;
		}

		public T Peek()
		{
			ThrowIndexOutOfRangeException(0);
			return Stack[0];
		}

		public T Peek(int index)
		{
			ThrowIndexOutOfRangeException(index);
			return Stack[index];
		}

		public T[] Reverse()
		{
			T[] array = Stack;

			if (Count >= 1)
			{
				array = (T[])Stack.Reverse<T>();
			}

			return array;
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

			switch (position)
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

		public T[] Get(int[] indexes)
		{
			T[] array = new T[indexes.Length];

			foreach (int index in indexes)
			{
				if (index < 0 || index > Count)
				{
					ThrowIndexOutOfRangeException(index);
				}
				else
				{
					ResizeAndReorderStack(array, indexes.Length);
					array[index] = Stack[index];
				}
			}

			return array;
		}

		private void ThrowIndexOutOfRangeException(int index)
		{
			if (index < 0 || index > Count)
			{
				throw new IndexOutOfRangeException();
			}
		}
	}
}