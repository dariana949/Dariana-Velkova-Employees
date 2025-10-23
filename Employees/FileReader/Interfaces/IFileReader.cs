using System.IO;

namespace Employees.FileReader.Interfaces
{
	public interface IFileReader
	{
		Task<StreamReader> CreateReader(string filePath);
	}
}
