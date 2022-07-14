namespace MedicineApp.Models
{
    public class Patient
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int HealthCardNumber { get; private set; }
        public int DoctorId { get; private set; }

        public Patient(int id, string name, int healthCardNumber, int doctorId)
        {
            Id = id;
            Name = name;
            HealthCardNumber = healthCardNumber;
            DoctorId = doctorId;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDoctorId(int doctorId)
        {
            DoctorId = doctorId;
        }
        public void UpdateHealthCardNumber(int healthCardNumber)
        {
            HealthCardNumber = healthCardNumber;
        }
    }
}
