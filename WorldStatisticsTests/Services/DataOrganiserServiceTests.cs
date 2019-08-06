using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldStatistics.Models;
using WorldStatistics.Services;
using WorldStatisticsTests.MockData;

namespace WorldStatisticsTests.Services
{
    public class DataOrganiserServiceTests
    {
        private IDataOrganiserService _dataOrganiser;
        [SetUp]
        public void Initialize()
        {            
            var mockApiClient = new MockApiClientService();
            _dataOrganiser = new DataOrganiserService(mockApiClient);
        }
        [Test]
        public async Task GetTopTenCountriesByGDP_ShouldReturnCorrectResult()
        {
            //Arrange
            string year = "2018";
            var topCountry = new CountryDisplayDetails
            {
                CountryName = "Japan",
                GDPValue = decimal.Parse("43534654785684565")
            };

            var bottomCountry = new CountryDisplayDetails
            {
                CountryName = "Germany",
                GDPValue = decimal.Parse("45645754433")
            };

            //Act
            var result = await _dataOrganiser.GetTopTenCountriesByGDP(year);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<CountryDisplayDetails>>();
            result.Count.ShouldBe(7);
            result[0].CountryName.ShouldBe(topCountry.CountryName);
            result[0].GDPValue.ShouldBe(topCountry.GDPValue);
            result[6].CountryName.ShouldBe(bottomCountry.CountryName);
            result[6].GDPValue.ShouldBe(bottomCountry.GDPValue);
        }
    }
}
