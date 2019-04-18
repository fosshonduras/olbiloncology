using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands
{
    public class CreateOncologyPatientCommand: IRequest<int>
    {
        public OncologyPatientModel Model { get; set; }

        public class CreateOncologyPatientCommandHandler : IRequestHandler<CreateOncologyPatientCommand, int>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public CreateOncologyPatientCommandHandler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateOncologyPatientCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.OncologyPatients
                    .Where(p => p.OncologyPatientId == request.Model.OncologyPatientId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(OncologyPatient), nameof(request.Model.OncologyPatientId), request.Model.OncologyPatientId);
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
                    if (patient2 != null)
                    {
                        throw new AlreadyExistsException(nameof(OncologyPatient), nameof(pModel.GovernmentIDNumber), pModel.GovernmentIDNumber);
                    }
                }

                person = _mapper.Map<Person>(pModel);

                var newPatient = new OncologyPatient
                {
                    AdmissionDate = request.Model.AdmissionDate,
                    InformantsRelationship = request.Model.InformantsRelationship,
                    ReasonForReferral = request.Model.ReasonForReferral,
                    RegistrationDate = DateTime.Now,
                    Person = person
                };

                _context.OncologyPatients.Add(newPatient);
                await _context.SaveChangesAsync(cancellationToken);

                return newPatient.OncologyPatientId;
            }
        }
    }
}
