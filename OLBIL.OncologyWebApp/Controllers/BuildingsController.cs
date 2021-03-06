﻿using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Buildings.Commands;
using OLBIL.OncologyApplication.Buildings.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class BuildingsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<BuildingModel>>> GetAll([FromQuery]GetBuildingsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<BuildingModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchBuildingsQuery{ SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetBuilding")]
        public async Task<ActionResult<BuildingModel>> GetBuilding(int id)
        {
            return Ok(await Mediator.Send(new GetBuildingQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBuilding([FromBody]BuildingModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/buildings/", await Mediator.Send(new CreateBuildingCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateBuildingCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBuilding([FromBody]BuildingModel model)
        {
            await Mediator.Send(new UpdateBuildingCommand { Model = model });
            return NoContent();
        }
    }
}
