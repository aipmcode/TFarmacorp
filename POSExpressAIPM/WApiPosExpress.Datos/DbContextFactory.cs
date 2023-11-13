using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WApiPosExpress.Datos
{
    public static class DbContextFactory
    {
        public static DbExpressContext CreateContext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbExpressContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBXpressConnection"));

            return new DbExpressContext(optionsBuilder.Options);
        }
    }
}
