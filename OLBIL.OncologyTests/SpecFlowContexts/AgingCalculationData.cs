using System;
using System.Collections.Generic;
using System.Text;

namespace OLBIL.OncologyTests.SpecFlowContexts
{
    public class AgingCalculationData
    {
        public DateTime PastDate { get; set; }
        public DateTime PresentDate { get; set; }
        public AgingCalculationData MyProperty { get; set; }
    }
}
