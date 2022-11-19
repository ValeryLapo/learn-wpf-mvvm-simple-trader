using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContext : DbContext
    {
        //This class is going to manage out inner action
        //with the database with Entity Framework

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AssetTransaction> AssetTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //tell database that owr stock will not be an entity and will be embedded in AssetTransAction
            modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Stock);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //this method only available with sqlserver nuget package
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SimpleTraderDB;Trusted_Connection=True");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
