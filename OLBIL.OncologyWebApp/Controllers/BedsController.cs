﻿using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Beds.Commands;
using OLBIL.OncologyApplication.Beds.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class BedsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<BedModel>>> GetAll([FromQuery]GetBedsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<BedModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchBedsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BedModel>> GetBed(int id)
        {
            return Ok(await Mediator.Send(new GetBedQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBed([FromBody]BedModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/beds/", await Mediator.Send(new CreateBedCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateBedCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBed([FromBody]BedModel model)
        {
            await Mediator.Send(new UpdateBedCommand { Model = model });
            return NoContent();
        }
    }
}
