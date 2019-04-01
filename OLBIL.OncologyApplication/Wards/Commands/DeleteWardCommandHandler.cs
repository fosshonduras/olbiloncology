using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class DeleteWardCommandHandler: IRequestHandler<DeleteWardCommand>
    {
                private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public DeleteWardCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteWardCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Wards
                .Where(p => p.WardId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (item == null)
            {
                throw new NotFoundException(nameof(Ward), nameof(item.WardId), request.Id);
            }

            _context.Wards.Remove(item);

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
