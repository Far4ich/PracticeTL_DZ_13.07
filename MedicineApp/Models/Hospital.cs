namespace MedicineApp.Models
{
    public class Hospital
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string TelephoneNumber { get; private set; }

        public Hospital(int id, string name, string address, string telephoneNumber)
        {
            Id = id;
            Name = name;
            Address = address;
            TelephoneNumber = telephoneNumber;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateAddress(string address)
        {
            Address = address;
        }
        public void UpdateTelephoneNumber(string telephoneNumber)
        {
            TelephoneNumber = telephoneNumber;
        }
    }
}
