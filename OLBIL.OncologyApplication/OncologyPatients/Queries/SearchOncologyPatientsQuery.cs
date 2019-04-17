using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class SearchOncologyPatientsQuery : IRequest<ListModel<OncologyPatientModel>>
    {
        public string SearchTerm { get; set; }
    }
}
