using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Beds.Commands
{
    public class UpdateBedCommandHandler : IRequestHandler<UpdateBedCommand>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public UpdateBedCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBedCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var item = await _context.Beds
                .Where(p => p.BedId == model.BedId)
                .FirstOrDefaultAsync(cancellationToken);
            if (item == null)
            {
                throw new NotFoundException(nameof(Bed), nameof(model.BedId), model.BedId);
            }

            item.Name = model.Name;
            item.LongDescription = model.LongDescription;
            item.WardId = model.WardId.Value;
            item.BedStatusId = model.BedStatusId.Value;

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
