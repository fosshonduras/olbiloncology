using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class SearchOncologyPatientsQuery : IRequest<ListModel<OncologyPatientModel>>
    {
        public string SearchTerm { get; set; }

        public class SearchOncologyPatientsListQueryHandler : IRequestHandler<SearchOncologyPatientsQuery, ListModel<OncologyPatientModel>>
        {
            private readonly OncologyContext _context;
            private readonly IMapper _mapper;

            public SearchOncologyPatientsListQueryHandler(OncologyContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListModel<OncologyPatientModel>> Handle(SearchOncologyPatientsQuery request, CancellationToken cancellationToken) => new ListModel<OncologyPatientModel>
            {
                Items = await _context.OncologyPatients
                                       .Where(i =>
                                            EF.Functions.ILike(i.Person.FirstName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.LastName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.MiddleName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.AdditionalLastName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.PreferredName, $"%{request.SearchTerm}%")
                                            || EF.Functions.ILike(i.Person.GovernmentIDNumber, $"%{request.SearchTerm}%")
                                        )
                                       .ProjectTo<OncologyPatientModel>(_mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken)
            };
        }
    }
}
