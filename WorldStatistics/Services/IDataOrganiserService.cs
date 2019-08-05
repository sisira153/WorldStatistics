using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldStatistics.Models;

namespace WorldStatistics.Services
{
    public interface IDataOrganiserService
    {
        Task<List<CountryDisplayDetails>> GetTopTenCountriesByGDP(string year);
    }
}
