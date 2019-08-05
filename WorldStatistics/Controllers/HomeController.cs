using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldStatistics.Models;
using WorldStatistics.Services;

namespace WorldStatistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IDataOrganiserService _dataOrganiser;
        public HomeController(IDataOrganiserService dataOrganiser)
        {
            _dataOrganiser = dataOrganiser;
        }
        [HttpGet]
        public async Task<ActionResult<List<CountryDisplayDetails>>>Get()
        {
            string year = "2018";
            var response = await _dataOrganiser.GetTopTenCountriesByGDP(year);
            return Ok(response);
        }

    }
}
