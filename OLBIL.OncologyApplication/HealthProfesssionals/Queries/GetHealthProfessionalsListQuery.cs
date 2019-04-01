using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Queries
{
    public class GetHealthProfessionalsListQuery: IRequest<HealthProfessionalsListModel>
    {
    }
}
