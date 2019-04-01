using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HealthProfesssionals.Queries
{
    public class GetHealthProfessionalQuery: IRequest<HealthProfessionalModel>
    {
        public int Id { get; set; }
    }
}
