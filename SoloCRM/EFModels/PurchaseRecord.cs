namespace SoloCRM.EFModels
{
    public class PurchaseRecord
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
        /// Status of the purchase (e.g., Pending, Approved)
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Date of application (no time part)
        /// </summary>
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// Foreign key to Product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product purchased
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Sum assured, nullable
        /// </summary>
        public decimal? SumAssured { get; set; }

        /// <summary>
        /// Fees associated with the purchase
        /// </summary>
        public decimal Fees { get; set; }

        /// <summary>
        /// Number of years, nullable
        /// </summary>
        public int? Years { get; set; }

        /// <summary>
        /// Record creation timestamp, default to current date/time
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Record update timestamp, nullable
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        public required string CreatedBy { get; set; }

        public required string UpdatedBy { get; set; }
    }

}
