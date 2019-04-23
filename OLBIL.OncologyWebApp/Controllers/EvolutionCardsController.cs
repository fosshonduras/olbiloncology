using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.EvolutionCards.Commands;
using OLBIL.OncologyApplication.EvolutionCards.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class EvolutionCardsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<EvolutionCardModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetEvolutionCardsListQuery()));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<EvolutionCardModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchEvolutionCardsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetEvolutionCard")]
        public async Task<ActionResult<EvolutionCardModel>> GetEvolutionCard(int id)
        {
            return Ok(await Mediator.Send(new GetEvolutionCardQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEvolutionCard([FromBody]EvolutionCardModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/evolutioncards/", await Mediator.Send(new CreateEvolutionCardCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateEvolutionCardCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEvolutionCard([FromBody]EvolutionCardModel model)
        {
            await Mediator.Send(new UpdateEvolutionCardCommand { Model = model });
            return NoContent();
        }
    }
}
