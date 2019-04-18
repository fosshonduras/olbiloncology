using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class SearchHospitalUnitsQuery: IRequest<ListModel<HospitalUnitModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchHospitalUnitsQuery, ListModel<HospitalUnitModel>>
        {
            public Handler(OncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HospitalUnitModel>> Handle(SearchHospitalUnitsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<HospitalUnitModel>
                {
                    Items = await Context.HospitalUnits
                                       .Where(i =>
                                            EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Code, $"%{request.SearchTerm}%")
                                        )
                                       .ProjectTo<HospitalUnitModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
