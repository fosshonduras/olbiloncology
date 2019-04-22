using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Exceptions;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class GetWardQuery : IRequest<WardModel>
    {
        public int Id { get; set; }

        public class Handler : HandlerBase, IRequestHandler<GetWardQuery, WardModel>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<WardModel> Handle(GetWardQuery request, CancellationToken cancellationToken)
            {
                var item = Mapper.Map<WardModel>(await Context
                    .Wards.Where(o => o.WardId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken));

                if (item == null)
                {
                    throw new NotFoundException(nameof(Ward), nameof(item.WardId), request.Id);
                }

                return item;
            }
        }
    }
}