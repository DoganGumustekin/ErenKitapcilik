using Domain.Entities;
using Core.Entities.UserClaimModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBContext
{
    public class DBConnection : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ErenKitapcilik;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<UserOperationClaim>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserOperationClaim>()
                .HasOne<OperationClaim>()
                .WithMany()
                .HasForeignKey(u => u.OperationClaimId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
