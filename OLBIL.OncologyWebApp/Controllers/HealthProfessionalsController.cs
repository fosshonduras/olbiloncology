using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.HealthProfessionals.Commands;
using OLBIL.OncologyApplication.HealthProfessionals.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class HealthProfessionalsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<HealthProfessionalModel>>> GetAll([FromQuery]GetHealthProfessionalsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<HealthProfessionalModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchHealthProfessionalsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetHealthProfessional")]
        public async Task<ActionResult<HealthProfessionalModel>> GetHealthProfessional(int id)
        {
            return Ok(await Mediator.Send(new GetHealthProfessionalQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateHealthProfessional([FromBody]HealthProfessionalModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/healthprofessional/", await Mediator.Send(new CreateHealthProfessionalCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateHealthProfessionalCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateHealthProfessional([FromBody]HealthProfessionalModel model)
        {
            await Mediator.Send(new UpdateHealthProfessionalCommand { Model = model });
            return NoContent();
        }
    }
}
