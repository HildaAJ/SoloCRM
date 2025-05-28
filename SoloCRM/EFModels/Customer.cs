namespace SoloCRM.EFModels
{
    public class Customer
    {
        public int Id { get; set; } // Primary key, auto-increment
        public string Name { get; set; } = string.Empty; // NVARCHAR(50), not null
        public string Tel { get; set; } = string.Empty; // VARCHAR(10), not null
        public string? State { get; set; } // NVARCHAR(10), nullable
        public string Status { get; set; } = string.Empty; // NVARCHAR(10), not null
        public int? CurrentTeamId { get; set; } // INT, nullable
        public string? Email { get; set; } // VARCHAR(50), nullable
        public string? Note { get; set; } // NVARCHAR(500), nullable
        public string? MetWhere { get; set; } // NVARCHAR(50), nullable
        public DateTime CreatedAt { get; set; } = DateTime.Now; // DATETIME, default GETDATE()
        public DateTime? UpdateDate { get; set; } // DATETIME, nullable

        public required string CreatedBy { get; set; }

        public required string UpdatedBy { get; set; }
    }

}
