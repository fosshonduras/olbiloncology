using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Commands
{
    public class UpdateHealthProfessionalCommand: IRequest
    {
        public HealthProfessionalModel Model { get; set; }

        public class Handler : IRequestHandler<UpdateHealthProfessionalCommand>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateHealthProfessionalCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.HealthProfessionals
                    .Where(p => p.HealthProfessionalId == request.Model.HealthProfessionalId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(HealthProfessional), nameof(request.Model.HealthProfessionalId), request.Model.HealthProfessionalId);
                }
                var pModel = request.Model.Person;
                var personId = pModel?.PersonId;
                var person = await _context.People
                                .Where(p => p.PersonId == personId)
                                .FirstOrDefaultAsync(cancellationToken);

                if (person == null)
                {
                    throw new NotFoundException(nameof(HealthProfessional), nameof(pModel.GovernmentIDNumber), pModel.GovernmentIDNumber);
                }

                MapPersonDetails(pModel, person);
                MapHealthProfessionalDetails(request, item);

                await _context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }

            private void MapHealthProfessionalDetails(UpdateHealthProfessionalCommand request, HealthProfessional healthProfessional)
            {

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
