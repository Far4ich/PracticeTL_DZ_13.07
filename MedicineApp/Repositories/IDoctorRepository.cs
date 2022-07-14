using MedicineApp.Models;

namespace MedicineApp.Repositories
{
    public interface IDoctorRepository
    {
        IReadOnlyList<Doctor> GetAll();
        Doctor GetById(int id);
        Doctor GetFromPatient(Patient patient);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
    }
}
