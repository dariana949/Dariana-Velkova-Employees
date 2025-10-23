namespace Employees.Factory.Interfaces
{
	public interface ICollectionFactory
	{
		List<string> CreateList();

		Dictionary<string, List<string>> CreateDictionary();

		Dictionary<string, List<T>> CreateCustomDictionary<T>();

	}
}
