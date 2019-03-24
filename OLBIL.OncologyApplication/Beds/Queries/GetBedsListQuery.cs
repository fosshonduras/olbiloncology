using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Beds.Queries
{
    public class GetBedsListQuery: IRequest<BedsListModel>
    {
    }
}
