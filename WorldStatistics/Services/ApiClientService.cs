using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorldStatistics.Models;
using static WorldStatistics.Constants;

namespace WorldStatistics.Services
{
    public class ApiClientService : IApiClientService
    {       
        private HttpClient _httpClient;
        public ApiClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<GDP>GetGDPValue(string countryId, string year)
        {
            var gdpRequest = ConstructCountryGDPRequestUri(countryId, year);
            var data = await GetData(gdpRequest);
            if (data.HasValues)
                return data[0].ToObject<GDP>();
            else
                return null;
        }

        private async Task<dynamic> GetData(Uri requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(content)[1];
        }

        private Uri ConstructCountryGDPRequestUri(string countryId, string year)
        {
            string relativePath = RequestPath.Country + countryId;
            relativePath += RequestPath.Indicator + Indicators.GDP;
            
            string queryString = RequestQueryParam.Format + Format.Json;
            queryString += "&" + RequestQueryParam.Date + year;

            string CompleteUrl = _httpClient.BaseAddress.ToString() + relativePath;

            var uriBuilder = new UriBuilder(CompleteUrl)
            {
                Query = queryString
            };
            return uriBuilder.Uri;
        }

        public async Task<List<Country>> GetCountriesWithHighIncomeLevel()
        {
            var requestString = "http://api.worldbank.org/v2/country/all?format=json&incomelevel=HIC";

            var pageCount = await GetPageCount(requestString);
            List<Country> listOfCountries = new List<Country>();
            for (int i = 1; i <= pageCount; i++)
            {
                var pageRequestString = requestString + "&" +RequestQueryParam.Page + i;
                var requestUri = new Uri(pageRequestString);
                var data = await GetData(requestUri);
                if(data.HasValues)
                    listOfCountries.AddRange(data.ToObject<List<Country>>());
            }
            return listOfCountries;
        }

        private async Task<int> GetPageCount(string requestString)
        {
            var requestUri = new Uri(requestString);
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<dynamic>(content)[0];
            return obj["pages"];
        }
    }
}
