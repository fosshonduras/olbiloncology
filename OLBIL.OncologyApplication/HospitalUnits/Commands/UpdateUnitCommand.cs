using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class UpdateUnitCommand: IRequest
    {
        public UnitModel Model { get; set; }
    }
}
