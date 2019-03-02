using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.DataAccess;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyApplication.Models;
using System;
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
                var newPerson = new Person
                {
                    GovernmentIDNumber = "0101-1001-00101",
                    FirstName = "Patient0",
                    LastName = "Krankenz"
                };
                _context.OncologyPatients.Add(new OncologyPatient
                {
                    RegistrationDate= DateTime.Now,
                    Person = newPerson
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<OncologyPatient>> GetAll()
        {
            return _context.OncologyPatients.Include(o => o.Person).ToList();
        }

        [HttpGet("{id}", Name = "GetPatient")]
        public ActionResult<OncologyPatient> GetPatient(int id)
        {
            var item = _context.OncologyPatients.Include(o => o.Person).FirstOrDefault(o => o.OncologyPatientId == id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public ActionResult<OncologyPatient> CreatePatient(OncologyPatientModel model)
        {
            var patient = _context.OncologyPatients.FirstOrDefault(p => p.OncologyPatientId == model.OncologyPatientId);
            if(patient != null)
            {
                return this.BadRequest("Patient already registered");
            }
            var pModel = model.Person;
            var personId = pModel?.PersonId;
            string governmentIDNumber = pModel?.GovernmentIDNumber;
            var person = _context.People.FirstOrDefault(p => p.PersonId == personId || p.GovernmentIDNumber == governmentIDNumber);

            if (person != null)
            {
                var patient2 = _context.OncologyPatients.Include(o => o.Person).FirstOrDefault(p => p.Person.GovernmentIDNumber == pModel.GovernmentIDNumber);
                if(patient2 != null)
                {
                    return BadRequest("Patient with same Id number already registered");
                }
            }

            person = new Person
            {
                GovernmentIDNumber = pModel.GovernmentIDNumber,
                FirstName = pModel.FirstName,
                LastName = pModel.LastName
            };

            var newPatient = new OncologyPatient
            {
                RegistrationDate = DateTime.Now,
                Person = person
            };

            _context.OncologyPatients.Add(newPatient);
            _context.SaveChanges();

            return Ok(newPatient);
        }
    }
}
