using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public  class SearchBuildingsQuery : IRequest<ListModel<BuildingModel>>
    {
        public string SearchTerm { get; set; }
    }
}
