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

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class UpdateHospitalUnitCommand: IRequest
    {
        public HospitalUnitModel Model { get; set; }

        public class Handler : IRequestHandler<UpdateHospitalUnitCommand>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateHospitalUnitCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await _context.HospitalUnits
                    .Where(p => p.HospitalUnitId == model.HospitalUnitId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(HospitalUnit), nameof(model.HospitalUnitId), model.HospitalUnitId);
                }

                item.Code = model.Code;
                item.Name = model.Name;

                await _context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
