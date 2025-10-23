using Employees.Models;

namespace Employees.Factory.Interfaces
{
	public interface IModelFactory
	{
		public EmployeeInfoOut CreateEmployeeModel();
	}
}
