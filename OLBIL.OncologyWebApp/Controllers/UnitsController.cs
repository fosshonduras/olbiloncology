using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.HospitalUnits.Commands;
using OLBIL.OncologyApplication.HospitalUnits.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class UnitsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<UnitsListModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetUnitsListQuery()));
        }

        [HttpGet("{id}", Name = "GetUnit")]
        public async Task<ActionResult<UnitModel>> GetUnit(int id)
        {
            return Ok(await Mediator.Send(new GetUnitQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUnit([FromBody]UnitModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/units/", await Mediator.Send(new CreateUnitCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateUnitCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUnit([FromBody]UnitModel model)
        {
            await Mediator.Send(new UpdateUnitCommand { Model = model });
            return NoContent();
        }
    }
}
