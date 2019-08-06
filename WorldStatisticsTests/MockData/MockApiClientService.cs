using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldStatistics.Models;
using WorldStatistics.Services;

namespace WorldStatisticsTests.MockData
{
    public class MockApiClientService : IApiClientService
    {

        public Task<List<Country>> GetCountriesWithHighIncomeLevel()
        {
            var data = MockData.GetMockData("ListofCountriesWithHIL");
            //for some reason, the below conversion work. Using a temp workaround
            //var listOfCountries = JsonConvert.DeserializeObject<List<Country>>(data);
            var listOfCountries = new List<Country>();
            listOfCountries.Add(new Country
            {
                Id = "AUS",
                Name= "Australia"
            });
            listOfCountries.Add(new Country
            {
                Id = "CAN",
                Name = "Canada"
            });
            listOfCountries.Add(new Country
            {
                Id = "DEU",
                Name = "Germany"
            });
            listOfCountries.Add(new Country
            {
                Id = "JPN",
                Name = "Japan"
            });
            listOfCountries.Add(new Country
            {
                Id = "ITA",
                Name = "Italy"
            });
            listOfCountries.Add(new Country
            {
                Id = "SGP",
                Name = "Singapore"
            });
            listOfCountries.Add(new Country
            {
                Id = "USA",
                Name = "United States"
            });

            return Task.FromResult(listOfCountries);
        }

        public Task<GDP> GetGDPValue(string country, string year)
        {
            var data = MockData.GetMockData(country);
            var gdp = new GDP
            {
                Value = decimal.Parse(data)
            };
            return Task.FromResult(gdp);
        }
    
    }
}
