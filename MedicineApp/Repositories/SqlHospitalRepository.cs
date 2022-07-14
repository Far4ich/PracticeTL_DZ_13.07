using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using MedicineApp.Models;


namespace MedicineApp.Repositories
{
    public class SqlHospitalRepository : IHospitalRepository
    {
        private readonly string _connectionString;

        public SqlHospitalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Hospital> GetAll()
        {
            var result = new List<Hospital>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Address], [TelephoneNumber] from [Hospital]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Hospital(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Name"]),
                    Convert.ToString(reader["Address"]),
                    Convert.ToString(reader["TelephoneNumber"])
                ));
            }

            return result;
        }

        public Hospital GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Address], [TelephoneNumber] from [Hospital] where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Hospital(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Name"]),
                    Convert.ToString(reader["Address"]),
                    Convert.ToString(reader["TelephoneNumber"])
                );
            }
            else
            {
                return null;
            }
        }

        public void Update(Hospital hospital)
        {
            if (hospital == null)
            {
                throw new ArgumentNullException(nameof(hospital));
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Hospital] set [Name] = @name, [Address] = @address, [TelephoneNumber] = @telephoneNumber where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = hospital.Id;
            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = hospital.Name;
            sqlCommand.Parameters.Add("@address", SqlDbType.NVarChar, 245).Value = hospital.Address;
            sqlCommand.Parameters.Add("@telephoneNumber", SqlDbType.NVarChar, 30).Value = hospital.TelephoneNumber;
            sqlCommand.ExecuteNonQuery();
        }
        public void Delete(Hospital hospital)
        {
            if (hospital == null)
            {
                throw new ArgumentNullException(nameof(hospital));
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [Hospital] where [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = hospital.Id;
            sqlCommand.ExecuteNonQuery();
        }

        public Hospital GetFromDoctor(Doctor doctor)
        {
            return GetById(doctor.HospitalId);
        }
    }
}
