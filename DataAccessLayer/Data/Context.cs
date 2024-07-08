using DataAccessLayer.Models;
using DataAccessLayer.Reposatries.OrderReposatry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class Context :DbContext
    {
        public Context() { }

        public Context(DbContextOptions <Context> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<order>()
                .Property(o => o.OrderId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");
        }
        public DbSet <product> products { get; set; }
        public DbSet <order> orders { get; set; }
    }
}
