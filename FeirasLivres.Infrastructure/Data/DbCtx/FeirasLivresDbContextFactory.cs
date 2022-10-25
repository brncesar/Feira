using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FeirasLivres.Infrastructure.Data.DbCtx
{
    public class FeirasLivresDbContextFactory : IDesignTimeDbContextFactory<FeirasLivresDbContext>
    {
        private readonly IConfiguration Configuration;

        public FeirasLivresDbContextFactory(IConfiguration configuration) => Configuration = configuration;

        public FeirasLivresDbContextFactory(){}

        public FeirasLivresDbContext CreateDbContext(string[] args)
        {
            string filePath = TryGetSolutionDirectoryInfo() + @"\FeirasLivres.Infrastructure\appsettings.json";

            IConfiguration Configuration = new ConfigurationBuilder()
               .SetBasePath(Path.GetDirectoryName(filePath))
               .AddJsonFile("appSettings.json")
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FeirasLivresDbContext>();
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("FeirasLivresConnection"));

            return new FeirasLivresDbContext(optionsBuilder.Options);
        }

        private static DirectoryInfo? TryGetSolutionDirectoryInfo(string? currentPath = null)
        {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
                directory = directory.Parent;

            return directory;
        }
    }
}
