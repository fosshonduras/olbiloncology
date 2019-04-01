using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Commands
{
    public class CreateHealthProfessionalCommand: IRequest<int>
    {
        public HealthProfessionalModel Model { get; set; }
    }
}
