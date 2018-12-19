using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Customer.Domain.Test
{
    public class CustomerTests
    {
        [Theory, AutoMoqData]
        public void Create_Customer_Should_Throw_Exception_When_FullName_Is_Empty(string cityCode, DateTime birthDate)
        {
            Assert.Throws<Exception>(() => new Customer(string.Empty, cityCode, birthDate));
        }

        [Theory, AutoMoqData]
        public void Create_Customer_Should_Throw_Exception_When_CityCode_Is_Empty(string fullName, DateTime birthDate)
        {
            Assert.Throws<Exception>(() => new Domain.Customer(fullName, string.Empty, birthDate));
        }

        [Theory, AutoMoqData]
        public void Create_Customer_Should_Throw_Exception_When_BirthDate_Is_Invalid(string fullName, string cityCode)
        {
            Assert.Throws<Exception>(() => new Domain.Customer(fullName, cityCode, DateTime.Today));
        }

        [Theory, AutoMoqData]
        public void Create_Customer_Should_Success(string fullName, string cityCode, DateTime birthDate)
        {
            var sut = new Domain.Customer(fullName, cityCode, birthDate);

            sut.FullName.Should().Be(fullName);
            sut.CityCode.Should().Be(cityCode);
            sut.BirthDate.Should().Be(birthDate);
        }

        [Theory, AutoMoqData]
        public void SetFields_Should_Update_Fields(string fullName, string cityCode, DateTime birthDate, Domain.Customer sut)
        {
            sut.SetFields(fullName, cityCode, birthDate);

            sut.FullName.Should().Be(fullName);
            sut.CityCode.Should().Be(cityCode);
            sut.BirthDate.Should().Be(birthDate);
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
