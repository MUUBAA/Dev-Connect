using dev_connect.Server.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dev_connect.Server.Utils
{
    public class DataMigration
    {
        public static void Configure(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Repository>();
                db.Database.Migrate();
                db.Database.EnsureCreated();
            }
        }
    }
}
