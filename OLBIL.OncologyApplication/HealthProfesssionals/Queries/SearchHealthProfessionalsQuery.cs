using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Queries
{
    public class SearchHealthProfessionalsQuery: IRequest<ListModel<HealthProfessionalModel>>
    {
        public string SearchTerm { get; set; }
    }
}
