using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.Diagnosiss.Commands;
using OLBIL.OncologyApplication.Diagnosiss.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class DiagnosesController: OlbilController
    {
        [HttpGet]

        public async Task<ActionResult<ListModel<DiagnosisModel>>> GetAll([FromQuery]GetDiagnosesListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<DiagnosisModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchDiagnosesQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetDiagnosis")]
        public async Task<ActionResult<DiagnosisModel>> GetDiagnosis(int id)
        {
            return Ok(await Mediator.Send(new GetDiagnosisQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDiagnosis([FromBody]DiagnosisModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/diagnoses/", await Mediator.Send(new CreateDiagnosisCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateDiagnosisCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDiagnosis([FromBody]DiagnosisModel model)
        {
            await Mediator.Send(new UpdateDiagnosisCommand { Model = model });
            return NoContent();
        }
    }
}
