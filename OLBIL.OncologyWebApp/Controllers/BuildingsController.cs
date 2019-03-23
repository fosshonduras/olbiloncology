using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Buildings.Commands;
using OLBIL.OncologyApplication.Buildings.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class BuildingsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<BuildingsListModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetBuildingsListQuery()));
        }

        [HttpGet("{id}", Name = "GetBuilding")]
        public async Task<ActionResult<BuildingModel>> GetBuilding(int id)
        {
            return Ok(await Mediator.Send(new GetBuildingQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePatient([FromBody]BuildingModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/buildings/", await Mediator.Send(new CreateBuildingCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateBuildingCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePatient([FromBody]BuildingModel model)
        {
            await Mediator.Send(new UpdateBuildingCommand { Model = model });
            return NoContent();
        }
    }
}
