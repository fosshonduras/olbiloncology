using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyData;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class SearchHospitalUnitsQueryHandler : IRequestHandler<SearchHospitalUnitsQuery, HospitalUnitsListModel>
    {
        private readonly OncologyContext _context;
        private readonly IMapper _mapper;

        public SearchHospitalUnitsQueryHandler(OncologyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HospitalUnitsListModel> Handle(SearchHospitalUnitsQuery request, CancellationToken cancellationToken)
        {
            return new HospitalUnitsListModel
            {
                Items = await _context.HospitalUnits
                                   .Where(i =>
                                        EF.Functions.ILike(i.Name, $"%{request.SearchTerm}%")
                                        || EF.Functions.ILike(i.Code, $"%{request.SearchTerm}%")
                                    )
                                   .ProjectTo<HospitalUnitModel>(_mapper.ConfigurationProvider)
                                   .ToListAsync(cancellationToken)
            };
        }
    }
}
