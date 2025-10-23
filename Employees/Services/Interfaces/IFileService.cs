using Employees.Models;

namespace Employees.Services.Interfaces
{
	public interface IFileService
	{
		public void Validate(IFormFile formFile);

		public Task<EmployeeInfoOut> ReadCsvFile(IFormFile formFile);

		public EmployeeInfoOut PrintTeamWorkData(Dictionary<string, List<string>> teamWorkDictionary);
	}
}
