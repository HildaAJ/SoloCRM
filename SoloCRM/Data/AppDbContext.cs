namespace SoloCRM.Data
{
    using Microsoft.EntityFrameworkCore;
    using SoloCRM.EFModels;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<FollowUpRecord> FollowUpRecord { get; set; }

        public DbSet<CancelProductRecord> CancelProductRecord { get; set; }

        public DbSet<PurchaseRecord> PurchaseRecord { get; set; }
        public DbSet<Team> Team { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer Entity Configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.State)
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Email)
                    .HasMaxLength(50);

                entity.Property(e => e.Note)
                    .HasMaxLength(500);

                entity.Property(e => e.MetWhere)
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdateDate);

                entity.Property(e => e.CreatedBy)
                     .IsRequired();

                entity.Property(e => e.UpdatedBy)
                     .IsRequired();

            });

            // Product Entity Configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ProductInfomation)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdateDate);

                entity.Property(e => e.CreatedBy)
                     .IsRequired();

                entity.Property(e => e.UpdatedBy)
                     .IsRequired();
            });


            modelBuilder.Entity<FollowUpRecord>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerId)
                      .IsRequired();

                entity.Property(e => e.FollowUpType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.NextFollowUpDate);

                entity.Property(e => e.Note)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdateDate);

                entity.Property(e => e.CreatedBy)
                     .IsRequired();

                entity.Property(e => e.UpdatedBy)
                     .IsRequired();
            });

            modelBuilder.Entity<PurchaseRecord>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerId)
                      .IsRequired();

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.ApplyDate)
                      .IsRequired()
                      .HasColumnType("date"); // map to SQL DATE type

                entity.Property(e => e.ProductId)
                      .IsRequired();

                entity.Property(e => e.ProductName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.SumAssured)
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Fees)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Years);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdateDate);

                entity.Property(e => e.CreatedBy)
                     .IsRequired();

                entity.Property(e => e.UpdatedBy)
                     .IsRequired();

            });


            modelBuilder.Entity<CancelProductRecord>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CustomerId)
                      .IsRequired();

                entity.Property(e => e.CustomerName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.PurchaseId)
                      .IsRequired();

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.Reason)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.ApplyDate)
                      .IsRequired()
                      .HasColumnType("date");

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdateDate);

                entity.Property(e => e.CreatedBy)
                      .IsRequired();

                entity.Property(e => e.UpdatedBy)
                     .IsRequired();
            });


            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.LicensingStatus)
                      .HasMaxLength(50);

                entity.Property(e => e.Code)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(e => e.State)
                      .HasMaxLength(10);

                entity.Property(e => e.BusinessEmail)
                      .HasMaxLength(50);

                entity.Property(e => e.Email)
                      .HasMaxLength(50);

                entity.Property(e => e.DOB)
                      .HasMaxLength(10);

                entity.Property(e => e.Address)
                      .HasMaxLength(100);

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdateDate);

                entity.Property(e => e.CreatedBy)
                     .IsRequired();

                entity.Property(e => e.UpdatedBy)
                     .IsRequired();
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AppUser");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(256)
                    .IsRequired();

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Account).IsUnique();
                entity.Property(e => e.Account).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });
        }


    }

}
