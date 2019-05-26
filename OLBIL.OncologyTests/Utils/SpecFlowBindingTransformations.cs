using System;
using TechTalk.SpecFlow;

namespace OLBIL.OncologyTests.Utils
{
    [Binding]
    public class SpecFlowBindingTransformations
    {
        [StepArgumentTransformation(@"(\d+)/(\d)+/(\d+)")]
        public static DateTime ListIntTransform(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }
    }
}
