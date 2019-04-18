using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyData;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class DeleteHospitalUnitCommand: IRequest
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<DeleteHospitalUnitCommand>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<Unit> Handle(DeleteHospitalUnitCommand request, CancellationToken cancellationToken)
            {
                var item = await Context.HospitalUnits
                    .Where(p => p.HospitalUnitId == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                if (item == null)
                {
                    throw new NotFoundException(nameof(HospitalUnit), nameof(item.HospitalUnitId), request.Id);
                }

                Context.HospitalUnits.Remove(item);

                await Context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
