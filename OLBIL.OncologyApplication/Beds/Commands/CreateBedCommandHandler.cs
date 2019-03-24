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
    public class CreateBedCommandHandler : IRequestHandler<CreateBedCommand, int>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public CreateBedCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBedCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var item = await _context.Beds
                .Where(p => p.BedId == model.BedId)
                .FirstOrDefaultAsync(cancellationToken);
            if(item != null)
            {
                throw new AlreadyExistsException(nameof(Bed), nameof(model.BedId), model.BedId);
            }

            var newRecord = new Bed
            {
                Name = model.Name,
                LongDescription = model.LongDescription,
                WardId = model.WardId.Value,
                BedStatusId = model.BedStatusId.Value
            };

            _context.Beds.Add(newRecord);
            await _context.SaveChangesAsync(cancellationToken);

            return newRecord.BedId;
        }
    }
}
