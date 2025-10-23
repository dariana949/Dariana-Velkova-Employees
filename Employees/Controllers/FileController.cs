using Employees.Exceptions;
using Employees.Models;
using Employees.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Services
{

	[ApiController]
	[Route("[controller]")]
	public class FileController : ControllerBase
	{		
		private readonly IFileService fileService;

		public FileController(IFileService fileService)
		{			
			this.fileService = fileService;
		}

		[HttpPost]
		public async Task<ActionResult<EmployeeInfoOut>> ReadCSVFile(IFormFile file)
		{
			EmployeeInfoOut result;
			try
			{
				fileService.Validate(file);
			    result = await this.fileService.ReadCsvFile(file);
			}
			catch (CsvCustomExeption ex)
			{
				return BadRequest(ex.Message);
			}
			
			return Ok(result);
		}
	}
}
