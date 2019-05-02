using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.RecordStorageLocations.Commands;
using OLBIL.OncologyApplication.RecordStorageLocations.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class RecordStorageLocationsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<RecordStorageLocationModel>>> GetAll([FromQuery]GetRecordStorageLocationsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<RecordStorageLocationModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchRecordStorageLocationsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecordStorageLocationModel>> GetRecordStorageLocation(int id)
        {
            return Ok(await Mediator.Send(new GetRecordStorageLocationQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRecordStorageLocation([FromBody]RecordStorageLocationModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/beds/", await Mediator.Send(new CreateRecordStorageLocationCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateRecordStorageLocationCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRecordStorageLocation([FromBody]RecordStorageLocationModel model)
        {
            await Mediator.Send(new UpdateRecordStorageLocationCommand { Model = model });
            return NoContent();
        }
    }
}
