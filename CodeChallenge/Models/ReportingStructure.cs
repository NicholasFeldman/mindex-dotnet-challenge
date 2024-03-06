namespace CodeChallenge.Models
{

    /// <summary>
    /// This model does not exist in the database, but is used as a DTO for the reporting structure of an employee.
    /// It is created dynamically by <see cref="CodeChallenge.Services.ReportingStructureService"/> and is not persisted. 
    /// </summary>
    public class ReportingStructure
    {
        /// <summary>
        /// The Employee for which the reporting structure is being derived.
        /// </summary>
        public Employee Employee { get; set; }
        
        /// <summary>
        /// The total number of all direct and transient reports for an employee. (Their reports, reports' reports, etc.)
        /// </summary>
        public int NumberOfReports { get; set; }
    }
}
