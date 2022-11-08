using Microsoft.EntityFrameworkCore;
using TestWork.Models;

namespace TestWork.Data
{
    public partial class TestWorkContext:DbContext
    {
        public TestWorkContext()
        {
        }
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Incident> Incidents { get; set; } = null!;
        public TestWorkContext(DbContextOptions<TestWorkContext> options)
        : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.ContactId, "IX_Accounts_ContactId");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ContactId);
            });

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.HasIndex(e => e.AccountId, "IX_Incidents_AccountId");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.AccountId);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestWork-Part1;Integrated Security=true;");
            }
        }
    }
}
