using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetUnitQuery: IRequest<UnitModel>
    {
        public int Id { get; set; }
    }
}
