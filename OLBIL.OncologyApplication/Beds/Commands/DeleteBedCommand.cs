using MediatR;

namespace OLBIL.OncologyApplication.Beds.Commands
{
    public class DeleteBedCommand: IRequest
    {
        public int Id { get; set; }
    }
}
