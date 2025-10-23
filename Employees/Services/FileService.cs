using Employees.Exceptions;
using Employees.Factory.Interfaces;
using Employees.FileReader.Interfaces;
using Employees.Models;
using Employees.Services.Interfaces;
using System.IO;

namespace Employees.Services
{
	public class FileService : IFileService
	{
		private readonly IModelFactory modelFactory;
		private readonly ICollectionFactory collectionFactory;
		private readonly IFileReader fileReader;
		private readonly string AllowedExtentions = ".csv";

		public FileService(ICollectionFactory collectionFactory,
		                   IFileReader fileReader,
						   IModelFactory modelFactory)
		{
			this.collectionFactory = collectionFactory;
			this.fileReader = fileReader;		
			this.modelFactory = modelFactory;
		}
		
		public void Validate(IFormFile formFile)
		{
			if (formFile == null || formFile.Length == 0)
			{
				throw new CsvCustomExeption("File is empty!");
			}

			var extentionType = Path.GetExtension(formFile.FileName).ToLower();
			if (extentionType != AllowedExtentions)
			{
				throw new CsvCustomExeption("Unsupported file type, please upload csv file!");
			}
		}

		public async Task<EmployeeInfoOut> ReadCsvFile(IFormFile formFile)
		{
			Dictionary<string, List<string>> dataTable = collectionFactory.CreateDictionary();
			string[] headers = [];
			string? line;
			int lineIndex = 0;
			int startIndex = 0;

			string filePath = formFile.FileName;
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await formFile.CopyToAsync(fileStream);
			}
			
			using StreamReader streamReader = await fileReader.CreateReader(filePath);
			
				while ((line = streamReader.ReadLine()) != null)
				{
					if (string.IsNullOrWhiteSpace(line))
					{
						throw new CsvCustomExeption("Null or empty!");
					}

					var columns = line.Split(';');

					if (lineIndex == startIndex)
					{
						headers = columns;

						foreach (var header in headers)
						{
							dataTable[header.Trim()] = collectionFactory.CreateList();
						}
					}
					else
					{
						for (int i = 0; i < columns.Length; i++)
						{

							string key = headers[i].Trim();
							string value = columns[i].Trim();

							dataTable[key].Add(value);
						}
					}

					lineIndex++;
			}
			return PrintTeamWorkData(dataTable);

		}

		private Dictionary<string,List<ProjectData>> FilterFileData(Dictionary<string, List<string>> teamWorkDictionary)
		{
			var filterFileData = this.collectionFactory.CreateCustomDictionary<ProjectData>();

			int startIndex = 0;
			string dateMissingValue = "null";
			string filterByProjectID = "ProjectID";
			string employeeID = "EmpID";
			string dateFromInput = "DateFrom";
			string dateToInput = "DateTo";

			for (int i = startIndex; i < teamWorkDictionary[filterByProjectID].Count; i++)
			{
				string projectId = teamWorkDictionary[filterByProjectID][i];
				string empId = teamWorkDictionary[employeeID][i];
				string dateFromAsString = teamWorkDictionary[dateFromInput][i];
				string dateToAsString = teamWorkDictionary[dateToInput][i];

				DateTime dateFrom = DateTime.Parse(dateFromAsString);

				DateTime dateTo = dateToAsString == dateMissingValue ? DateTime.Now : DateTime.Parse(dateFromAsString);

				if (!filterFileData.ContainsKey(projectId))
					filterFileData[projectId] = [];

				filterFileData[projectId].Add(new ProjectData { EmpID = empId, ProjectID = projectId, DateFrom = dateFrom, DateTo = dateTo });

			}

			return filterFileData;

		}

		public EmployeeInfoOut PrintTeamWorkData(Dictionary<string, List<string>> teamWorkDictionary)
		{
			var dataToPrint = FilterFileData(teamWorkDictionary);
			int countEmployees = 1;
			EmployeeInfoOut employeeInfo = this.modelFactory.CreateEmployeeModel();

			foreach (var keyValuePair in dataToPrint.Where(p => p.Value.Count > 1))
			{	
				employeeInfo.ProjectID = keyValuePair.Key;				

				foreach (var record in keyValuePair.Value)
				{				

					if (countEmployees == 1)
						employeeInfo.Employee1ID = record.EmpID;
					else
						employeeInfo.Employee2ID = record.EmpID;

					var duration = record.DateTo - record.DateFrom;
					double totalDays = (int)Math.Round(duration.TotalDays);
					
					countEmployees++;
				}
				// no intersection in provided time interval
				employeeInfo.TeamWorkHours = 0;
			}
			return employeeInfo;
						
		}
	}
}
