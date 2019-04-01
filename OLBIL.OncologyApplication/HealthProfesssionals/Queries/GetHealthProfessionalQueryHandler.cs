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

namespace OLBIL.OncologyApplication.HealthProfesssionals.Queries
{
    public class GetHealthProfessionalQueryHandler : IRequestHandler<GetHealthProfessionalQuery, HealthProfessionalModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public GetHealthProfessionalQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HealthProfessionalModel> Handle(GetHealthProfessionalQuery request, CancellationToken cancellationToken)
        {            
            var item = _mapper.Map<HealthProfessionalModel>(await _context
                .HealthProfessionals.Include(o => o.Person).Where(o => o.HealthProfessionalId == request.Id)
                .SingleOrDefaultAsync(cancellationToken));

            if(item == null)
            {
                throw new NotFoundException(nameof(HealthProfessional), nameof(request.Id), request.Id);
            }

            return item;
        }
    }
}
