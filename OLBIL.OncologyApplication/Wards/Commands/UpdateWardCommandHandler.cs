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
            var ward = await _context.Wards
                .Where(p => p.WardId == model.WardId)
                .FirstOrDefaultAsync(cancellationToken);
            if (ward == null)
            {
                throw new NotFoundException(nameof(Ward), nameof(model.WardId), model.WardId);
            }

            ward.BuildingId = model.BuildingId;
            ward.FloorNumber = model.FloorNumber;
            ward.Name = model.Name;
            ward.UnitId = model.UnitId;
            ward.WardGenderId = model.WardGenderId;
            ward.WardStatusId = model.WardStatusId;

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}