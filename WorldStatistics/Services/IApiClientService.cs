using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorldStatistics.Models;

namespace WorldStatistics.Services
{
    public interface IApiClientService
    {
        Task<List<Country>> GetCountriesWithHighIncomeLevel();
        Task<GDP> GetGDPValue(string country, string year);
    }
}
