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
    public class CreateWardCommandHandler : IRequestHandler<CreateWardCommand, int>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public CreateWardCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateWardCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var ward = await _context.Wards
                .Where(p => p.WardId == model.WardId)
                .FirstOrDefaultAsync(cancellationToken);
            if(ward != null)
            {
                throw new AlreadyExistsException(nameof(Ward), nameof(model.WardId), model.WardId);
            }

            var newRecord = new Ward
            {
                Name = model.Name,
                BuildingId = model.BuildingId.Value,
                FloorNumber = model.FloorNumber.Value,
                UnitId = model.UnitId.Value,
                WardGenderId = model.WardGenderId.Value,
                WardStatusId = model.WardStatusId.Value
            };

            _context.Wards.Add(newRecord);
            await _context.SaveChangesAsync(cancellationToken);

            return newRecord.WardId;
        }
    }
}
