using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
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

        public class Handler : HandlerBase, IRequestHandler<CreateOncologyPatientCommand, int>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateOncologyPatientCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.OncologyPatients
                    .Where(p => p.OncologyPatientId == request.Model.OncologyPatientId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(OncologyPatient), nameof(request.Model.OncologyPatientId), request.Model.OncologyPatientId);
                }
                var pModel = request.Model.Person;
                var personId = pModel?.PersonId;
                string governmentIDNumber = pModel?.GovernmentIDNumber;
                var person = await Context.People
                                .Where(p => p.PersonId == personId || p.GovernmentIDNumber == governmentIDNumber)
                                .FirstOrDefaultAsync(cancellationToken);

                if (person != null)
                {
                    var patient2 = Context.OncologyPatients.Include(o => o.Person).FirstOrDefault(p => p.Person.GovernmentIDNumber == pModel.GovernmentIDNumber);
                    if (patient2 != null)
                    {
                        throw new AlreadyExistsException(nameof(OncologyPatient), nameof(pModel.GovernmentIDNumber), pModel.GovernmentIDNumber);
                    }
                }

                person = Mapper.Map<Person>(pModel);

                var newPatient = new OncologyPatient
                {
                    AdmissionDate = request.Model.AdmissionDate,
                    InformantsRelationship = request.Model.InformantsRelationship,
                    ReasonForReferral = request.Model.ReasonForReferral,
                    RegistrationDate = DateTime.Now,
                    Person = person
                };

                Context.OncologyPatients.Add(newPatient);
                await Context.SaveChangesAsync(cancellationToken);

                return newPatient.OncologyPatientId;
            }
        }
    }
}
