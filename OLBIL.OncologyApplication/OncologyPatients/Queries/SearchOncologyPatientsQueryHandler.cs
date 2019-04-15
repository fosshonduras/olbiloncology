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
    public class SearchOncologyPatientsListQueryHandler : IRequestHandler<SearchOncologyPatientsQuery, OncologyPatientsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public SearchOncologyPatientsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OncologyPatientsListModel> Handle(SearchOncologyPatientsQuery request, CancellationToken cancellationToken)
        {
            return new OncologyPatientsListModel
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
