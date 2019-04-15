using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public  class SearchBuildingsQuery : IRequest<BuildingsListModel>
    {
        public string SearchTerm { get; set; }
    }
}
