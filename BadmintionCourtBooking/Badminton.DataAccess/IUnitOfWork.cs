


using Badminton.DataAccess.Repository.Interface;

namespace Badminton.DataAccess
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
