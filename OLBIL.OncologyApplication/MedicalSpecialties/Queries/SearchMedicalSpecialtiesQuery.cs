using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.MedicalSpecialties.Queries
{
    public class SearchMedicalSpecialtiesQuery : IRequest<ListModel<MedicalSpecialtyModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchMedicalSpecialtiesQuery, ListModel<MedicalSpecialtyModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<MedicalSpecialtyModel>> Handle(SearchMedicalSpecialtiesQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<MedicalSpecialtyModel>
                {
                    Items = await ApplyFilter(request, cancellationToken)
                };
            }

            private async Task<List<MedicalSpecialtyModel>> ApplyFilter(SearchMedicalSpecialtiesQuery request, CancellationToken cancellationToken)
            {
                return await Context.MedicalSpecialties
                                    .Where(i =>
                                        EF.Functions.ILike(i.Description, $"%{request.SearchTerm}%")
                                    )
                                    .ProjectTo<MedicalSpecialtyModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
            }
        }
    }
}
