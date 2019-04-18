using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands
{
    public class AttemptOncologyPatientCreationCommand: IRequest<ListModel<OncologyPatientModel>>
    {
        public OncologyPatientModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<AttemptOncologyPatientCreationCommand, ListModel<OncologyPatientModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<OncologyPatientModel>> Handle(AttemptOncologyPatientCreationCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var matches = Context.OncologyPatients.Include(o => o.Person)
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
                return new ListModel<OncologyPatientModel>
                {
                    Items = await matches.ProjectTo<OncologyPatientModel>(Mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
