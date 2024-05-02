namespace Tasks.Sorts
{
	public class QuickSort<T> :ASort<T> where T : IComparable
	{
		public override Comparer<T> Comparer { get; set; } = new Comparer<T>();

		private int Partition(T[] data, int start, int end)
		{
			var pivot = data[end];
			var storeIndex = start;

			for (int i = start; i < end; i++)
				if (Comparer.Compare(data[i], pivot) > 0)
				{
					(data[i], data[storeIndex]) = (data[storeIndex], data[i]);
					storeIndex++;
				}

			(data[end], data[storeIndex]) = (data[storeIndex], data[end]);

			return storeIndex;
		}

		private T[] Sorting(T[] data, int start, int end)
		{
			if (start >= end) return data;

			var pivotIndex = Partition(data, start, end);
			Sorting(data, start, pivotIndex - 1);
			Sorting(data, pivotIndex + 1, end);

			return data;
		}

		public override T[] Sorting(T[] data)
		{
			var newData = new T[data.Length];
			Array.Copy(data, newData, data.Length);

			return Sorting(newData, 0, data.Length - 1);
		}
	}
}
