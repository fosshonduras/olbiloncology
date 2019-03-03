using OLBIL.OncologyApplication.Models;
using System.Collections.Generic;

namespace OLBIL.OncologyApplication.OncologyPatients.Queries.GetOncologyPatientsList
{
    public class OncologyPatientsListModel
    {
        public IList<OncologyPatientModel> Items { get; set; }
    }
}
