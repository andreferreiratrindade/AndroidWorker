using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rests.Infra;
using Microsoft.EntityFrameworkCore;

namespace Rests.Api.Configuration
{
     public static class DataBaseManagement
    {
        public static void MigrationInitialization (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<RestContext>();

                _db.Database.Migrate();
            }
        }
    }
}
