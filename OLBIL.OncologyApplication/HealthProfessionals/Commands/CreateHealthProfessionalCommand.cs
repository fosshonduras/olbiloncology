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

namespace OLBIL.OncologyApplication.HealthProfessionals.Commands
{
    public class CreateHealthProfessionalCommand: IRequest<int>
    {
        public HealthProfessionalModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateHealthProfessionalCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateHealthProfessionalCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.HealthProfessionals
                    .Where(p => p.HealthProfessionalId == request.Model.HealthProfessionalId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(HealthProfessional), nameof(request.Model.HealthProfessionalId), request.Model.HealthProfessionalId);
                }
                var pModel = request.Model.Person;
                var personId = pModel?.PersonId;
                string governmentIDNumber = pModel?.GovernmentIDNumber;
                var person = await Context.People
                                .Where(p => p.PersonId == personId || p.GovernmentIDNumber == governmentIDNumber)
                                .FirstOrDefaultAsync(cancellationToken);

                if (person != null)
                {
                    var healthProfessional2 = Context.HealthProfessionals.Include(o => o.Person).FirstOrDefault(p => p.Person.GovernmentIDNumber == pModel.GovernmentIDNumber);
                    if (healthProfessional2 != null)
                    {
                        throw new AlreadyExistsException(nameof(HealthProfessional), nameof(pModel.GovernmentIDNumber), pModel.GovernmentIDNumber);
                    }
                }

                person = Mapper.Map<Person>(pModel);

                var newHealthProfessional = new HealthProfessional
                {
                    MainSpecialtyId = request.Model.MainSpecialtyId,
                    Person = person
                };

                Context.HealthProfessionals.Add(newHealthProfessional);
                await Context.SaveChangesAsync(cancellationToken);

                return newHealthProfessional.HealthProfessionalId;
            }
        }
    }
}
