using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.AppointmentReasons.Commands;
using OLBIL.OncologyApplication.AppointmentReasons.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class AppointmentReasonsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<AppointmentReasonModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAppointmentReasonsListQuery()));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<AppointmentReasonModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchAppointmentReasonsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetAppointmentReason")]
        public async Task<ActionResult<AppointmentReasonModel>> GetAppointmentReason(int id)
        {
            return Ok(await Mediator.Send(new GetAppointmentReasonQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAppointmentReason([FromBody]AppointmentReasonModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/appointmentreason/", await Mediator.Send(new CreateAppointmentReasonCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateAppointmentReasonCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAppointmentReason([FromBody]AppointmentReasonModel model)
        {
            await Mediator.Send(new UpdateAppointmentReasonCommand { Model = model });
            return NoContent();
        }
    }
}
