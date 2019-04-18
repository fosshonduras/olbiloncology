using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public  class SearchBuildingsQuery : IRequest<ListModel<BuildingModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchBuildingsQuery, ListModel<BuildingModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<BuildingModel>> Handle(SearchBuildingsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<BuildingModel>
                {
                    Items = await Context.Buildings
                                       .Where(i =>
                                            EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Code, $"%{request.SearchTerm}%")
                                        )
                                       .ProjectTo<BuildingModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
