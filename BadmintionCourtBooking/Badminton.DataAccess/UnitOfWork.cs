using Badminton.DataAccess.Models;
using Badminton.DataAccess.Repository.Interface;


namespace Badminton.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BadmintonManagmentDBContext _dbContext;
        private readonly ICustomerRepository _customerRepository;

        public UnitOfWork(
            BadmintonManagmentDBContext dbContext,
            ICustomerRepository customerRepository
        )
        {
            _dbContext = dbContext;
            _customerRepository = customerRepository;
        }

        public ICustomerRepository CustomerRepository => _customerRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
