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

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class SearchOncologyPatientsQuery : IRequest<ListModel<OncologyPatientModel>>
    {
        public string SearchTerm { get; set; }

        public class Handler : HandlerBase, IRequestHandler<SearchOncologyPatientsQuery, ListModel<OncologyPatientModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<OncologyPatientModel>> Handle(SearchOncologyPatientsQuery request, CancellationToken cancellationToken) => new ListModel<OncologyPatientModel>
            {
                Items = await Context.OncologyPatients
                                       .Where(i =>
                                            EF.Functions.ILike(i.Person.FirstName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.LastName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.MiddleName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.AdditionalLastName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.PreferredName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.GovernmentIDNumber, $"%{request.SearchTerm}%")
                                        )
                                       .ProjectTo<OncologyPatientModel>(Mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
            };
        }
    }
}
