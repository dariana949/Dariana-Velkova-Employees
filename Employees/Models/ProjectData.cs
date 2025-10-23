namespace Employees.Models
{
	public class ProjectData
	{
		public required string EmpID { get; set; }
		public required string ProjectID { get; set; }
		public DateTime	DateFrom {  get; set; }
		public DateTime DateTo { get; set; }
	}
}
