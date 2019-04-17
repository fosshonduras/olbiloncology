using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class SearchBedsQuery : IRequest<ListModel<BedModel>>
    {
        public string SearchTerm { get; set; }
    }
}
