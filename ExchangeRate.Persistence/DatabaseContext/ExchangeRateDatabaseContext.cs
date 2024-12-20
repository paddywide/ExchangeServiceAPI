using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeRate.Domain.Common;
using ExchangeRate.Application.Contracts.Identity;
using ExchangeRate.Domain.Models;

namespace ExchangeRate.Persistence.DatabaseContext
{
    public class ExchangeRateDatabaseContext : DbContext
    {
        //private readonly IUserService _userService;

        //public ExchangeRateDatabaseContext(DbContextOptions<ExchangeRateDatabaseContext> options, IUserService userService) : base(options)
        //{
        //    this._userService = userService;

        //}
        //public DbSet<CurrencyCode> CurrencyCode { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExchangeRateDatabaseContext).Assembly);
        //    base.OnModelCreating(modelBuilder);
        //}
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        //        .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        //    {
        //        entry.Entity.DateModified = DateTime.Now;
        //        entry.Entity.ModifiedBy = _userService.UserId;
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.DateCreated = DateTime.Now;
        //            entry.Entity.CreatedBy = _userService.UserId;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

        public ExchangeRateDatabaseContext(DbContextOptions<ExchangeRateDatabaseContext> options) : base(options)
        {
        }
        public DbSet<CurrencyCode> CurrencyCode { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExchangeRateDatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
       

    }
}
