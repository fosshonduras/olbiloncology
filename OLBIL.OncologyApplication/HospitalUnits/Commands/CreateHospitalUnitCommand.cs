using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class CreateHospitalUnitCommand : IRequest<int>
    {
        public HospitalUnitModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<CreateHospitalUnitCommand, int>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<int> Handle(CreateHospitalUnitCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.HospitalUnits
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

                Context.HospitalUnits.Add(newRecord);
                await Context.SaveChangesAsync(cancellationToken);

                return newRecord.HospitalUnitId;
            }
        }
    }
}
