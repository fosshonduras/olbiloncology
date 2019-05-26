using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OLBIL.Common;
using OLBIL.OncologyCrossCutting;
using System;

namespace OLBIL.OncologyTests.UnitTests.DomainServices
{
    [TestClass]
    [TestCategory("Age Calculation MsTest")]
    public class AgeCalculationMsTests
    {
        private readonly IDateTimeCalculationsDomainService _calculationsService;

        public AgeCalculationMsTests()
        {
            _calculationsService = new DateTimeCalculationsDomainService();
        }

        [Ignore]
        [TestMethod]
        public void CalculateDifference_should_work_for_cases_where_dates_are_on_the_very_same_month()
        {
            // Arrange
            var pastDate = new DateTime(2016, 1, 10);
            var currentDate = new DateTime(2016, 1, 20);
            var expectedAge = new AgeDescriptor(0, 0, 10);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore] 
        [TestMethod]
        public void CalculateDifference_should_count_days_if_a_month_hasnt_passed_after_a_31day_month()
        {
            // Arrange
            var pastDate = new DateTime(2016, 1, 20);
            var currentDate = new DateTime(2016, 2, 1);
            var expectedAge = new AgeDescriptor(0, 0, 12);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore] 
        [TestMethod]
        public void CalculateDifference_should_count_days_if_a_month_hasnt_passed_after_a_30day_month()
        {
            // Arrange
            var pastDate = new DateTime(2016, 11, 20);
            var currentDate = new DateTime(2016, 12, 1);
            var expectedAge = new AgeDescriptor(0, 0, 11);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore] 
        [TestMethod]
        public void CalculateDifference_should_output_one_month_if_date_is_the_same_on_the_next_very_month()
        {
            // Arrange
            var pastDate = new DateTime(2016, 1, 10);
            var currentDate = new DateTime(2016, 2, 10);
            var expectedAge = new AgeDescriptor(0, 1, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore] 
        [TestMethod]
        public void CalculateDifference_should_output_two_months_if_date_is_the_same_two_months_later_in_same_year()
        {
            // Arrange
            var pastDate = new DateTime(2016, 3, 5);
            var currentDate = new DateTime(2016, 5, 5);
            var expectedAge = new AgeDescriptor(0, 2, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore] 
        [TestMethod]
        public void CalculateDifference_should_output_one_month_if_date_is_the_same_on_the_next_very_month_across_years()
        {
            // Arrange
            var pastDate = new DateTime(2015, 12, 10);
            var currentDate = new DateTime(2016, 1, 10);
            var expectedAge = new AgeDescriptor(0, 1, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore] 
        [TestMethod]
        public void CalculateDifference_should_output_one_month_if_date_is_the_same_on_the_next_very_month_for_february_29th()
        {
            // Arrange
            var pastDate = new DateTime(2016, 2, 29);
            var currentDate = new DateTime(2016, 3, 29);
            var expectedAge = new AgeDescriptor(0, 1, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore]
        [TestMethod]
        public void CalculateDifference_should_output_incomplete_years_on_feb_28th_for_people_born_on_feb_29th()
        {
            // Arrange
            var pastDate = new DateTime(2016, 2, 29);
            var currentDate = new DateTime(2019, 2, 28);
            var expectedAge = new AgeDescriptor(2, 11, 28);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore]
        [TestMethod]
        public void CalculateDifference_people_born_on_february_29th_have_birthdays_the_day_after_feb_28th()
        {
            // Arrange
            var pastDate = new DateTime(2016, 2, 29);
            var currentDate = new DateTime(2019, 3, 1);
            var expectedAge = new AgeDescriptor(3, 0, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore]
        [TestMethod]
        public void CalculateDifference_people_born_on_february_29th_have_birthdays_on_feb_29th_on_leap_years()
        {
            // Arrange
            var pastDate = new DateTime(2016, 2, 29);
            var currentDate = new DateTime(2020, 2, 29);
            var expectedAge = new AgeDescriptor(4, 0, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore]
        [TestMethod]
        public void CalculateDifference_from_the_first_day_of_a_month_to_the_first_day_of_the_next_theres_one_month()
        {
            // Arrange
            var pastDate = new DateTime(2016, 2, 1);
            var currentDate = new DateTime(2016, 3, 1);
            var expectedAge = new AgeDescriptor(0, 1, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }

        [Ignore]
        [TestMethod]
        public void CalculateDifference_from_the_first_day_of_a_month_to_the_first_day_of_the_next_theres_on_month_on_leap_years()
        {
            // Arrange
            var pastDate = new DateTime(2017, 2, 1);
            var currentDate = new DateTime(2017, 3, 1);
            var expectedAge = new AgeDescriptor(0, 1, 0);

            // Act
            var actualAge = _calculationsService.CalculateDifference(pastDate, currentDate);

            // Assert
            actualAge.Should().Be(expectedAge);
        }
    }
}
