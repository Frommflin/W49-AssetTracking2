using Microsoft.EntityFrameworkCore;

namespace W49_AssetTracking2
{
    internal class DatabaseContext : DbContext
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=AssetTracking;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring.
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
        }
    }
}
