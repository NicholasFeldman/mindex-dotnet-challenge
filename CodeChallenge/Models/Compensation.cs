using System;

namespace CodeChallenge.Models
{
    
    // This could be an owned entity, but requires a bit more context before making that decision.
    // That likely depends on the data design itself and what database is being used.
    /// <summary>
    /// This entity represents a compensation for an employee, effective at a given point in time.
    /// One employee may have multiple compensations,
    /// and compensations may be effective in the future or superseded by a more recent compensation.
    /// </summary>
    public class Compensation
    {
        // I'm creating a new ID for this entity, as opposed to using the EmployeeId as the primary key.
        // This is because with EffectiveDate, it's implied that the same employee could have a new compensation come into
        // effect in the future, or that we may want to store historical compensations.
        /// <summary>
        ///  The UUID of this compensation
        /// </summary>
        public string CompensationId { get; set; }

        /// <summary>
        /// The UUID of the employee this compensation is for
        /// </summary>
        public string EmployeeId { get; set; }
        
        // Depending on use case, salary could be represented a few different ways.
        // I'm going to default to the safe bet of "Don't use floating point types for money"
        /// <summary>
        /// How much money the employee is compensated, in USD
        /// </summary>
        public string Salary { get; set; }
        
        /// <summary>
        /// When this compensation becomes effective
        /// </summary>
        public DateTime EffectiveDate { get; set; }
    }
}
