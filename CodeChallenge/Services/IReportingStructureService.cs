using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    /// <summary>
    /// This service composes the employee service, and can be used to derive a reporting structure for a given employee.
    /// </summary>
    public interface IReportingStructureService
    {
        /// <summary>
        /// Get the reporting structure for an employee by their employee id.
        /// </summary>
        /// <param name="id">The ID of the employee get reports for</param>
        /// <returns>A <see cref="ReportingStructure"/>, fully hydrated for the given employee with all of their direct and transient reports</returns>
        ReportingStructure GetByEmployeeId(String id);
    }
}
