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
    public class CreateHealthProfessionalCommand: IRequest<int>
    {
        public HealthProfessionalModel Model { get; set; }

        public class Handler : IRequestHandler<CreateHealthProfessionalCommand, int>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateHealthProfessionalCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.HealthProfessionals
                    .Where(p => p.HealthProfessionalId == request.Model.HealthProfessionalId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(HealthProfessional), nameof(request.Model.HealthProfessionalId), request.Model.HealthProfessionalId);
                }
                var pModel = request.Model.Person;
                var personId = pModel?.PersonId;
                string governmentIDNumber = pModel?.GovernmentIDNumber;
                var person = await _context.People
                                .Where(p => p.PersonId == personId || p.GovernmentIDNumber == governmentIDNumber)
                                .FirstOrDefaultAsync(cancellationToken);

                if (person != null)
                {
                    var healthProfessional2 = _context.HealthProfessionals.Include(o => o.Person).FirstOrDefault(p => p.Person.GovernmentIDNumber == pModel.GovernmentIDNumber);
                    if (healthProfessional2 != null)
                    {
                        throw new AlreadyExistsException(nameof(HealthProfessional), nameof(pModel.GovernmentIDNumber), pModel.GovernmentIDNumber);
                    }
                }

                person = _mapper.Map<Person>(pModel);

                var newHealthProfessional = new HealthProfessional
                {
                    Person = person
                };

                _context.HealthProfessionals.Add(newHealthProfessional);
                await _context.SaveChangesAsync(cancellationToken);

                return newHealthProfessional.HealthProfessionalId;
            }
        }
    }
}
