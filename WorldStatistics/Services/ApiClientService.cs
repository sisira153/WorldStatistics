using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorldStatistics.ErrorHandling;
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
        private async Task<(bool Response, dynamic DataObject)> GetData(Uri requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var dataObj = JsonConvert.DeserializeObject<dynamic>(content)[1];
                return (Response: true, DataObject: dataObj);
            }
            else
                return (Response: false, DataObject: null);

        }
        public async Task<GDP>GetGDPValue(string countryId, string year)
        {
            var gdpRequest = ConstructCountryGDPRequestUri(countryId, year);

            var (response, dataObject) = await GetData(gdpRequest);

            if (response && dataObject.HasValues)               
                return dataObject[0].ToObject<GDP>();
            else
                return null;
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
            var requestString = "http://api.worldbank.org/v2/country?format=json&incomelevel=HIC";

            var pageCount = await GetPageCount(requestString);

            var listOfCountries = new List<Country>();
            for (int i = 1; i <= pageCount; i++)
            {
                var pageRequestString = requestString + "&" +RequestQueryParam.Page + i;
                var requestUri = new Uri(pageRequestString);

                var (response, dataObject) = await GetData(requestUri); 

                if(response)
                    listOfCountries.AddRange(dataObject.ToObject<List<Country>>());
            }
            return listOfCountries;
        }

        private async Task<int> GetPageCount(string requestString)
        {
            var requestUri = new Uri(requestString);
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<dynamic>(content)[0];
                return obj["pages"];
            }
            else
                throw new RequestException("Request to World bank api failed");              
        }
    }
}
