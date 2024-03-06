using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    
    /// <summary>
    /// This controller is responsible for handling requests related to compensations.
    /// It can create a new compensation, and retrieve an employee's latest effective compensation by their ID.
    /// </summary>
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        /// <summary>
        /// Create a new compensation
        /// </summary>
        /// <param name="compensation">A Compensation object to store</param>
        /// <returns>HTTP RouteResult for where the new compensation can be fetched</returns>
        [HttpPost(Name = "createCompensation")]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensation.EmployeeId }, compensation);
        }

        // Instead of fetching the compensation by it's id, we fetch it by the employee's id, as per the requirements
        // of this assignment. However, as an alternative, I would propose that this fetch a compensation by it's ID,
        // and that the employee's latest effective compensation be fetched by a different route such as `api/employee/{id}/compensation`
        /// <summary>
        /// Fetch the latest effective compensation for an employee by their ID
        /// </summary>
        /// <param name="id">The ID of the employee to fetch the compensation of</param>
        /// <returns>HTTP Result with the employee's latest effective compensation</returns>
        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
