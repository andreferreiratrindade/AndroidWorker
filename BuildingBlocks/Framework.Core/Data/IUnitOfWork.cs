using System.Threading.Tasks;

namespace Framework.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}