using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IReportingStructureService _reportingStructureService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _reportingStructureService = reportingStructureService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
        
        // I added this endpoint here because it's inherently related to the employee, and pretty much just a derived view.
        // It also aligns with how REST should be structured, and since the ReportingStructure
        // isn't an entity itself it shouldn't have it's own controller for requests.
        
        /// <summary>
        /// Get the reporting structure for an employee by their employee id.
        /// </summary>
        /// <param name="id">The UUID of the employee to get reports for</param>
        /// <returns>A <see cref="ReportingStructure"/> with all transient reports fully hydrated</returns>
        [HttpGet("{id}/reports", Name = "getEmployeeReports")]
        public IActionResult GetEmployeeReports(String id)
        {
            _logger.LogDebug($"Received employee reports get request for '{id}'");
        
            var reports = _reportingStructureService.GetByEmployeeId(id);

            if (reports == null)
                return NotFound();

            return Ok(reports);
        }
    }
}
