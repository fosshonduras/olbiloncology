using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class CreateUnitCommand : IRequest<int>
    {
        public UnitModel Model { get; set; }
    }
}
