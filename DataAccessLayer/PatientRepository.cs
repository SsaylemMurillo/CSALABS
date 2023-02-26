using Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PatientRepository
    {
        public DbConnection _connection { get; set; }

        public PatientRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public Patient DeletePatient(Patient patient)
        {
            if (Search(patient.Id) != null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"delete from patient where patient.id = @patientId;";
                command.Parameters.Add(new SqlParameter("@patientId", patient.Id));
                var value = command.ExecuteNonQuery();
                if (value == 1)
                    return patient;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public Patient Save(Patient patient)
        {
            Patient patientUnSaved = null;

            if (Search(patient.Id) == null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"insert into patient (id, id_type, first_name, second_name, last_name, " +
                    $"second_lastname, born_date, expedition_date, expedition_place, phone, address, nacionality) " +
                    "values (@patientId, @patientIdType, @patientFirstName, @patientSecondName, @patientLastName, @patientSecondLastName, @patientBornDate, "
                    + $"@patientExpeditionDate, @patientExpeditionPlace, @patientPhone, @patientAddress, @patientNationality);";
                command.Parameters.Add(new SqlParameter("@patientId", patient.Id));
                command.Parameters.Add(new SqlParameter("@patientIdType", patient.IdType));
                command.Parameters.Add(new SqlParameter("@patientFirstName", patient.FirstName));
                command.Parameters.Add(new SqlParameter("@patientSecondName", patient.SecondName));
                command.Parameters.Add(new SqlParameter("@patientLastName", patient.LastName));
                command.Parameters.Add(new SqlParameter("@patientSecondLastName", patient.SecondLastName));
                var date = "" + patient.BornDate.Month + "/" + patient.BornDate.Day + "/" + patient.BornDate.Year;
                command.Parameters.Add(new SqlParameter("@patientBornDate", date));
                var date2 = "" + patient.ExpeditionDate.Month + "/" + patient.ExpeditionDate.Day + "/" + patient.ExpeditionDate.Year;
                command.Parameters.Add(new SqlParameter("@patientExpeditionDate", date2));
                command.Parameters.Add(new SqlParameter("@patientExpeditionPlace", patient.ExpeditionPlace));
                command.Parameters.Add(new SqlParameter("@patientPhone", patient.Phone));
                command.Parameters.Add(new SqlParameter("@patientAddress", patient.Address));
                command.Parameters.Add(new SqlParameter("@patientNationality", patient.Nacionality));

                command.ExecuteNonQuery();
            }
            else
            {
                patientUnSaved = patient;
            }
            return patientUnSaved;
        }

        public void UpdatePatient(Patient patient)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = $"update patient set id_type = @patientIdType, first_name = @patientFirstName," +
                $" second_name = @patientSecondName, last_name = @patientLastName, second_lastname = @patientSecondLastName, " +
                $"born_date = @patientBornDate, expedition_date = @patientExpeditionDate, expedition_place = @patientExpeditionPlace, phone = @patientPhone, " +
                $"address = @patientAddress, nacionality = @patientNationality WHERE id = @id";
            command.Parameters.Add(new SqlParameter("@id", patient.Id));
            command.Parameters.Add(new SqlParameter("@patientIdType", patient.IdType));
            command.Parameters.Add(new SqlParameter("@patientFirstName", patient.FirstName));
            command.Parameters.Add(new SqlParameter("@patientSecondName", patient.SecondName));
            command.Parameters.Add(new SqlParameter("@patientLastName", patient.LastName));
            command.Parameters.Add(new SqlParameter("@patientSecondLastName", patient.SecondLastName));
            var date = "" + patient.BornDate.Month + "/" + patient.BornDate.Day + "/" + patient.BornDate.Year;
            command.Parameters.Add(new SqlParameter("@patientBornDate", date));
            var date2 = "" + patient.ExpeditionDate.Month + "/" + patient.ExpeditionDate.Day + "/" + patient.ExpeditionDate.Year;
            command.Parameters.Add(new SqlParameter("@patientExpeditionDate", date2));
            command.Parameters.Add(new SqlParameter("@patientExpeditionPlace", patient.ExpeditionPlace));
            command.Parameters.Add(new SqlParameter("@patientPhone", patient.Phone));
            command.Parameters.Add(new SqlParameter("@patientAddress", patient.Address));
            command.Parameters.Add(new SqlParameter("@patientNationality", patient.Nacionality));
            command.ExecuteNonQuery();
        }

        public Patient Search(int id)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from patient where id = @id;";
            command.Parameters.Add(new SqlParameter("@id", id));
            var reader = command.ExecuteReader();

            Patient patient = null;

            while (reader.Read())
            {
                int patientId = reader.GetInt32(0);
                string patientIdType = reader.GetString(1);
                string patientFirstName = reader.GetString(2);
                string patientSecondName = reader.GetString(3);
                string patientLastName = reader.GetString(4);
                string patientSecondLastName = reader.GetString(5);
                DateTime patientBornDate = Convert.ToDateTime(reader.GetDateTime(6));
                var patientBornDateTime = "" + patientBornDate.Month + "/" + patientBornDate.Day + "/" + patientBornDate.Year;
                DateTime patientExpeditionDate = Convert.ToDateTime(reader.GetDateTime(7));
                var patientExpeditionDateTime = "" + patientExpeditionDate.Month + "/" + patientExpeditionDate.Day + "/" + patientExpeditionDate.Year;
                string patientExpeditionPlace = reader.GetString(8);
                int patientPhone = reader.GetInt32(9);
                string patientAddress = reader.GetString(10);
                string nacionality = reader.GetString(11);

                patient = new Patient(patientId, patientIdType, patientFirstName, patientSecondName, patientLastName, patientSecondLastName, 
                    patientBornDateTime, patientExpeditionDateTime, patientExpeditionPlace, patientPhone, patientAddress, nacionality);
            }

            reader.Close();
            return patient;
        }

        public List<Patient> GetAllPatients()
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from patient;";
            var reader = command.ExecuteReader();

            List<Patient> patients = new List<Patient>();

            while (reader.Read())
            {
                int patientId = reader.GetInt32(0);
                string patientIdType = reader.GetString(1);
                string patientFirstName = reader.GetString(2);
                string patientSecondName = reader.GetString(3);
                string patientLastName = reader.GetString(4);
                string patientSecondLastName = reader.GetString(5);
                DateTime patientBornDate = Convert.ToDateTime(reader.GetDateTime(6));
                string patientBornDateTime = "" + patientBornDate.Month + "/" + patientBornDate.Day + "/" + patientBornDate.Year;
                DateTime patientExpeditionDate = Convert.ToDateTime(reader.GetDateTime(7));
                string patientExpeditionDateTime = "" + patientExpeditionDate.Month + "/" + patientExpeditionDate.Day + "/" + patientExpeditionDate.Year;
                string patientExpeditionPlace = reader.GetString(8);
                int patientPhone = reader.GetInt32(9);
                string patientAddress = reader.GetString(10);
                string nacionality = reader.GetString(11);

                Patient patient = new Patient(patientId, patientIdType, patientFirstName, patientSecondName, patientLastName, patientSecondLastName,
                    patientBornDateTime, patientExpeditionDateTime, patientExpeditionPlace, patientPhone, patientAddress, nacionality);

                patients.Add(patient);
            }

            reader.Close();
            return patients;
        }
    }
}
