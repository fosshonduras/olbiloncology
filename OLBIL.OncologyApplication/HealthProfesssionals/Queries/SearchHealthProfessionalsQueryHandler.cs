using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Queries
{
    public class SearchHealthProfessionalsListQueryHandler : IRequestHandler<SearchHealthProfessionalsQuery, HealthProfessionalsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public SearchHealthProfessionalsListQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HealthProfessionalsListModel> Handle(SearchHealthProfessionalsQuery request, CancellationToken cancellationToken)
        {
            return new HealthProfessionalsListModel
            {
                Items = await _context.HealthProfessionals
                                   .Where(i =>
                                        EF.Functions.ILike(i.Person.FirstName, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Person.LastName, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Person.MiddleName, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Person.AdditionalLastName, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Person.PreferredName, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Person.GovernmentIDNumber, $"%{request.SearchTerm}%")
                                    )
                                   .ProjectTo<HealthProfessionalModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}
