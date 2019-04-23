using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetHospitalUnitsListQuery: GetListBase, IRequest<ListModel<HospitalUnitModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetHospitalUnitsListQuery, ListModel<HospitalUnitModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HospitalUnitModel>> Handle(GetHospitalUnitsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<HospitalUnitModel>
                {
                    Items = await Context.HospitalUnits
                                       .ProjectTo<HospitalUnitModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
