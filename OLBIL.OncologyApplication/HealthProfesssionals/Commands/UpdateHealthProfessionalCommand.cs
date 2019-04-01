using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Commands
{
    public class UpdateHealthProfessionalCommand: IRequest
    {
        public HealthProfessionalModel Model { get; set; }
    }
}
