namespace Tasks.Sorts
{
	internal class BinaryTree<T> where T : IComparable
	{
		public T Data { get; set; }
		public BinaryTree<T>? Left { get; set; } = null;
		public BinaryTree<T>? Right { get; set; } = null;

		public BinaryTree(T data) => Data = data;

		public void Insert(BinaryTree<T> node, Comparer<T> comparer)
		{
			if (comparer.Compare(node.Data, Data) > 0)
			{
				if (Left == null) Left = node;
				else Left.Insert(node, comparer);
			}
			else
			{
				if (Right == null) Right = node;
				else Right.Insert(node, comparer);
			}
		}

		public T[] Transform(List<T>? elements = null)
		{
			elements ??= new List<T>();

			Left?.Transform(elements);
			elements.Add(Data);
			Right?.Transform(elements);

			return elements.ToArray();
		}
	}

	public class TreeSort<T> :ASort<T> where T : IComparable
	{
		public override Comparer<T> Comparer { get; set; } = new Comparer<T>();

		public override T[] Sorting(T[] data)
		{
			var tree = new BinaryTree<T>(data[0]);
			foreach(var item in data[1..])
				tree.Insert(new BinaryTree<T>(item), Comparer);

			return tree.Transform();
		}
	}
}
