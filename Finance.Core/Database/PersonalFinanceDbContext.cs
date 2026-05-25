using Finance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finance.Core.Database;

public class PersonalFinanceDbContext : DbContext
{
    public PersonalFinanceDbContext(DbContextOptions<PersonalFinanceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTransaction>()
            .HasOne(t => t.SenderWallet)
            .WithMany()
            .HasForeignKey(t => t.SenderWalletId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<UserTransaction>()
            .HasOne(t => t.ReceiverWallet)
            .WithMany()
            .HasForeignKey(t => t.ReceiverWalletId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserTransaction> Transactions { get; set; }
    public DbSet<WalletAccount> WalletAccounts { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}