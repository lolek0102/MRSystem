using Microsoft.EntityFrameworkCore;
using MRSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MRSystem
{
    public class MRlibContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Borrow> Borrows { get; set; } = null!;

        public string ConnectionString { get; }

        public MRlibContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }
    }
}
