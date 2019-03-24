using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class UpdateWardCommandHandler : IRequestHandler<UpdateWardCommand>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public UpdateWardCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateWardCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var item = await _context.Wards
                .Where(p => p.WardId == model.WardId)
                .FirstOrDefaultAsync(cancellationToken);
            if (item == null)
            {
                throw new NotFoundException(nameof(Ward), nameof(model.WardId), model.WardId);
            }

            item.BuildingId = model.BuildingId.Value;
            item.FloorNumber = model.FloorNumber.Value;
            item.Name = model.Name;
            item.UnitId = model.UnitId.Value;
            item.WardGenderId = model.WardGenderId.Value;
            item.WardStatusId = model.WardStatusId.Value;

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}