using MediatR;

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class DeleteWardCommand: IRequest
    {
        public int Id { get; set; }
    }
}
