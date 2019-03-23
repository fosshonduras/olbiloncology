using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class GetWardQuery : IRequest<WardModel>
    {
        public int Id { get; set; }
    }
}
