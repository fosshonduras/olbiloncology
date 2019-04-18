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

namespace OLBIL.OncologyApplication.HealthProfessionals.Queries
{
    public class SearchHealthProfessionalsQuery: IRequest<ListModel<HealthProfessionalModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchHealthProfessionalsQuery, ListModel<HealthProfessionalModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<HealthProfessionalModel>> Handle(SearchHealthProfessionalsQuery request, CancellationToken cancellationToken)
            {
                return new ListModel<HealthProfessionalModel>
                {
                    Items = await Context.HealthProfessionals
                                       .Where(i =>
                                            EF.Functions.ILike(i.Person.FirstName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.LastName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.MiddleName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.AdditionalLastName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.PreferredName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.GovernmentIDNumber, $"%{request.SearchTerm}%")
                                        )
                                       .ProjectTo<HealthProfessionalModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
