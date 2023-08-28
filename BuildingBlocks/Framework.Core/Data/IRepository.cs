using System;
using System.Data.Common;
using Framework.Core.DomainObjects;

namespace Framework.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        DbConnection GetConnection();

    }
}