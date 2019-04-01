using MediatR;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Commands
{
    public class DeleteHealthProfessionalCommand: IRequest
    {
        public int Id { get; set; }
    }
}
