namespace SoloCRM.EFModels
{
    public class CancelProductRecord
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
        /// Name of the customer
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key to PurchaseRecord
        /// </summary>
        public int PurchaseId { get; set; }

        /// <summary>
        /// Status of cancellation (e.g., Pending, Approved)
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Reason for cancellation, up to 500 characters
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Date of cancellation application (no time part)
        /// </summary>
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// Record creation timestamp, defaults to current date/time
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Record update timestamp (nullable)
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        public required string CreatedBy { get; set; }

        public required string UpdatedBy { get; set; }

    }
}
