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
        public async Task<ActionResult<ListModel<WardModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetWardsListQuery()));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<WardModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchWardsQuery { SearchTerm = searchTerm }));
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
