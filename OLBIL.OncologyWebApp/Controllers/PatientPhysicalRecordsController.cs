using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.PatientPhysicalRecords.Commands;
using OLBIL.OncologyApplication.PatientPhysicalRecords.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class PatientPhysicalRecordsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<PatientPhysicalRecordModel>>> GetAll([FromQuery]GetPatientPhysicalRecordsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<PatientPhysicalRecordModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchPatientPhysicalRecordsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientPhysicalRecordModel>> GetPatientPhysicalRecord(int id)
        {
            return Ok(await Mediator.Send(new GetPatientPhysicalRecordQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePatientPhysicalRecord([FromBody]PatientPhysicalRecordModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/beds/", await Mediator.Send(new CreatePatientPhysicalRecordCommand { Model = model }));
            return Ok(await Mediator.Send(new CreatePatientPhysicalRecordCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePatientPhysicalRecord([FromBody]PatientPhysicalRecordModel model)
        {
            await Mediator.Send(new UpdatePatientPhysicalRecordCommand { Model = model });
            return NoContent();
        }
    }
}
