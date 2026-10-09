using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
  public class ApplicationDbContext : IdentityDbContext<AppUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Stock> Stocks { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Portfolio> Portfolios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
      builder.Entity<Portfolio>().HasOne(x => x.AppUser).WithMany(x => x.Portfolios).HasForeignKey(x => x.AppUserId);
      builder.Entity<Portfolio>().HasOne(x => x.Stock).WithMany(x => x.Portfolios).HasForeignKey(x => x.StockId);

      List<IdentityRole> roles = new List<IdentityRole>()
      {
        new IdentityRole {
          Id = "admin-role-id",
Name = "Admin", NormalizedName = "ADMIN" ,
            ConcurrencyStamp = "be17a4a6-c7f6-419d-a5f0-01727e11ca8c"
},
        new IdentityRole { Id = "user-role-id", Name = "User", NormalizedName = "USER",
         ConcurrencyStamp = "9045fc53-fcc9-4fef-8eef-0652035affdb" },
      };

      builder.Entity<IdentityRole>().HasData(roles);
    }

  }
}