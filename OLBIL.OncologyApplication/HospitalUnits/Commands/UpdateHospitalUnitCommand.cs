using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class UpdateHospitalUnitCommand: IRequest
    {
        public HospitalUnitModel Model { get; set; }
    }
}
