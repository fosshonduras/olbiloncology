using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class GetHospitalUnitQuery: IRequest<HospitalUnitModel>
    {
        public int Id { get; set; }
    }
}
