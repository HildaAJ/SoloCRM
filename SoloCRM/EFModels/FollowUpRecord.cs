namespace SoloCRM.EFModels
{
    public class FollowUpRecord
    {
        /// <summary>
        /// Primary key, auto-increment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to Customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Type of follow-up (e.g., call, email, meeting)
        /// </summary>
        public string FollowUpType { get; set; } = string.Empty;

        /// <summary>
        /// Scheduled date for next follow-up (nullable)
        /// </summary>
        public DateTime? NextFollowUpDate { get; set; }

        /// <summary>
        /// Notes about the follow-up, up to 1000 characters
        /// </summary>
        public string Note { get; set; } = string.Empty;

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
