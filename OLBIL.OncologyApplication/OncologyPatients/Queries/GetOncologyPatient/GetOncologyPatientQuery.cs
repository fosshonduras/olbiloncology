using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatient
{
    public class GetOncologyPatientQuery: IRequest<OncologyPatientModel>
    {
        public int Id { get; set; }
    }
}
