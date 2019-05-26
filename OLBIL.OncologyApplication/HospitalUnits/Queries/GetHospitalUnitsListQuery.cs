using AutoMapper;
using MediatR;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetHospitalUnitsListQuery: GetListBase, IRequest<ListModel<HospitalUnitModel>>
    {
        public class Handler : GetListHandlerBase, IRequestHandler<GetHospitalUnitsListQuery, ListModel<HospitalUnitModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HospitalUnitModel>> Handle(GetHospitalUnitsListQuery request, CancellationToken cancellationToken)
            {
                var defaultSort = BuildSortList<HospitalUnit>(i => i.HospitalUnitId);

                return await RetrieveListResults<HospitalUnit, HospitalUnitModel>(null, defaultSort, request, cancellationToken);
            }
        }
    }
}
