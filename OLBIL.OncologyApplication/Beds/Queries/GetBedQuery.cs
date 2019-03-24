using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class GetBedQuery: IRequest<BedModel>
    {
        public int Id { get; set; }
    }
}
