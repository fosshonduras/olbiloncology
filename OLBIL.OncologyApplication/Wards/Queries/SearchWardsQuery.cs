using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class SearchWardsQuery : IRequest<ListModel<WardModel>>
    {
        public string SearchTerm { get; set; }
    }
}
