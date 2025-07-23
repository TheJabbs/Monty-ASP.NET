using api.core.Models;

namespace api.Data; 
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
    : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.SenderAccount)
            .WithMany(a => a.SentTransactions)
            .HasForeignKey(t => t.SenderAccountId)
            .OnDelete(DeleteBehavior.Restrict); // Important to avoid cascade cycle

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.ReceiverAccount)
            .WithMany(a => a.ReceivedTransactions)
            .HasForeignKey(t => t.ReceiverAccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer("Server=PapaJabb;Database=monty;Integrated Security=True;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }

    
    public DbSet<Models.Customer> Customers { get; set; } = null!;
    public DbSet<Models.Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
}