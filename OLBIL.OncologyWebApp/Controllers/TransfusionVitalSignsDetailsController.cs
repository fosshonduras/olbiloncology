using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Commands;
using OLBIL.OncologyApplication.TransfusionVitalSignsDetails.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class TransfusionVitalSignsDetailsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<TransfusionVitalSignsDetailModel>>> GetAll([FromQuery]GetTransfusionVitalSignsDetailsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<TransfusionVitalSignsDetailModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchTransfusionVitalSignsDetailsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetTransfusionVitalSignsDetail")]
        public async Task<ActionResult<TransfusionVitalSignsDetailModel>> GetTransfusionVitalSignsDetail(int id)
        {
            return Ok(await Mediator.Send(new GetTransfusionVitalSignsDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTransfusionVitalSignsDetail([FromBody]TransfusionVitalSignsDetailModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/TransfusionVitalSignsDetails/", await Mediator.Send(new CreateTransfusionVitalSignsDetailCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateTransfusionVitalSignsDetailCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTransfusionVitalSignsDetail([FromBody]TransfusionVitalSignsDetailModel model)
        {
            await Mediator.Send(new UpdateTransfusionVitalSignsDetailCommand { Model = model });
            return NoContent();
        }
    }
}
