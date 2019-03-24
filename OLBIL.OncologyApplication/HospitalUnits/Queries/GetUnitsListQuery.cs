using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetUnitsListQuery: IRequest<UnitsListModel>
    {
    }
}
