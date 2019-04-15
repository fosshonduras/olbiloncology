using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands
{
    public class AttemptOncologyPatientCreationCommand: IRequest<OncologyPatientsListModel>
    {
        public OncologyPatientModel Model { get; set; }
    }
}
