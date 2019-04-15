using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class SearchOncologyPatientsQuery : IRequest<OncologyPatientsListModel>
    {
        public string SearchTerm { get; set; }
    }
}
