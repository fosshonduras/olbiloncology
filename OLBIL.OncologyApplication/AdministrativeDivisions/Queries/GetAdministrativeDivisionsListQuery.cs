using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Queries
{
    public class GetAdministrativeDivisionsListQuery : IRequest<ListModel<AdministrativeDivisionModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetAdministrativeDivisionsListQuery, ListModel<AdministrativeDivisionModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AdministrativeDivisionModel>> Handle(GetAdministrativeDivisionsListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<AdministrativeDivisionModel>
                {
                    Items = await Context.AdministrativeDivisions
                                       .ProjectTo<AdministrativeDivisionModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
