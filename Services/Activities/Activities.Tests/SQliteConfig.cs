using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Mediator;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Activities.Infra;

namespace Activities.Tests
{
    public class SQliteConfig
    {
        private SqliteConnection _sqlConnection;
        public ActivityContext ActivityContext {get;set;}
        public void OpenConnection()
        {
            _sqlConnection = new SqliteConnection(@"Data Source=:memory:");
            _sqlConnection.Open();
            ActivityContext = GetContext();
        }

        public void CloseConnection()
        {
            _sqlConnection.Close();
        }
        public ActivityContext  GetContext()
        {
            var options = new DbContextOptionsBuilder<ActivityContext>()
                .UseSqlite(_sqlConnection)
                .Options;

            var mediatorHandler = Substitute.For<IMediatorHandler>();

            var context = new ActivityContext(options, mediatorHandler);
            context.Database.EnsureCreated();

            return context;

        }
    }
}