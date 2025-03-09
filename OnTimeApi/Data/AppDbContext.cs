using Microsoft.EntityFrameworkCore;
using OnTimeApi.Models;

namespace OnTimeApi.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }
    
    public DbSet<CommuteLog> CommuteLogs { get; set; }
    public DbSet<SleepLog> SleepLogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserPreference> UserPreferences { get; set; }
}