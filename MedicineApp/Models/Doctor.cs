namespace MedicineApp.Models
{
    public class Doctor
    { 
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string TelephoneNumber { get; private set; }
        public int HospitalId { get; private set; }

        public Doctor(int id, string name, string telephoneNumber, int hospitalId)
        {
            Id = id;
            Name = name;
            TelephoneNumber = telephoneNumber;
            HospitalId = hospitalId;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateHospitalId(int hospitalId)
        {
            HospitalId = hospitalId;
        }
        public void UpdateTelephoneNumber(string telephoneNumber)
        {
            TelephoneNumber = telephoneNumber;
        }
    }
}
