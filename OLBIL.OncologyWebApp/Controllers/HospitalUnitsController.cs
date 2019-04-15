using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.HospitalUnits.Commands;
using OLBIL.OncologyApplication.HospitalUnits.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class HospitalUnitsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<HospitalUnitsListModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetHospitalUnitsListQuery()));
        }

        [HttpGet("search")]
        public async Task<ActionResult<HospitalUnitsListModel>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchHospitalUnitsQuery{ SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetHospitalUnit")]
        public async Task<ActionResult<HospitalUnitModel>> GetHospitalUnit(int id)
        {
            return Ok(await Mediator.Send(new GetHospitalUnitQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateHospitalUnit([FromBody]HospitalUnitModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/hospitalunits/", await Mediator.Send(new CreateHospitalUnitCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateHospitalUnitCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateHospitalUnit([FromBody]HospitalUnitModel model)
        {
            await Mediator.Send(new UpdateHospitalUnitCommand { Model = model });
            return NoContent();
        }
    }
}
