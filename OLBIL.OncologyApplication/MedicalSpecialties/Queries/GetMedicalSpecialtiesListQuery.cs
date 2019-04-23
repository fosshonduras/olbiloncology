using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.MedicalSpecialties.Queries
{
    public class GetMedicalSpecialtiesListQuery: GetListBase, IRequest<ListModel<MedicalSpecialtyModel>>
    {
        public class Handler : HandlerBase, IRequestHandler<GetMedicalSpecialtiesListQuery, ListModel<MedicalSpecialtyModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<MedicalSpecialtyModel>> Handle(GetMedicalSpecialtiesListQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<MedicalSpecialtyModel>
                {
                    Items = await Context.MedicalSpecialties
                                       .ProjectTo<MedicalSpecialtyModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
