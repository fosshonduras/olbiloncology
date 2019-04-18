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
    public class CreateHospitalUnitCommand : IRequest<int>
    {
        public HospitalUnitModel Model { get; set; }

        public class Handler : IRequestHandler<CreateHospitalUnitCommand, int>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public Handler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateHospitalUnitCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await _context.HospitalUnits
                    .Where(p => p.HospitalUnitId == model.HospitalUnitId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    throw new AlreadyExistsException(nameof(HospitalUnit), nameof(model.HospitalUnitId), model.HospitalUnitId);
                }

                var newRecord = new HospitalUnit
                {
                    Name = model.Name,
                    Code = model.Code,
                };

                _context.HospitalUnits.Add(newRecord);
                await _context.SaveChangesAsync(cancellationToken);

                return newRecord.HospitalUnitId;
            }
        }
    }
}
