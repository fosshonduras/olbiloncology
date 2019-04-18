using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Countries.Commands;
using OLBIL.OncologyApplication.Countries.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class CountriesController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<CountryModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCountriesListQuery()));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<CountryModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchCountriesQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetCountry")]
        public async Task<ActionResult<CountryModel>> GetCountry(int id)
        {
            return Ok(await Mediator.Send(new GetCountryQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCountry([FromBody]CountryModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/wards/", await Mediator.Send(new CreateCountryCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateCountryCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCountry([FromBody]CountryModel model)
        {
            await Mediator.Send(new UpdateCountryCommand { Model = model });
            return NoContent();
        }
    }
}
