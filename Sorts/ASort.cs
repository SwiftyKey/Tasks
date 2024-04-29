namespace Tasks.Sorts
{
	public class Comparer<T> where T : IComparable
	{
		public bool Descending { get; set; } = false;
		public Func<T, T, int> Predicate { get; set; } = (x, y) => y.CompareTo(x);
		public int Compare(T x, T y)
		{
			return Predicate(x, y) * (Descending ? -1 : 1);
		}
	}

	public abstract class ASort<T> where T: IComparable
	{
		public abstract Comparer<T> Comparer{ get; set; }
		public abstract T[] Sorting(T[] data);
	}
}
