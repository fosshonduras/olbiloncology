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

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class GetOncologyPatientQuery: IRequest<OncologyPatientModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetOncologyPatientQuery, OncologyPatientModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<OncologyPatientModel> Handle(GetOncologyPatientQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<OncologyPatientModel>(await Context
                    .OncologyPatients.Include(o => o.Person).Where(o => o.OncologyPatientId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(OncologyPatient), nameof(request.Id), request.Id);
                }

                return item;
            }
        }
    }
}
