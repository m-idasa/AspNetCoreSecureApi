// IdentityUser is the Microsoft base identity class.
// creates empty scheme with just all the identity tables.
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AspNetCoreSecureApi.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Net;
using System.Data.Common;

namespace AspNetCoreSecureApi;

public class AppDbContext : IdentityUserContext<IdentityUser>
{

    public DbSet<Test> Tests { get; set; }

    public DbSet<Shopping> Shoppings { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Factor> Factors { get; set; }

    public DbSet<Bill> Bills { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Bill>()
            .ToView(nameof(Bills))
            .HasNoKey();
        //modelBuilder.Entity<Test>().InsertUsingStoredProcedure("dbo.Insert_test", spbuilder => spbuilder
        //    .HasParameter(test => test.Id)
        //    .HasParameter(test => test.Text));
            
        modelBuilder.Entity<Test>().HasQueryFilter(m => !m.Text.ToLower().Contains("test"));
        base.OnModelCreating(modelBuilder);
    }


}