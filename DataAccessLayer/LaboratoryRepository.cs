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
    public class LaboratoryRepository : ICRUD<Laboratory>
    {
        public DbConnection _connection { get; set; }

        public LaboratoryRepository(DbConnection connection)
        {
            _connection = connection;
        }


        public Laboratory Delete(Laboratory laboratory)
        {
            if (Search(laboratory) != null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"delete from laboratory where laboratory.id_lab = @labId;";
                command.Parameters.Add(new SqlParameter("@labId", laboratory.Id));
                var value = command.ExecuteNonQuery();
                if (value == 1)
                    return laboratory;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        public List<Laboratory> SearchAllLabsFromAPatient(Patient patient)
        {
            if (patient != null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"select * from laboratory where laboratory.id_patient = @patientId;";
                command.Parameters.Add(new SqlParameter("@patientId", patient.Id));
                var reader = command.ExecuteReader();

                List<Laboratory> labs = new List<Laboratory>();

                while (reader.Read())
                {
                    int labId = reader.GetInt32(0);
                    int labPatientId = reader.GetInt32(1);
                    string labResult = reader.GetString(2);
                    DateTime labDateTime = reader.GetDateTime(3);
                    var labDate = "" + labDateTime.Month + "/" + labDateTime.Day + "/" + labDateTime.Year;
                    string labPlace = reader.GetString(4);

                    labs.Add(new Laboratory(labId, labPatientId, labResult, labDate, labPlace));
                }

                reader.Close();
                return labs;
            }
            else
            {
                return null;
            }
        }

        public Patient DeleteAllLabsFromAPatient(Patient patient)
        {
            if (patient != null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"delete from laboratory where laboratory.id_patient = @patientId;";
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

        public List<Laboratory> GetAll()
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from laboratory;";
            var reader = command.ExecuteReader();

            List<Laboratory> labs = new List<Laboratory>();

            while (reader.Read())
            {
                int labId = reader.GetInt32(0);
                int labPatientId = reader.GetInt32(1);
                string labResult = reader.GetString(2);
                DateTime labDateTime = reader.GetDateTime(3);
                var labDate = "" + labDateTime.Month + "/" + labDateTime.Day + "/" + labDateTime.Year;
                string labPlace = reader.GetString(4);

                labs.Add(new Laboratory(labId, labPatientId, labResult, labDate, labPlace));
            }

            reader.Close();
            return labs;
        }

        public string Save(Laboratory laboratory)
        {
            string message;

            if (Search(laboratory) == null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"insert into laboratory (id_patient, result, date_lab, place) " +
                    $"values (@labPatientId, @labResult, @labDate, @labPlace);";
                command.Parameters.Add(new SqlParameter("@labPatientId", laboratory.Patient.Id));
                command.Parameters.Add(new SqlParameter("@labResult", ""));
                var date = "" + laboratory.LabDate.Month + "/" + laboratory.LabDate.Day + "/" + laboratory.LabDate.Year;
                command.Parameters.Add(new SqlParameter("@labDate", date));
                command.Parameters.Add(new SqlParameter("@labPlace", laboratory.Place));
                command.ExecuteNonQuery();
                message = "Laboratorio Correctamente Añadido";
            }
            else
            {
                message = "El laboratorio no se pudo crear";
            }
            return message;
        }

        public Laboratory GetLastLaboratoryCreated()
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"SELECT IDENT_CURRENT('laboratory');";
            var reader = command.ExecuteReader();

            Laboratory lastLab = null;

            while (reader.Read())
            {
                Decimal dec1 = reader.GetDecimal(0);
                int labId = decimal.ToInt32(dec1);
                lastLab = new Laboratory(labId);
            }

            reader.Close();
            return lastLab;
        }

        public Laboratory Search(Laboratory laboratory)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from laboratory where id_lab = @id;";
            command.Parameters.Add(new SqlParameter("@id", laboratory.Id));
            var reader = command.ExecuteReader();

            Laboratory searchedLab = null;

            while (reader.Read())
            {
                int labId = reader.GetInt32(0);
                int labPatientId = reader.GetInt32(1);
                string labResult = reader.GetString(2);
                DateTime labDateTime = reader.GetDateTime(3);
                var labDate = "" + labDateTime.Month + "/" + labDateTime.Day + "/" + labDateTime.Year;
                string labPlace = reader.GetString(4);

                searchedLab = new Laboratory(labId, labPatientId, labResult, labDate, labPlace);
            }

            reader.Close();
            return searchedLab;
        }

        public string Update(Laboratory laboratory)
        {
            string message;


            if (Search(laboratory) != null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;

                command.CommandText = $"update laboratory set result = @labResult, date_lab = @labDate, place = @labPlace " +
                    "WHERE id_lab = @labId";
                command.Parameters.Add(new SqlParameter("@labId", laboratory.Id));
                command.Parameters.Add(new SqlParameter("@labResult", laboratory.Result));
                var date = "" + laboratory.LabDate.Month + "/" + laboratory.LabDate.Day + "/" + laboratory.LabDate.Year;
                command.Parameters.Add(new SqlParameter("@labDate", date));
                command.Parameters.Add(new SqlParameter("@labPlace", laboratory.Place));
                command.ExecuteNonQuery();
                message = "Laboratorio Actualizado Correctamente";
            }
            else
            {
                message = "No se pudo actualizar el laboratorio";
            }
            return message;
        }

        public List<Laboratory> GetAllLabsFromPatient(int id)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select id_lab, result, date_lab, place from laboratory where id_patient = @id";
            command.Parameters.Add(new SqlParameter("@id", id));
            var reader = command.ExecuteReader();

            var labs = new List<Laboratory>();

            while (reader.Read())
            {
                int labId = reader.GetInt32(0);
                string labResult = reader.GetString(1);
                DateTime labDateTime = reader.GetDateTime(2);
                //var laboDate = "" + labDateTime.Month + "/" + labDateTime.Day + "/" + labDateTime.Year;
                string labPlace = reader.GetString(3);

                var laboratory = new Laboratory()
                {
                    Id = labId,
                    Result = labResult,
                    LabDate = labDateTime,
                    Place = labPlace,
                    Exams = new List<Exam>()
                };

                labs.Add(laboratory);
            }

            reader.Close();
            return labs;
        }
    }
}
