namespace SoloCRM.EFModels
{
    public class Team
    {
        /// <summary>
        /// Primary key, auto-increment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// login user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Team name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Licensing status (nullable)
        /// </summary>
        public string? LicensingStatus { get; set; }

        /// <summary>
        /// Team code (max 10 characters)
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// State (nullable)
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Business email (nullable)
        /// </summary>
        public string? BusinessEmail { get; set; }

        /// <summary>
        /// General email (nullable)
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Date of birth in string format (nullable)
        /// </summary>
        public string? DOB { get; set; }

        /// <summary>
        /// Address (nullable)
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Record creation timestamp, defaults to current date/time
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Record update timestamp (nullable)
        /// </summary>
        public DateTime UpdateDate { get; set; }

        public required string CreatedBy { get; set; }

        public required string UpdatedBy { get; set; }
    }

}
