using System.Collections.Generic;
using Customer.Contract;

namespace Customer.Service
{
    public class CustomerAssembler : ICustomerAssembler
    {
        public Domain.Customer ToCustomer(CustomerDto customerDto)
        {
            return new Domain.Customer(customerDto.FullName, customerDto.CityCode, customerDto.BirthDate);
        }

        public CustomerDto ToCustomerDto(Domain.Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                BirthDate = customer.BirthDate,
                CityCode = customer.CityCode,
                FullName = customer.FullName
            };
        }

        public List<CustomerDto> ToCustomerDtoList(List<Domain.Customer> customerList)
        {
            var response = new List<CustomerDto>();
            foreach (var customer in customerList)
            {
                var customerDto = ToCustomerDto(customer);
                response.Add(customerDto);
            }

            return response;
        }
    }
}
