using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    
    /// <summary>
    /// Service for interacting with Compensation data
    /// </summary>
    public interface ICompensationService
    {
        /// <summary>
        /// Gets the latest effective compensation for an employee by their ID
        /// </summary>
        /// <param name="id">The ID of the employee</param>
        /// <returns>Latest effective compensation for the employee</returns>
        Compensation GetByEmployeeId(String id);
        
        /// <summary>
        /// Create a new compensation
        /// </summary>
        /// <param name="compensation">The compensation object to create</param>
        /// <returns>The created compensation object</returns>
        Compensation Create(Compensation compensation);
    }
}
