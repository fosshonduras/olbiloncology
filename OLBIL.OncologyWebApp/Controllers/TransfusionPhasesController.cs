using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.TransfusionPhases.Commands;
using OLBIL.OncologyApplication.TransfusionPhases.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class TransfusionPhasesController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<TransfusionPhaseModel>>> GetAll([FromQuery]GetTransfusionPhasesListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<TransfusionPhaseModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchTransfusionPhasesQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetTransfusionPhase")]
        public async Task<ActionResult<TransfusionPhaseModel>> GetTransfusionPhase(int id)
        {
            return Ok(await Mediator.Send(new GetTransfusionPhaseQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTransfusionPhase([FromBody]TransfusionPhaseModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/TransfusionPhases/", await Mediator.Send(new CreateTransfusionPhaseCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateTransfusionPhaseCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTransfusionPhase([FromBody]TransfusionPhaseModel model)
        {
            await Mediator.Send(new UpdateTransfusionPhaseCommand { Model = model });
            return NoContent();
        }
    }
}
