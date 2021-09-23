using CompanyName.GatewayManagement.Data.Entities;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private GatewayDbContext Context { get; }

        #region Private properties

        private readonly IRepository<Gateway> _gatewayRepository;
        private readonly IRepository<PeripheralDevice> _peripheralDeviceRepository;

        #endregion

        public UnitOfWork(GatewayDbContext context)
        {
            Context = context;
            _gatewayRepository = new GenericRepository<Gateway>(Context);
            _peripheralDeviceRepository = new GenericRepository<PeripheralDevice>(Context);

        }

        #region Repositories

        public IRepository<Gateway> GatewayRepository { get { return _gatewayRepository; } }
        public IRepository<PeripheralDevice> PeripheralDeviceRepository { get { return _peripheralDeviceRepository; } }

        #endregion

        /// <summary>
        /// Get repository for fake db unit test
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(Context);
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }



    }
}
