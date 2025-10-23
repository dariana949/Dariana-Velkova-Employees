using Employees.Factory.Interfaces;
using Employees.Models;

namespace Employees.Factory
{
	public class ModelFactory : IModelFactory
	{
		public EmployeeInfoOut CreateEmployeeModel()
		{
			return new EmployeeInfoOut();
		}
	}
}
