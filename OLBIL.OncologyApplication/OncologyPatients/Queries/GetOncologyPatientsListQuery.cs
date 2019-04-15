using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries
{
    public class GetOncologyPatientsListQuery: IRequest<OncologyPatientsListModel>
    {
    }
}
