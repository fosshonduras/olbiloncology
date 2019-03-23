using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Buildings.Commands
{
    public class UpdateBuildingCommand : IRequest
    {
        public BuildingModel Model { get; set; }
    }
}
