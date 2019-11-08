using IDataInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class LibraryContext : DbContext
    {
        private const string connectionString = "Server=LAPTOP-D5J0GDQQ;Database=LibraryDB;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<ScrapList> ScrapLists{ get; set; }
        public DbSet<LibraryBill> LibraryBills { get; set; }
    }
}
