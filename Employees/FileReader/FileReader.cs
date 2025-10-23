using Employees.FileReader.Interfaces;

namespace Employees.FileReader
{
	public class FileReader : IFileReader
	{
		public async Task<StreamReader> CreateReader(string filePath)
		{
			return new StreamReader(filePath);
		}
	}
}
