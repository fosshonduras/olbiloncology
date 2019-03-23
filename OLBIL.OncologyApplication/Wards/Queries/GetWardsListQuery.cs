using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Wards.Queries
{
    public class GetWardsListQuery: IRequest<WardsListModel>
    {
    }
}
