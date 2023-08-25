using KutuphaneAPI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KutuphaneAPI.Data
{
    public class veritabani : DbContext
    {
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
