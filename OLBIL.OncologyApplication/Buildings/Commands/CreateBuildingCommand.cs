using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Buildings.Commands
{
    public class CreateBuildingCommand: IRequest<int>
    {
        public BuildingModel Model { get; set; }
    }
}
