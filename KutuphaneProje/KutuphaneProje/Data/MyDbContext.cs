using KutuphaneProje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KutuphaneProje.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Book> Kitaplar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;database=ismailproje;uid=local;password=123456;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString, option => {
                option.EnableRetryOnFailure();
            });
        }
    }
}
