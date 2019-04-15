using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands
{
    public class AttemptOncologyPatientCreationCommandHandler : IRequestHandler<AttemptOncologyPatientCreationCommand, OncologyPatientsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public AttemptOncologyPatientCreationCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OncologyPatientsListModel> Handle(AttemptOncologyPatientCreationCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var matches = _context.OncologyPatients.Include(o => o.Person)
                .Where(o =>
                    o.Person != null &&
                    (
                        (o.Person.FirstName != null && o.Person.FirstName == model.Person.FirstName) ||
                        (o.Person.MiddleName != null && o.Person.MiddleName == model.Person.MiddleName) ||
                        (o.Person.LastName != null && o.Person.LastName == model.Person.LastName) ||
                        (o.Person.AdditionalLastName != null && o.Person.AdditionalLastName == model.Person.AdditionalLastName) ||
                        (o.Person.Birthdate != null && o.Person.Birthdate == model.Person.Birthdate) ||
                        (o.Person.GovernmentIDNumber != null && o.Person.GovernmentIDNumber == model.Person.GovernmentIDNumber)
                    )
                );
            return new OncologyPatientsListModel
            {
                Items = await matches.ProjectTo<OncologyPatientModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };
        }
    }
}
