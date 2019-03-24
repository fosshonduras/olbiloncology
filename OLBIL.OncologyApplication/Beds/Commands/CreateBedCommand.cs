using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Beds.Commands
{
    public class CreateBedCommand: IRequest<int>
    {
        public BedModel Model { get; set; }
    }
}
