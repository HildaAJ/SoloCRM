namespace SoloCRM.EFModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Account { get; set; }

        [Required]
        [MaxLength(256)]
        public required string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public string AgentCode { get; set; }
    }

}
