using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Commands
{
    public class DeleteHealthProfessionalCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteHealthProfessionalCommand>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteHealthProfessionalCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.HealthProfessionals
                    .Where(p => p.HealthProfessionalId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(HealthProfessional), nameof(item.HealthProfessionalId), request.Id);
                }

                _context.HealthProfessionals.Remove(item);

                await _context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
