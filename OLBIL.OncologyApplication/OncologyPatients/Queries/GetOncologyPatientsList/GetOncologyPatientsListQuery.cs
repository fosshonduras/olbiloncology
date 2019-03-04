using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatientsList
{
    public class GetOncologyPatientsListQuery: IRequest<OncologyPatientsListModel>
    {
    }
}
