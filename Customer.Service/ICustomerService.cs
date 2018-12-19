
using System;
using System.Collections.Generic;
using Customer.Contract;

namespace Customer.Service
{
    public interface ICustomerService
    {
        void CreateNew(CustomerDto customer);
        CustomerDto Update(CustomerDto customer);
        List<CustomerDto> GetAll();
        List<CustomerDto> GetByCityCode(string cityCode);
        CustomerDto GetById(Guid id);
    }
}
