using MedicineApp.Models;

namespace MedicineApp.Repositories
{
    public interface IHospitalRepository
    {
        IReadOnlyList<Hospital> GetAll();
        Hospital GetById(int id);
        Hospital GetFromDoctor(Doctor doctor);
        void Update(Hospital hospital);
        void Delete(Hospital hospital);
    }
}
