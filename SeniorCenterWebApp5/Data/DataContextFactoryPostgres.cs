using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SeniorCenterWebApp.Data
{
    public class DataContextFactoryPostgres : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseNpgsql(
                "Host=aws-1-eu-north-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres;Password=Ktgtcnjr111;SslMode=Require;Trust Server Certificate=true"
            );

            return new DataContext(optionsBuilder.Options);
        }
    }
}

