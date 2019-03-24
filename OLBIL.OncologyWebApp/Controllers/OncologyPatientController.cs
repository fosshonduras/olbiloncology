using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatientsList;
using OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatient;
using OLBIL.OncologyApplication.OncologyPatients.Commands.CreateOncologyPatient;
using OLBIL.OncologyApplication.OncologyPatients.Commands.AttemptCreation;
using OLBIL.OncologyApplication.OncologyPatients.Commands.UpdatePatient;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class OncologyPatientsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<OncologyPatientsListModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetOncologyPatientsListQuery()));
        }

        [HttpGet("{id}", Name = "GetPatient")]
        public async Task<ActionResult<OncologyPatientModel>> GetPatient(int id)
        {
            return Ok(await Mediator.Send(new GetOncologyPatientQuery { Id = id }));
        }

        [HttpPost("attempt",Name = "AttemptCreation")]
        public async Task<ActionResult<OncologyPatientsListModel>> AttemptCreatePatient([FromBody]OncologyPatientModel model)
        {
            return Ok(await Mediator.Send(new AttemptOncologyPatientCreationCommand { Model = model}));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePatient([FromBody]OncologyPatientModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/oncologyPatient/", await Mediator.Send(new CreateOncologyPatientCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateOncologyPatientCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePatient([FromBody]OncologyPatientModel model)
        {
            await Mediator.Send(new UpdateOncologyPatientCommand { Model = model });
            return NoContent();
        }
    }
}
