using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OLBIL.OncologyTests.UnitTests.Appointments
{
    [TestClass]
    public class AppointmentsTests
    {
        [TestMethod]
        [TestCategory("AppointmentCreation")]
        [Ignore("Needs to be implemented once the appointment feature is complete")]
        public void Should_not_allow_a_new_appointment_for_one_patient_on_the_same_date_for_the_same_reason()
        {
            
        }

        [TestMethod]
        [Ignore("Needs to be implemented once the appointment feature is complete")]
        public void Should_not_allow_making_appointments_on_a_known_holiday()
        {

        }

        [TestMethod]
        [Ignore("Needs to be implemented once the appointment feature is complete")]
        public void Should_not_allow_making_appointments_with_a_doctor_without_available_attention_blocks()
        {

        }

        [TestMethod]
        [Ignore("Needs to be implemented once the appointment feature is complete")]
        public void Should_not_allow_making_appointments_on_a_day_the_doctor_is_on_vacations()
        {

        }
    }
}
