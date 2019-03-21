using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands.UpdatePatient
{
    public class UpdateOncologyPatientCommand: IRequest
    {
        public OncologyPatientModel Model { get; set; }
    }
}
