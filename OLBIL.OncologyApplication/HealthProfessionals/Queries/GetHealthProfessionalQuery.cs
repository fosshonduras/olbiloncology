using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HealthProfessionals.Queries
{
    public class GetHealthProfessionalQuery: IRequest<HealthProfessionalModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetHealthProfessionalQuery, HealthProfessionalModel>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<HealthProfessionalModel> Handle(GetHealthProfessionalQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<HealthProfessionalModel>(await Context
                    .HealthProfessionals.Include(o => o.Person).Where(o => o.HealthProfessionalId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(HealthProfessional), nameof(request.Id), request.Id);
                }

                return item;
            }
        }
    }
}
