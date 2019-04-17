using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Commands
{
    public class AttemptOncologyPatientCreationCommand: IRequest<ListModel<OncologyPatientModel>>
    {
        public OncologyPatientModel Model { get; set; }
    }
}
