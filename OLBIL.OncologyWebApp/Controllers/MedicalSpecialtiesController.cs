using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.MedicalSpecialties.Commands;
using OLBIL.OncologyApplication.MedicalSpecialties.Queries;
using OLBIL.OncologyApplication.Models;
using System.Threading.Tasks;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class MedicalSpecialtiesController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<MedicalSpecialtyModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetMedicalSpecialtiesListQuery()));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<MedicalSpecialtyModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchMedicalSpecialtiesQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("{id}", Name = "GetMedicalSpecialty")]
        public async Task<ActionResult<MedicalSpecialtyModel>> GetMedicalSpecialty(int id)
        {
            return Ok(await Mediator.Send(new GetMedicalSpecialtyQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateMedicalSpecialty([FromBody]MedicalSpecialtyModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/medicalspecialties/", await Mediator.Send(new CreateMedicalSpecialtyCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateMedicalSpecialtyCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMedicalSpecialty([FromBody]MedicalSpecialtyModel model)
        {
            await Mediator.Send(new UpdateMedicalSpecialtyCommand { Model = model });
            return NoContent();
        }
    }
}
