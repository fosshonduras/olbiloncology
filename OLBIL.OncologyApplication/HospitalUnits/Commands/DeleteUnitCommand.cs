using MediatR;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class DeleteUnitCommand: IRequest
    {
        public int Id { get; set; }
    }
}
