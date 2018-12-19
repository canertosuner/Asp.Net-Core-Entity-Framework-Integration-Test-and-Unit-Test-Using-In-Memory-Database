using Customer.Domain;

namespace Customer.Repository
{
public class CustomerRepository : GenericRepository<Domain.Customer>, ICustomerRepository
{
    public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
    {
    }
}
}
