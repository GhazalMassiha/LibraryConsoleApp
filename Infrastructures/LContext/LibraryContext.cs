using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Enums;
using Microsoft.EntityFrameworkCore;

namespace HW12_Issue1.Infrastructures.LContext
{
    public class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-DSN9OLT;Database=HW12-LibraryDb;Integrated Security=true;TrustServerCertificate=true;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // --- User Configuration ---

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(u => u.Username)
                      .IsUnique();

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Role)
                      .IsRequired();

                entity.HasMany(u => u.BorrowedBooks)
                      .WithOne(bb => bb.User)        
                      .HasForeignKey(bb => bb.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Reviews)
                      .WithOne(r => r.User)         
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --- Admin Configuration ---

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admins");
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(a => a.Username)
                      .IsUnique();

                entity.Property(a => a.Password)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(a => a.Role)
                      .IsRequired();
            });

            // --- Category Configuration ---

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany<Book>()
                      .WithOne(b => b.Category)      
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --- Book Configuration ---

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(b => b.IsBorrowed)
                      .IsRequired();

                entity.HasMany<BorrowedBook>()
                      .WithOne(bb => bb.Book)        
                      .HasForeignKey(bb => bb.BookId)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(b => b.Reviews)
                      .WithOne(r => r.Book)
                      .HasForeignKey(r => r.BookId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --- BorrowedBook Configuration ---

            modelBuilder.Entity<BorrowedBook>(entity =>
            {
                entity.ToTable("BorrowedBooks");
                entity.HasKey(bb => bb.Id);

                entity.Property(bb => bb.BorrowedTime)
                      .IsRequired();


                entity.HasOne(bb => bb.User)
                      .WithMany(u => u.BorrowedBooks)
                      .HasForeignKey(bb => bb.UserId)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(bb => bb.Book)
                      .WithMany(b => b.BorrowedBooks)
                      .HasForeignKey(bb => bb.BookId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --- Review Configuration ---

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.Comment)
                      .HasMaxLength(1000);

                entity.Property(r => r.Rating)
                      .IsRequired();

                entity.Property(r => r.CreatedAt)
                      .IsRequired();

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Book)
                      .WithMany(b => b.Reviews)
                      .HasForeignKey(r => r.BookId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // === Seed Data ===

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction" },
                new Category { Id = 2, Name = "History" },
                new Category { Id = 3, Name = "Romance" }
            );

            // Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Book 1", CategoryId = 2, IsBorrowed = false },
                new Book { Id = 2, Title = "Book 2", CategoryId = 1, IsBorrowed = false },
                new Book { Id = 3, Title = "Book 3", CategoryId = 2, IsBorrowed = false }
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "User1", Password = "password1", Role = RoleEnum.User },
                new User { Id = 2, Username = "User2", Password = "password2", Role = RoleEnum.User }
            );

            // Admins
            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, Username = "Admin1", Password = "password1", Role = RoleEnum.Admin }
            );

            // Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    UserId = 1,
                    BookId = 1,
                    Rating = 5,
                    Comment = "Loved the creativity!",
                    CreatedAt = new DateTime(2025, 1, 5)
                },
                new Review
                {
                    Id = 2,
                    UserId = 2,
                    BookId = 2,
                    Rating = 3,
                    Comment = "If you're really into history, you'll know most of it is fiction instead of the actual truth. In general, not bad.",
                    CreatedAt = new DateTime(2024, 2, 2)
                }
            );
        }
    }
}
