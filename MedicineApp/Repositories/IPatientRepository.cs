using MedicineApp.Models;

namespace MedicineApp.Repositories
{
    public interface IPatientRepository
    {
        IReadOnlyList<Patient> GetAll();
        Patient GetById(int id);
        void Update(Patient patient);
        void Delete(Patient patient);
        List<Tuple<int, int>> GetDoctorsByCountPatients(int minCount);
    }
}
