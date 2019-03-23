using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Buildings.Queries
{
    public class GetBuildingsListQuery: IRequest<BuildingsListModel>
    {
    }
}
