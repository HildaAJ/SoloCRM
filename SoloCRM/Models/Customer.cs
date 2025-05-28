using System;
using System.Collections.Generic;

namespace SoloCRM.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Tel { get; set; }

    public string? State { get; set; }

    /// <summary>
    /// Following = 1,Inactive = 2
    /// </summary>
    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Email { get; set; }

    public string? Note { get; set; }

    public string? MetWhere { get; set; }

    public int? CurrentTeamId { get; set; }

    public DateTime? MetWhen { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;
}
