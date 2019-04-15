using MediatR;
using OLBIL.OncologyApplication.Models;

namespace OLBIL.OncologyApplication.HospitalUnits.Queries
{
    public class SearchHospitalUnitsQuery: IRequest<HospitalUnitsListModel>
    {
        public string SearchTerm { get; set; }
    }
}
