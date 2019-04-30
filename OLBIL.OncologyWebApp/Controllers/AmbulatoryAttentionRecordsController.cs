using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Commands;
using OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Queries;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.DTOs;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class AmbulatoryAttentionRecordsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<AmbulatoryAttentionRecordModel>>> GetAll([FromQuery]GetAmbulatoryAttentionRecordsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<AmbulatoryAttentionRecordModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchAmbulatoryAttentionRecordsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("at1-report")]
        public async Task<ActionResult<ListModel<AT1ReportItemDTO>>> GetReport([FromQuery] GetAT1ReportQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}", Name = "GetAmbulatoryAttentionRecord")]
        public async Task<ActionResult<AmbulatoryAttentionRecordModel>> GetAmbulatoryAttentionRecord(int id)
        {
            return Ok(await Mediator.Send(new GetAmbulatoryAttentionRecordQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAmbulatoryAttentionRecord([FromBody]AmbulatoryAttentionRecordModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/ambulatoryattentionrecords/", await Mediator.Send(new CreateAmbulatoryAttentionRecordCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateAmbulatoryAttentionRecordCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAmbulatoryAttentionRecord([FromBody]AmbulatoryAttentionRecordModel model)
        {
            await Mediator.Send(new UpdateAmbulatoryAttentionRecordCommand { Model = model });
            return NoContent();
        }
    }
}
