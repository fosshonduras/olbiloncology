using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands
{
    public class UpdateOncologyPatientCommand: IRequest
    {
        public OncologyPatientModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateOncologyPatientCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdateOncologyPatientCommand request, CancellationToken cancellationToken)
            {
                var patient = await Context.OncologyPatients
                    .Where(p => p.OncologyPatientId == request.Model.OncologyPatientId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (patient == null)
                {
                    throw new NotFoundException(nameof(OncologyPatient), nameof(request.Model.OncologyPatientId), request.Model.OncologyPatientId);
                }
                var pModel = request.Model.Person;
                var personId = pModel?.PersonId;
                var person = await Context.People
                                .Where(p => p.PersonId == personId)
                                .FirstOrDefaultAsync(cancellationToken);

                if (person == null)
                {
                    throw new NotFoundException(nameof(OncologyPatient), nameof(pModel.GovernmentIDNumber), pModel.GovernmentIDNumber);

                }

                MapPersonDetails(pModel, person);
                MapPatientDetails(request, patient);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }

            private void MapPatientDetails(UpdateOncologyPatientCommand request, OncologyPatient patient)
            {
                patient.OncologyPatientId = request.Model.OncologyPatientId.Value;
                patient.RegistrationDate = request.Model.RegistrationDate;
                patient.AdmissionDate = request.Model.AdmissionDate;
                patient.InformantsRelationship = request.Model.InformantsRelationship;
                patient.ReasonForReferral = request.Model.ReasonForReferral;
            }

            private void MapPersonDetails(PersonModel pModel, Person person)
            {
                person.FirstName = pModel.FirstName;
                person.MiddleName = pModel.MiddleName;
                person.LastName = pModel.LastName;
                person.AdditionalLastName = pModel.AdditionalLastName;
                person.PreferredName = pModel.PreferredName;
                person.GovernmentIDNumber = pModel.GovernmentIDNumber;
                person.Address = pModel.Address;
                person.AddressLine2 = pModel.AddressLine2;
                person.City = pModel.City;
                person.State = pModel.State;
                person.Country = pModel.Country;
                person.HomePhone = pModel.HomePhone;
                person.MobilePhone = pModel.MobilePhone;
                person.Nationality = pModel.Nationality;
                person.Race = pModel.Race;
                person.Gender = pModel.Gender;
                person.Birthdate = pModel.Birthdate;
                person.Birthplace = pModel.Birthplace;
                person.FamilyStatus = pModel.FamilyStatus;
                person.SchoolLevel = pModel.SchoolLevel;
                person.MethodOfTranspotation = pModel.MethodOfTranspotation;
            }
        }
    }
}
