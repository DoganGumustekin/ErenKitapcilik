using Domain.Entities;
using Core.Entities.UserClaimModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Security.Hashing;

namespace Infrastructure.DBContext
{
    public class DBConnection : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ErenKitapcilik;Trusted_Connection=true;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");

                entity.Property(i => i.BookName).HasColumnName("BookName").HasColumnType("nvarchar").HasMaxLength(100);
                entity.Property(i => i.AuthorName).HasColumnName("AuthorName").HasColumnType("nvarchar").HasMaxLength(100);
                entity.Property(i => i.Price).HasColumnName("Price").HasColumnType("decimal(18,2)");
            });

            byte[] adminPasswordHash, adminPasswordSalt;
            HashingHelper.CreatePasswordHash("admin", out adminPasswordHash, out adminPasswordSalt);

            byte[] userPasswordHash, userPasswordSalt;
            HashingHelper.CreatePasswordHash("user", out userPasswordHash, out userPasswordSalt);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id=1,
                    BookName = "Cosmos",
                    AuthorName = "Carl Sagan",
                    Image = "http://localhost:5000/images/b4c2e771-0c0c-4287-9301-5dae7728ebec.jpg",
                    Price = 18,
                    PrintingHouse = "Premius Yayın Evi"
                },
                new Book
                {
                    Id = 2,
                    BookName = "The Theory Of Everything",
                    AuthorName = "Stephen W. Hawking",
                    Image = "http://localhost:5000/images/516a5d15-6bb5-4428-a574-421a111e5017.jpg",
                    Price = 8,
                    PrintingHouse = "Premius Yayın Evi"
                },
                new Book
                {
                    Id = 3,
                    BookName = "Notre Dame'in Kamburu",
                    AuthorName = "Victor Hugo",
                    Image = "http://localhost:5000/images/a208ba3f-79d5-49e2-b3e1-7d6b9aafb2db.jpg",
                    Price = 10,
                    PrintingHouse = "Kültür Yayın Evi"
                },
                new Book
                {
                    Id = 4,
                    BookName = "Şizofren",
                    AuthorName = "Wulf Dorn",
                    Image = "http://localhost:5000/images/c2df2671-66de-44d3-af88-b9b7981b95e8.jpg",
                    Price = 14,
                    PrintingHouse = "Pegasus Yayıncılık"
                },
                new Book
                {
                    Id = 5,
                    BookName = "Simyacı",
                    AuthorName = "Paulo Coelho",
                    Image = "http://localhost:5000/images/1b52d6bd-0b73-4937-a84a-217d859cad74.jpg",
                    Price = 3,
                    PrintingHouse = "Pegasus Yayıncılık"
                },
                new Book
                {
                    Id = 6,
                    BookName = "Ferrari'sini Satan Bilge",
                    AuthorName = "Robin Sharma",
                    Image = "http://localhost:5000/images/94fc2ab3-5398-4288-a01a-1847625dd739.jpg",
                    Price = 14,
                    PrintingHouse = "Pegasus Yayıncılık"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    PasswordSalt = adminPasswordSalt,
                    PasswordHash = adminPasswordHash,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    FirstName = "user",
                    LastName = "user",
                    Email = "user@gmail.com",
                    PasswordSalt = userPasswordSalt,
                    PasswordHash = userPasswordHash,
                    IsActive = true
                }
            );

            modelBuilder.Entity<OperationClaim>().HasData(
                new OperationClaim { Id = 1, Name = "admin" }
            );

            modelBuilder.Entity<UserOperationClaim>().HasData(
                new UserOperationClaim { Id = 1, UserId = 1, OperationClaimId = 1 }
            );

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
