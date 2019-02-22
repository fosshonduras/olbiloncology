using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyWebApp.DataAccess;
using OLBIL.OncologyWebApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OLBIL.OncologyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OncologyPatientController: ControllerBase
    {
        private readonly OncologyContext _context;

        public OncologyPatientController(OncologyContext context)
        {
            _context = context;
            if(_context.OncologyPatients.Count() == 0)
            {
                _context.OncologyPatients.Add(new OncologyPatient
                {
                    FirstName = "Patient0",
                    LastName = "Krankenz"
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<OncologyPatient>> GetAll()
        {
            return _context.OncologyPatients.ToList();
        }

        [HttpGet("{id}", Name = "GetPatient")]
        public ActionResult<OncologyPatient> GetPatient(int id)
        {
            var item = _context.OncologyPatients.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}
