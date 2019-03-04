using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands.AttemptCreation
{
    public class AttemptOncologyPatientCreationCommand: IRequest<OncologyPatientsListModel>
    {
        public OncologyPatientModel Model { get; set; }
    }
}
