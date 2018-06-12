using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Housing.Forecast.Library.Models;
using Housing.Forecast.Service;

namespace Housing.Forecast.Testing.Library
{
    public class AddressTests
    {
        private Address getTestAddress()
        {
            Address address = new Address
            {
                Id = Guid.NewGuid(),
                AddressId = Guid.NewGuid(),
                Address1 = "123 test street",
                City = "Tampa",
                State = "Florida",
                PostalCode = "33617",
                Country = "US"
            };
            return address;
        }

        [Fact]
        public void DefaultAddressInvalidTest()
        {
            Address test = new Address();
            Assert.False(test.Validate());
        }

        [Fact]
        public void addressValidTest()
        {
            Address test = getTestAddress();
            Assert.True(test.Validate());
        }

        [Fact]
        public void addressIdInvalidTest()
        {
            Address test = getTestAddress();
            test.Id = Guid.Empty;
            Assert.False(test.Validate());
        }

        [Fact]
        public void addressAddressIdInvalidTest()
        {
            Address test = getTestAddress();
            test.AddressId = Guid.Empty;
            Assert.False(test.Validate());
        }

        [Fact]
        public void addressAddress1InvalidTest()
        {
            Address test = getTestAddress();
            test.Address1 = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void addressCityInvalidTest()
        {
            Address test = getTestAddress();
            test.City = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void addressStateInvalidTest()
        {
            Address test = getTestAddress();
            test.State = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void addressPostalCodeInvalidTest()
        {
            Address test = getTestAddress();
            test.PostalCode = "";
            Assert.False(test.Validate());
        }

        [Fact]
        public void addressCountryInvalidTest()
        {
            Address test = getTestAddress();
            test.Country = "";
            Assert.False(test.Validate());
        }
    }
}
