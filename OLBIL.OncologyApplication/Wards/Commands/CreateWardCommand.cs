using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.Wards.Commands
{
    public class CreateWardCommand : IRequest<int>
    {
        public WardModel Model { get; set; }
    }
}
