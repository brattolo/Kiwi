using FakeItEasy;
using FluentAssertions;
using Kiwi.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class CustomerControllerTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetCustomers_WhenCalled_Returns200WithCustomers()
        {
            // arrange
            var customersExpected = new List<CustomerModel>() {
                 new CustomerModel(),
                 new CustomerModel(),
                 new CustomerModel()
           };
            var fakeCustomerListQuery = A.Fake<IGetCustomersListQuery>();
            A.CallTo(() => fakeCustomerListQuery.Execute()).Returns(customersExpected);
            var customerControlller = new CustomersController(fakeCustomerListQuery);

            // act
            IActionResult result = await customerControlller.Get();

            // assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var customersResult = okResult.Value.Should().BeAssignableTo<List<CustomerModel>>().Subject;
            customersResult.Should().BeEquivalentTo(customersExpected);
        }
    }
}