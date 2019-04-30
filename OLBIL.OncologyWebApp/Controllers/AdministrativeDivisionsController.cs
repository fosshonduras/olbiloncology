using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.AdministrativeDivisions.Commands;
using OLBIL.OncologyApplication.AdministrativeDivisions.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class AdministrativeDivisionsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<AdministrativeDivisionModel>>> GetAll([FromQuery]GetAdministrativeDivisionsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<AdministrativeDivisionModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchAdministrativeDivisionsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetAdministrativeDivision")]
        public async Task<ActionResult<AdministrativeDivisionModel>> GetAdministrativeDivision(int id)
        {
            return Ok(await Mediator.Send(new GetAdministrativeDivisionQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAdministrativeDivision([FromBody]AdministrativeDivisionModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/administrativedivisions/", await Mediator.Send(new CreateAdministrativeDivisionCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateAdministrativeDivisionCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAdministrativeDivision([FromBody]AdministrativeDivisionModel model)
        {
            await Mediator.Send(new UpdateAdministrativeDivisionCommand { Model = model });
            return NoContent();
        }
    }
}
