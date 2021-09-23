
using CompanyName.GatewayManagement.Data.Entities;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Data
{
    public interface IUnitOfWork
    {
        IRepository<Gateway> GatewayRepository { get; }
        IRepository<PeripheralDevice> PeripheralDeviceRepository { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> CommitAsync();
    }
}
