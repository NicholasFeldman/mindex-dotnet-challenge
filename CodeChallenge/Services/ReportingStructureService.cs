using System.Linq;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    
    public class ReportingStructureService: IReportingStructureService
    {
        
        private readonly IEmployeeService _employeeService;
        
        public ReportingStructureService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
        public ReportingStructure GetByEmployeeId(string id)
        {
            var employee = _employeeService.GetById(id);

            // Recursively sum reports for the given employee.
            // This could have a few improvements in the future,
            // such as protections for circular references and depth limits.
            var reports = employee.DirectReports.Count + employee.DirectReports.Sum(
                directReport => GetByEmployeeId(directReport.EmployeeId).NumberOfReports
            );

            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = reports
            };
        }
    }
}

