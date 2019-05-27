using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.BloodTransfusions.Commands;
using OLBIL.OncologyApplication.BloodTransfusions.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class BloodTransfusionsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<BloodTransfusionModel>>> GetAll([FromQuery]GetBloodTransfusionsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<BloodTransfusionModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchBloodTransfusionsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetBloodTransfusion")]
        public async Task<ActionResult<BloodTransfusionModel>> GetBloodTransfusion(int id)
        {
            return Ok(await Mediator.Send(new GetBloodTransfusionQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBloodTransfusion([FromBody]BloodTransfusionModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/BloodTransfusions/", await Mediator.Send(new CreateBloodTransfusionCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateBloodTransfusionCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBloodTransfusion([FromBody]BloodTransfusionModel model)
        {
            await Mediator.Send(new UpdateBloodTransfusionCommand { Model = model });
            return NoContent();
        }
    }
}
