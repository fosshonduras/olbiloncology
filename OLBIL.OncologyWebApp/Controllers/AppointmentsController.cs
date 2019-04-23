using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.Appointments.Commands;
using OLBIL.OncologyApplication.Appointments.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class AppointmentsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<AppointmentModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAppointmentsListQuery()));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<AppointmentModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchAppointmentsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetAppointment")]
        public async Task<ActionResult<AppointmentModel>> GetAppointment(int id)
        {
            return Ok(await Mediator.Send(new GetAppointmentQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAppointment([FromBody]AppointmentModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/appointment/", await Mediator.Send(new CreateAppointmentCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateAppointmentCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAppointment([FromBody]AppointmentModel model)
        {
            await Mediator.Send(new UpdateAppointmentCommand { Model = model });
            return NoContent();
        }
    }
}
