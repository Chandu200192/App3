using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                        .HasIndex(u => new { u.EmployeeID, u.Email })
                        .IsUnique();

            modelBuilder.Entity<Client>()
                        .HasIndex(c => c.ClientID)
                        .IsUnique();

            modelBuilder.Entity<Projects>()
                        .HasIndex(p => p.ProjectID)
                        .IsUnique();
            modelBuilder.Entity<Expenses>()
                        .HasIndex(e => e.ExpensesId)
                        .IsUnique();
            modelBuilder.Entity<ExpensesDetails>()
                        .HasIndex(d => d.ExpenseID)
                        .IsUnique();

            modelBuilder.Entity<Receipt>()
                      .HasIndex(d => d.ReceiptID)
                      .IsUnique();

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expenses> ExpensesDb { get; set; }
        public DbSet<ExpensesDetails> ExpensesDetailsDb { get; set; }
        public DbSet<BioMetricInfo> BioMetricInfoDb { get; set; }
        public DbSet<Receipt> ReceiptDb { get; set; }
        public DbSet<DailyBioMetricRecords> DailyBioRecordDb { get; set; }

    }
}
