using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Customer.Repository.Test
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void Save_Should_Save_The_Customer_And_Should_Return_All_Count_As_Two()
        {
            var customer1 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));
            var customer2 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase("customer_db")
                .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer1);
                repository.Save(customer2);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.All().Count().Should().Be(2);
            }
        }

        [Fact]
        public void Delete_Should_Delete_The_Customer_And_Should_Return_All_Count_As_One()
        {
            var customer1 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));
            var customer2 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase("customer_db")
                .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer1);
                repository.Save(customer2);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Delete(customer1.Id);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.All().Count().Should().Be(1);
            }
        }

        [Fact]
        public void Update_Should_Update_The_Customer()
        {
            var customer = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase("customer_db")
                .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer);
                context.SaveChanges();
            }

            customer.SetFields("Caner T", "IZM", customer.BirthDate);

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Update(customer);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.Get(customer.Id);

                result.Should().NotBe(null);
                result.FullName.Should().Be(customer.FullName);
                result.CityCode.Should().Be(customer.CityCode);
                result.BirthDate.Should().Be(customer.BirthDate);
            }
        }

        [Fact]
        public void Find_Should_Fid_The_Customer_And_Should_Return_All_Count_As_One()
        {
            var customer1 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));
            var customer2 = new Domain.Customer("Caner Tosuner", "IZM", DateTime.Today.AddYears(28));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase("customer_db")
                .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer1);
                repository.Save(customer2);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.Find(c => c.CityCode == customer1.CityCode);
                result.Should().NotBeNull();
                result.Count().Should().Be(1);
            }
        }
    }
}
