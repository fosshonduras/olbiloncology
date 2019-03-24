using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyCore.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public UpdateUnitCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var item = await _context.Units
                .Where(p => p.UnitId == model.UnitId)
                .FirstOrDefaultAsync(cancellationToken);
            if (item == null)
            {
                throw new NotFoundException(nameof(HospitalUnit), nameof(model.UnitId), model.UnitId);
            }

            item.Code = model.Code;
            item.Name = model.Name;

            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
