using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatientsList;
using OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatient;
using OLBIL.OncologyApplication.OncologyPatients.Commands.CreateOncologyPatient;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class OncologyPatientController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<List<OncologyPatientsListModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetOncologyPatientsListQuery()));
        }

        [HttpGet("{id}", Name = "GetPatient")]
        public async Task<ActionResult<OncologyPatientModel>> GetPatient(int id)
        {
            return Ok(await Mediator.Send(new GetOncologyPatientQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePatient([FromBody]OncologyPatientModel model)
        {
            return Ok(await Mediator.Send(new CreateOncologyPatientCommand { Model = model }));
        }
    }
}
