using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Customer.Contract;
using Customer.Domain;
using Customer.Repository;
using FluentAssertions;
using Moq;
using Xunit;

namespace Customer.Service.Test
{
    public class CustomerServiceTests
    {
        [Theory, AutoMoqData]
        public void CreateNewCustomer_Should_Success([Frozen]Mock<ICustomerAssembler> assembler, [Frozen]Mock<ICustomerRepository> repository, CustomerDto customerDto, Domain.Customer customer, CustomerService sut)
        {
            assembler.Setup(c => c.ToCustomer(customerDto)).Returns(customer);
            repository.Setup(c => c.Save(customer)).Returns(It.IsAny<Guid>());

            Action action = () =>
            {
                sut.CreateNew(customerDto);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void UpdateCustomer_Should_Success([Frozen]Mock<ICustomerAssembler> assembler, [Frozen]Mock<ICustomerRepository> repository, CustomerDto customerDto, Domain.Customer customer, CustomerService sut)
        {
            assembler.Setup(c => c.ToCustomer(customerDto)).Returns(customer);
            repository.Setup(c => c.Update(customer));

            Action action = () =>
            {
                sut.Update(customerDto);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetAll_Should_Success([Frozen]Mock<ICustomerAssembler> assembler, [Frozen]Mock<ICustomerRepository> repository, List<Domain.Customer> customers, List<CustomerDto> customersDtos, CustomerService sut)
        {
            repository.Setup(c => c.All()).Returns(customers.AsQueryable);
            assembler.Setup(c => c.ToCustomerDtoList(customers)).Returns(customersDtos);

            Action action = () =>
            {
                var result = sut.GetAll();
                result.Count.Should().Be(customersDtos.Count);
            };
            action.Should().NotThrow<Exception>();
        }


        [Theory, AutoMoqData]
        public void GetByCityCode_Should_Success([Frozen]Mock<ICustomerAssembler> assembler, [Frozen]Mock<ICustomerRepository> repository, string cityCode, List<Domain.Customer> customers, List<CustomerDto> customersDtos, CustomerService sut)
        {
            assembler.Setup(c => c.ToCustomerDtoList(customers)).Returns(customersDtos);
            repository.Setup(x => x.Find(It.IsAny<Expression<Func<Domain.Customer, bool>>>())).Returns(customers.AsQueryable);

            Action action = () =>
            {
                var result = sut.GetByCityCode(cityCode);
                result.Should().BeEquivalentTo(customersDtos);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetById_Should_Return_As_Expected([Frozen]Mock<ICustomerAssembler> assembler, [Frozen]Mock<ICustomerRepository> repository, Guid id, CustomerDto customerDto, Domain.Customer customer, CustomerService sut)
        {
            assembler.Setup(c => c.ToCustomerDto(customer)).Returns(customerDto);
            repository.Setup(c => c.Get(id)).Returns(customer);

            Action action = () =>
            {
                var result = sut.GetById(id);
                result.Should().BeEquivalentTo(customerDto);
            };
            action.Should().NotThrow<Exception>();
        }
    }
    //Method parameter olarak Automoq yapabilmek için kullanacağımız attribute
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
