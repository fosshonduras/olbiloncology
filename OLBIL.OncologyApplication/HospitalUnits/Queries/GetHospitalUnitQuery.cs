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

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetHospitalUnitQuery: IRequest<HospitalUnitModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetHospitalUnitQuery, HospitalUnitModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<HospitalUnitModel> Handle(GetHospitalUnitQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<HospitalUnitModel>(await Context
                    .HospitalUnits.Where(o => o.HospitalUnitId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(HospitalUnit), nameof(item.HospitalUnitId), request.Id);
                }

                return item;
            }
        }
    }
}
