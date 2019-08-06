using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldStatistics.Models;

namespace WorldStatistics.Services
{
    public class DataOrganiserService : IDataOrganiserService
    {
        IApiClientService _apiClientService;
        public DataOrganiserService(IApiClientService apiClient)
        {
            _apiClientService = apiClient;
        }
        public async Task<List<CountryDisplayDetails>> GetTopTenCountriesByGDP(string year)
        {
            var listOfCountriesWithHIL = await _apiClientService.GetCountriesWithHighIncomeLevel();

            List<CountryDisplayDetails> displayList = new List<CountryDisplayDetails>();
            decimal minGDP = 0.0m, maxGDP = 0.0m;

            foreach (Country country in listOfCountriesWithHIL)
            {
                var gdp = await _apiClientService.GetGDPValue(country.Id, year);

                if (gdp == null ||  gdp.Value == null|| gdp.Value < minGDP) continue;

                displayList.Add(new CountryDisplayDetails
                {
                    CountryName = country.Name,
                    GDPValue = gdp.Value
                });

                displayList = displayList.OrderByDescending(x => x.GDPValue).ToList();
                   
                if (displayList.Count > 10)
                {
                    displayList = displayList.GetRange(0, 10);
                    maxGDP = displayList[0].GDPValue.GetValueOrDefault();
                    minGDP = displayList[9].GDPValue.GetValueOrDefault();
                }
            }

            return displayList;
        }

    }
}
