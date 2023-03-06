using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Models;

namespace LibraryMVC.Data
{
    public class LibraryMVCContext : DbContext
    {
        public LibraryMVCContext (DbContextOptions<LibraryMVCContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryMVC.Models.City> City { get; set; } = default!;

        public DbSet<LibraryMVC.Models.District> District { get; set; }

        public DbSet<LibraryMVC.Models.Library> Library { get; set; }
        public DbSet<LibraryMVC.Models.Author> Author { get; set; }
        public DbSet<LibraryMVC.Models.Book> Book { get; set; }
        public DbSet<LibraryMVC.Models.LibraryBook> LibraryBooks { get; set; }
        public DbSet<LibraryMVC.Models.Employee> Employee { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryBook>().HasKey(b => new { b.ISBN, b.LibraryId });
        }
    }
}
