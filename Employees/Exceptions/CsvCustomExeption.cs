namespace Employees.Exceptions
{
	public class CsvCustomExeption : Exception
	{
		public CsvCustomExeption()
		{
		}

		public CsvCustomExeption(string message) : base(message)
		{
		}

		public CsvCustomExeption(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
