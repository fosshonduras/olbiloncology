using FluentAssertions;
using OLBIL.Common;
using OLBIL.OncologyCrossCutting;
using System;
using TechTalk.SpecFlow;

namespace OLBIL.OncologyTests.UnitTests.DomainServices
{
    [Binding]
    public class AgeCalculationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public AgeCalculationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"a date time calculations provider")]
        public void GivenADateTimeCalculationsProvider()
        {
            var calculationsService = new DateTimeCalculationsDomainService();
            scenarioContext.Add(nameof(calculationsService), calculationsService);
        }
        
        [Given(@"a kid was born on ""(.*)""")]
        public void GivenAKidWasBornOn(DateTime pastDate)
        {
            scenarioContext.Add(nameof(pastDate), pastDate);
        }
        
        [When(@"their age is calculated on ""(.*)""")]
        public void WhenTheirAgeIsCalculatedOn(DateTime currentDate)
        {
            var pastDate = scenarioContext.Get<DateTime>("pastDate");
            var calculationsService = scenarioContext.Get<IDateTimeCalculationsDomainService>("calculationsService");
            var ageDescriptor = calculationsService.CalculateDifference(pastDate, currentDate);
            scenarioContext.Add(nameof(ageDescriptor), ageDescriptor);
        }
        
        [Then(@"the age should be (.*) years (.*) months (.*) days")]
        public void ThenTheAgeShouldBeYearsMonthsDays(int years, int months, int days)
        {
            var ageDescriptor = scenarioContext.Get<AgeDescriptor>("ageDescriptor");

            ageDescriptor.Years.Should().Be(years);
            ageDescriptor.Months.Should().Be(months);
            ageDescriptor.Days.Should().Be(days);
        }
    }
}
