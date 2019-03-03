using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands.CreateOncologyPatient
{
    public class CreateOncologyPatientCommand: IRequest<int>
    {
        public OncologyPatientModel Model { get; set; }
    }
}
