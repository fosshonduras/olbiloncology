using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.TransfusionProductDetails.Commands;
using OLBIL.OncologyApplication.TransfusionProductDetails.Queries;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class TransfusionProductDetailsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<TransfusionProductDetailModel>>> GetAll([FromQuery]GetTransfusionProductDetailsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<TransfusionProductDetailModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchTransfusionProductDetailsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetTransfusionProductDetail")]
        public async Task<ActionResult<TransfusionProductDetailModel>> GetTransfusionProductDetail(int id)
        {
            return Ok(await Mediator.Send(new GetTransfusionProductDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateTransfusionProductDetail([FromBody]TransfusionProductDetailModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/TransfusionProductDetails/", await Mediator.Send(new CreateTransfusionProductDetailCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateTransfusionProductDetailCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTransfusionProductDetail([FromBody]TransfusionProductDetailModel model)
        {
            await Mediator.Send(new UpdateTransfusionProductDetailCommand { Model = model });
            return NoContent();
        }
    }
}
