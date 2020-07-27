using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.DataAccess
{
    public class EveryPayContext:DbContext
    {

        public EveryPayContext() : base("name=EveryPayContext")
        {
                 
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Bill> Bills { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<SupplierField> SupplierFields { get; set; }
        public DbSet<SpecificFieldValue> SpecificFieldValues { get; set; }
        public DbSet<BillSupplier> BillSupplier { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<SystemSettings> GlobalSettings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();

            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Role).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired();

          

            modelBuilder.Entity<Supplier>().Property(m => m.Name).IsRequired();
            modelBuilder.Entity<Supplier>().Property(m => m.Commission).IsRequired();

            modelBuilder.Entity<SupplierField>().Property(s => s.FieldName).IsRequired();
            modelBuilder.Entity<SupplierField>().Property(s => s.TypeOfField).IsRequired();

            modelBuilder.Entity<Bill>().Property(b => b.Amount).IsRequired();
            modelBuilder.Entity<BillSupplier>()
                        .HasKey(c => new { c.BillId, c.SupplierId });

            modelBuilder.Entity<Transaction>().Property(t => t.ClientId).IsOptional();


            base.OnModelCreating(modelBuilder);
        }
    }
}
