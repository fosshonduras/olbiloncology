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

namespace OLBIL.OncologyApplication.AdministrativeDivisions.Queries
{
    public class GetAdministrativeDivisionQuery : IRequest<AdministrativeDivisionModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetAdministrativeDivisionQuery, AdministrativeDivisionModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<AdministrativeDivisionModel> Handle(GetAdministrativeDivisionQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<AdministrativeDivisionModel>(await Context
                    .AdministrativeDivisions.Where(o => o.AdministrativeDivisionId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(AdministrativeDivision), nameof(item.AdministrativeDivisionId), request.Id);
                }

                return item;
            }
        }
    }
}
