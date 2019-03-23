using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class UpdateWardCommand: IRequest
    {
        public WardModel Model { get; set; }
    }
}
