using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activities.Infra;
using Microsoft.EntityFrameworkCore;

namespace Activities.Api.Configuration
{
     public static class DataBaseManagement
    {
        public static void MigrationInitialization (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ActivityContext>();

                _db.Database.Migrate();
            }
        }
    }
}
