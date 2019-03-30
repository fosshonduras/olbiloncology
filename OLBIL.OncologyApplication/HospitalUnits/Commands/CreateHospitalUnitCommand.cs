using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class CreateHospitalUnitCommand : IRequest<int>
    {
        public HospitalUnitModel Model { get; set; }
    }
}
