using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class GetBuildingQuery : IRequest<BuildingModel>
    {
        public int Id { get; set; }
    }
}
