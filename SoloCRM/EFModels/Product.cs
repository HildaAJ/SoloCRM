namespace SoloCRM.EFModels
{
    public class Product
    {
        public int Id { get; set; } // Primary key, auto-increment
        public string Company { get; set; } = string.Empty; // NVARCHAR(50), not null
        public string Type { get; set; } = string.Empty; // NVARCHAR(20), not null
        public string Name { get; set; } = string.Empty; // NVARCHAR(50), not null
        public string TaxType { get; set; } = string.Empty; // NVARCHAR(20), not null
        public string ProductInfomation { get; set; } = string.Empty; // NVARCHAR(300), not null
        public DateTime CreatedAt { get; set; } = DateTime.Now; // DATETIME, default GETDATE()
        public DateTime? UpdateDate { get; set; } // DATETIME, nullable
        public required string CreatedBy { get; set; }

        public required string UpdatedBy { get; set; }
    }

}
