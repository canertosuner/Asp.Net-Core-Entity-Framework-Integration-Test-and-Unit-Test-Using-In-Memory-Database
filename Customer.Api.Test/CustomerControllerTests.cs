using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Customer.Api.Controllers;
using Customer.Contract;
using Customer.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Customer.Api.Test
{
    public class CustomerControllerTests
    {
        [Theory, AutoMoqData]
        public void GetAll_Should_Return_As_Expected(Mock<ICustomerService> customerServiceMock, List<CustomerDto> expected)
        {
            var sut = new CustomerController(customerServiceMock.Object);
            customerServiceMock.Setup(c => c.GetAll()).Returns(expected);

            var result = sut.Get();

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<List<CustomerDto>>().Subject;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void GetById_Should_Return_As_Expected(Mock<ICustomerService> customerServiceMock, Guid id, CustomerDto expected)
        {
            var sut = new CustomerController(customerServiceMock.Object);
            customerServiceMock.Setup(c => c.GetById(id)).Returns(expected);

            var result = sut.Get(id);

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<CustomerDto>().Subject;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void Post_Should_Return_As_Expected(Mock<ICustomerService> customerServiceMock, CustomerDto customer)
        {
            var sut = new CustomerController(customerServiceMock.Object);
            customerServiceMock.Setup(c => c.CreateNew(customer));

            var actual = sut.Post(customer);

            actual.GetType().Should().Be(typeof(OkResult));
        }

        [Theory, AutoMoqData]
        public void Put_Should_Return_As_Expected(Mock<ICustomerService> customerServiceMock, CustomerDto expected)
        {
            var sut = new CustomerController(customerServiceMock.Object);
            customerServiceMock.Setup(c => c.Update(expected)).Returns(expected);

            var result = sut.Put(expected);

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<CustomerDto>().Subject;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void GetByCityCode_Should_Return_As_Expected(Mock<ICustomerService> customerServiceMock, string cityCode, List<CustomerDto> expected)
        {
            var sut = new CustomerController(customerServiceMock.Object);
            customerServiceMock.Setup(c => c.GetByCityCode(cityCode)).Returns(expected);

            var result = sut.GetByCityCode(cityCode);

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<List<CustomerDto>>().Subject;

            Assert.Equal(expected, actual);
        }
    }
    //Method parameter olarak Automoq yapabilmek için kullanacaðýmýz attribute
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
