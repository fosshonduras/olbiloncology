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
    public class UpdateHospitalUnitCommand: IRequest
    {
        public HospitalUnitModel Model { get; set; }

        public class Handler : HandlerBase, IRequestHandler<UpdateHospitalUnitCommand>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(UpdateHospitalUnitCommand request, CancellationToken cancellationToken)
            {
                var model = request.Model;
                var item = await Context.HospitalUnits
                    .Where(p => p.HospitalUnitId == model.HospitalUnitId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(HospitalUnit), nameof(model.HospitalUnitId), model.HospitalUnitId);
                }

                item.Code = model.Code;
                item.Name = model.Name;

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
