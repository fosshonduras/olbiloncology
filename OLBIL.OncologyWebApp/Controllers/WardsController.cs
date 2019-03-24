using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.Wards.Commands;
using OLBIL.OncologyApplication.Wards.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class WardsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<WardsListModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetWardsListQuery()));
        }

        [HttpGet("{id}", Name = "GetWard")]
        public async Task<ActionResult<WardModel>> GetWard(int id)
        {
            return Ok(await Mediator.Send(new GetWardQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateWard([FromBody]WardModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/wards/", await Mediator.Send(new CreateWardCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateWardCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateWard([FromBody]WardModel model)
        {
            await Mediator.Send(new UpdateWardCommand { Model = model });
            return NoContent();
        }
    }
}
