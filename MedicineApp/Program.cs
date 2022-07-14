using MedicineApp.Models;
using MedicineApp.Repositories;
using System.Data.SqlClient;

const string connectionString = @"Data Source=EVE_14_P416\SQLEXPRESS;Initial Catalog=Medicine;Pooling=true;Integrated Security=SSPI;TrustServerCertificate=True";

IHospitalRepository hospitalRepository = new SqlHospitalRepository(connectionString);
IDoctorRepository doctorRepository = new SqlDoctorRepository(connectionString);
IPatientRepository patientRepository = new SqlPatientRepository(connectionString);

PrintCommands();

while (true)
{
    Console.WriteLine("Введите команду:");
    string command = Console.ReadLine();
    try
    {
        if (command == "get-hospitals")
        {
            GetHospitals();
        }
        else if (command == "get-hospital-from-doctor")
        {
            GetHospitalFromDoctor();
        }
        else if (command == "get-doctors")
        {
            GetDoctors();
        }
        else if (command == "get-doctor-from-patient")
        {
            GetDoctorFromPatient();
        }
        else if (command == "get-patients")
        {
            GetPatients();
        }
        else if (command == "get-patients")
        {
            GetPatients();
        }
        else if (command == "update-hospital")
        {
            UpdateHospital();
        }
        else if (command == "update-doctor")
        {
            UpdateDoctor();
        }
        else if (command == "update-patient")
        {
            UpdatePatient();
        }
        else if (command == "delete-hospital")
        {
            DeleteHospital();
        }
        else if (command == "delete-doctor")
        {
            DeleteDoctor();
        }
        else if (command == "delete-patient")
        {
            DeletePatient();
        }
        else if (command == "get-doctors-by-count-patient")
        {
            GetDoctorsByCountPatients();
        }
        else if (command == "help")
        {
            PrintCommands();
        }
        else if (command == "exit")
        {
            break;
        }
        else
        {
            Console.WriteLine("Неправильно введенная команда");
        }
    }
    catch(ArgumentNullException ex)
    {

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void PrintCommands()
{
    Console.WriteLine("Доступные команды:");
    Console.WriteLine("get-hospitals(doctors, patients) - Получить список всех больниц(врачей, пациентов)");
    Console.WriteLine("get-doctor-from-patient - Получить лечащего врача пациента");
    Console.WriteLine("get-hospital-from-doctor - Получить больницу в которой работает врач");
    Console.WriteLine("delete-hospital(doctor, patient) - Удалить больницу(врача, пациента)");
    Console.WriteLine("update-hospital(doctor, patient) - Обновить больницу(врача, пациента)");
    Console.WriteLine("get-doctors-by-count-patient - Получить Id докторов и количество их пациентов");
    Console.WriteLine("help - Список команд");
    Console.WriteLine("exit - Выход");
}

void GetDoctorsByCountPatients()
{
    Console.WriteLine("Введите минимальное число пациентов меньше которого доктора выводится не будут");
    int count = GetCorrectInt();
    var doctorsList = patientRepository.GetDoctorsByCountPatients(count);
    foreach(var doctor in doctorsList)
    {
        Console.WriteLine($"DoctorId: {doctor.Item1} количество пациентов: {doctor.Item2}");
    }
}

void GetDoctors()
{
    IReadOnlyList<Doctor> doctors = doctorRepository.GetAll();
    if (doctors.Count == 0)
    {
        Console.WriteLine("Доктора не найдены!");
        throw new ArgumentNullException();
    }

    foreach (Doctor doctor in doctors)
    {
        Console.WriteLine($"Id: {doctor.Id}, Name: {doctor.Name}, TelephoneNumber: {doctor.TelephoneNumber}, HospitalId: {doctor.HospitalId}");
    }
}

void GetDoctorFromPatient()
{
    Console.WriteLine("Введите Id пациента:");
    int id = GetCorrectInt();
    Patient patient = patientRepository.GetById(id);
    if(patient == null)
    {
        Console.WriteLine("Пациент не найден");
        throw new ArgumentNullException(nameof(patient));
    }

    Doctor doctor = doctorRepository.GetFromPatient(patient);
    if (doctor == null)
    {
        Console.WriteLine("У пациента нет доктора");
        throw new ArgumentNullException(nameof(doctor));
    }

    Console.WriteLine($"Id: {doctor.Id}, Name: {doctor.Name}, TelephoneNumber: {doctor.TelephoneNumber}, HospitalId: {doctor.HospitalId}");
}

void GetPatients()
{
    IReadOnlyList<Patient> patients = patientRepository.GetAll();
    if (patients.Count == 0)
    {
        Console.WriteLine("Пациенты не найдены!");
        throw new ArgumentNullException();
    }

    foreach (Patient patient in patients)
    {
        Console.WriteLine($"Id: {patient.Id}, Name: {patient.Name}, HealthCardNumber: {patient.HealthCardNumber}, DoctorId: {patient.DoctorId}");
    }
}

void GetHospitals()
{
    IReadOnlyList<Hospital> hospitals = hospitalRepository.GetAll();
    if (hospitals.Count == 0)
    {
        Console.WriteLine("Больницы не найдены!");
        throw new ArgumentNullException();
    }

    foreach (Hospital hospital in hospitals)
    {
        Console.WriteLine($"Id: {hospital.Id}, Name: {hospital.Name}, Address: {hospital.Address}, TelephoneNumber: {hospital.TelephoneNumber}");
    }
}

void GetHospitalFromDoctor()
{
    Console.WriteLine("Введите Id доктора:");
    int id = GetCorrectInt();
    Doctor doctor = doctorRepository.GetById(id);
    if (doctor == null)
    {
        Console.WriteLine("Доктор не найден");
        throw new ArgumentNullException(nameof(doctor));
    }

    Hospital hospital = hospitalRepository.GetFromDoctor(doctor);
    if (hospital == null)
    {
        Console.WriteLine("У доктора нет больницы");
        throw new ArgumentNullException(nameof(hospital));
    }

    Console.WriteLine($"Id: {hospital.Id}, Name: {hospital.Name}, TelephoneNumber: {hospital.TelephoneNumber}, Address: {hospital.Address}");
}

void UpdateHospital()
{
    Console.WriteLine("Введите Id:");
    int id = GetCorrectInt();
    Hospital hospital = hospitalRepository.GetById(id);

    if (hospital == null)
    {
        Console.WriteLine("Больница не найдена");
        throw new ArgumentNullException();
    }

    Console.WriteLine("Введите новое название:");
    string newString = Console.ReadLine();
    hospital.UpdateName(newString);

    Console.WriteLine("Введите новый адрес:");
    newString = Console.ReadLine();
    hospital.UpdateAddress(newString);

    Console.WriteLine("Введите новый номер телефона:");
    newString = Console.ReadLine();
    hospital.UpdateTelephoneNumber(newString);

    hospitalRepository.Update(hospital);
    Console.WriteLine("Больница обновлена");
}

void UpdateDoctor()
{
    Console.WriteLine("Введите Id:");
    int id = GetCorrectInt();
    Doctor doctor = doctorRepository.GetById(id);

    if (doctor == null)
    {
        Console.WriteLine("Доктор не найден");
        throw new ArgumentNullException(nameof(doctor));
    }

    Console.WriteLine("Введите новое имя:");
    string newString = Console.ReadLine();
    doctor.UpdateName(newString);

    Console.WriteLine("Введите новый Id больницы:");
    doctor.UpdateHospitalId(GetCorrectInt());

    Console.WriteLine("Введите новый номер телефона:");
    newString = Console.ReadLine();
    doctor.UpdateTelephoneNumber(newString);

    doctorRepository.Update(doctor);
    Console.WriteLine("Доктор обновлен");
}

void UpdatePatient()
{
    Console.WriteLine("Введите Id:");
    int id = GetCorrectInt();
    Patient patient = patientRepository.GetById(id);

    if (patient == null)
    {
        Console.WriteLine("Пациент не найден");
        throw new ArgumentNullException(nameof(patient));
    }

    Console.WriteLine("Введите новое имя:");
    string newString = Console.ReadLine();
    patient.UpdateName(newString);

    Console.WriteLine("Введите новый номер медицинской карты:");
    patient.UpdateHealthCardNumber(GetCorrectInt());

    Console.WriteLine("Введите новый Id доктора:");
    patient.UpdateDoctorId(GetCorrectInt());

    patientRepository.Update(patient);
    Console.WriteLine("Пациент обновлен");
}

void DeleteHospital()
{
    Console.WriteLine("Введите ID:");
    int id = GetCorrectInt();
    Hospital hospital = hospitalRepository.GetById(id);

    if (hospital == null)
    {
        Console.WriteLine("Больница не найдена");
        throw new ArgumentNullException();
    }

    hospitalRepository.Delete(hospital);
    Console.WriteLine("Больница удалена");
}

void DeleteDoctor()
{
    Console.WriteLine("Введите ID:");
    int id = GetCorrectInt();
    Doctor doctor = doctorRepository.GetById(id);

    if (doctor == null)
    {
        Console.WriteLine("Доктор не найден");
        throw new ArgumentNullException();
    }

    doctorRepository.Delete(doctor);
    Console.WriteLine("Доктор удален");
}

void DeletePatient()
{
    Console.WriteLine("Введите ID:");
    int id = GetCorrectInt();
    Patient patient = patientRepository.GetById(id);

    if (patient == null)
    {
        Console.WriteLine("Пациент не найден");
        throw new ArgumentNullException();
    }

    patientRepository.Delete(patient);
    Console.WriteLine("Пациент удален");
}

int GetCorrectInt()
{
    int result;
    try 
    {
        result = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        Console.WriteLine("Введите число корректно:");
        result = GetCorrectInt();
    }
    return result;
}