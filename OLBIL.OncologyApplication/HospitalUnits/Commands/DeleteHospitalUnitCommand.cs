using MediatR;

namespace OLBIL.OncologyApplication.HospitalUnits.Commands
{
    public class DeleteHospitalUnitCommand: IRequest
    {
        public int Id { get; set; }
    }
}
