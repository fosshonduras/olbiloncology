using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.PhysicalRecordTransfers.Commands;
using OLBIL.OncologyApplication.PhysicalRecordTransfers.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class PhysicalRecordTransfersController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<PhysicalRecordTransferModel>>> GetAll([FromQuery]GetPhysicalRecordTransfersListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<PhysicalRecordTransferModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchPhysicalRecordTransfersQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicalRecordTransferModel>> GetPhysicalRecordTransfer(int id)
        {
            return Ok(await Mediator.Send(new GetPhysicalRecordTransferQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePhysicalRecordTransfer([FromBody]PhysicalRecordTransferModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/beds/", await Mediator.Send(new CreatePhysicalRecordTransferCommand { Model = model }));
            return Ok(await Mediator.Send(new CreatePhysicalRecordTransferCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePhysicalRecordTransfer([FromBody]PhysicalRecordTransferModel model)
        {
            await Mediator.Send(new UpdatePhysicalRecordTransferCommand { Model = model });
            return NoContent();
        }
    }
}
