using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;

namespace Payments.Domain;

public class ApplicationDbContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}