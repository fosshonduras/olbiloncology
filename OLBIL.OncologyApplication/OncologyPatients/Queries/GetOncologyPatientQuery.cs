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

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class GetOncologyPatientQuery: IRequest<OncologyPatientModel>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetOncologyPatientQuery, OncologyPatientModel>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OncologyPatientModel> Handle(GetOncologyPatientQuery request, CancellationToken cancellationToken)
            {
                var item = _mapper.Map<OncologyPatientModel>(await _context
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
