using System;
using Customer.Contract;
using FluentAssertions;
using Xunit;

namespace Customer.Service.Test
{
    public class CustomerAssemblerTests
    {
        [Theory, AutoMoqData]
        public void ToCustomer_Should_Success(CustomerDto customerDto,CustomerAssembler sut)
        {
            Action action = () =>
            {
                var result = sut.ToCustomer(customerDto);
                result.CityCode.Should().Be(customerDto.CityCode);
                result.FullName.Should().Be(customerDto.FullName);
                result.BirthDate.Should().Be(customerDto.BirthDate);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void ToCustomerDto_Should_Success(Domain.Customer customer, CustomerAssembler sut)
        {
            Action action = () =>
            {
                var result = sut.ToCustomerDto(customer);
                result.Id.Should().Be(customer.Id);
                result.CityCode.Should().Be(customer.CityCode);
                result.FullName.Should().Be(customer.FullName);
                result.BirthDate.Should().Be(customer.BirthDate);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}
