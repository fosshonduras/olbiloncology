using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Queries
{
    public class SearchHealthProfessionalsQuery: IRequest<HealthProfessionalsListModel>
    {
        public string SearchTerm { get; set; }
    }
}
