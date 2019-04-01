using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyDomain.Entities;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class CreateHospitalUnitCommandHandler : IRequestHandler<CreateHospitalUnitCommand, int>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public CreateHospitalUnitCommandHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateHospitalUnitCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var item = await _context.Units
                .Where(p => p.HospitalUnitId == model.HospitalUnitId)
                .FirstOrDefaultAsync(cancellationToken);
            if(item != null)
            {
                throw new AlreadyExistsException(nameof(HospitalUnit), nameof(model.HospitalUnitId), model.HospitalUnitId);
            }

            var newRecord = new HospitalUnit
            {
                Name = model.Name,
                Code = model.Code,
            };

            _context.Units.Add(newRecord);
            await _context.SaveChangesAsync(cancellationToken);

            return newRecord.HospitalUnitId;
        }
    }
}
