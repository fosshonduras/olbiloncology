using MediatR;

namespace OLBIL.OncologyApplication.Buildings.Commands
{
    public class DeleteBuildingCommand : IRequest
    {
        public int Id { get; set; }
    }
}
