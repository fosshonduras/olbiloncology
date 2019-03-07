using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands.CreateOncologyPatient
{
    public class CreateOncologyPatientCommandHandler : IRequestHandler<CreateOncologyPatientCommand, int>
    {
        private readonly OncologyContext _context;
        public CreateOncologyPatientCommandHandler(OncologyContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOncologyPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _context.OncologyPatients
                .Where(p => p.OncologyPatientId == request.Model.OncologyPatientId)
                .FirstOrDefaultAsync(cancellationToken);
            if(patient != null)
            {
                throw new AlreadyExistsException(nameof(OncologyPatient), request.Model.OncologyPatientId);
            }
            var pModel = request.Model.Person;
            var personId = pModel?.PersonId;
            string governmentIDNumber = pModel?.GovernmentIDNumber;
            var person = await _context.People
                            .Where(p => p.PersonId == personId || p.GovernmentIDNumber == governmentIDNumber)
                            .FirstOrDefaultAsync(cancellationToken);

            if (person != null)
            {
                var patient2 = _context.OncologyPatients.Include(o => o.Person).FirstOrDefault(p => p.Person.GovernmentIDNumber == pModel.GovernmentIDNumber);
                if(patient2 != null)
                {
                    throw new AlreadyExistsException(nameof(OncologyPatient), pModel.GovernmentIDNumber);
                }
            }

            person = new Person
            {
                GovernmentIDNumber = pModel.GovernmentIDNumber,
                FirstName = pModel.FirstName,
                MiddleName = pModel.MiddleName,
                LastName = pModel.LastName,
                AdditionalLastName = pModel.AdditionalLastName,
                Birthdate = pModel.Birthdate,
                Birthplace = pModel.Birthplace
            };

            var newPatient = new OncologyPatient
            {
                RegistrationDate = DateTime.Now,
                Person = person
            };

            _context.OncologyPatients.Add(newPatient);
            await _context.SaveChangesAsync(cancellationToken);

            return newPatient.OncologyPatientId;
        }
    }
}
