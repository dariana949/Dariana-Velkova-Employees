using Employees.Factory.Interfaces;

namespace Employees.Factory
{
	public class CollectionFactory : ICollectionFactory
	{
		public List<string> CreateList()
		{
			return [];
		}

		public Dictionary<string, List<string>> CreateDictionary()
		{
			return [];
		}

		public Dictionary<string, List<T>> CreateCustomDictionary<T>()
		{
			return [];
		}
	}
}
