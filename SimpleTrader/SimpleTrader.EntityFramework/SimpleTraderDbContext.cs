using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.EntityFramework
{
    //This class is going to manage out inner action
    //with the database with Entity Framework
    public class SimpleTraderDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        public SimpleTraderDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //tell database that owr stock will not be an entity and will be embedded in AssetTransAction
            modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Stock);

            base.OnModelCreating(modelBuilder);
        }
    }
}
