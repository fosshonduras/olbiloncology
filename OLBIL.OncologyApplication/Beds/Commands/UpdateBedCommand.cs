using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Beds.Commands
{
    public class UpdateBedCommand: IRequest
    {
        public BedModel Model { get; set; }
    }
}
