using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SeniorCenterWebApp.Data
{
    public class DataContextFactorySqlServer : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-87GLJFV;Initial Catalog=SeniorCenterDB;Integrated Security=True"
            );

            return new DataContext(optionsBuilder.Options);
        }
    }
}

