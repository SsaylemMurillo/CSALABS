using Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LabsExamsRepository : ICRUD<Laboratory>
    {
        public DbConnection _connection { get; set; }

        public LabsExamsRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public Laboratory Delete(Laboratory laboratory)
        {
            if (Search(laboratory) != null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"delete from labs_exams where labs_exams.id_lab = @labId;";
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

        public List<Laboratory> GetAll()
        {
            throw new NotImplementedException();
        }

        public string Save(Laboratory laboratory)
        {
            throw new NotImplementedException();
        }

        public Laboratory Search(Laboratory laboratory)
        {
            throw new NotImplementedException();
        }

        public string Update(Laboratory laboratory)
        {
            throw new NotImplementedException();
        }

        public string SaveExamFromLaboratory(int laboratoryId, int examId)
        {
            string message;
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"insert into labs_exams (id_lab, id_exam)" +
                $"values (@labId, @examId);";
            command.Parameters.Add(new SqlParameter("@labId", laboratoryId));
            command.Parameters.Add(new SqlParameter("@examId", examId));

            command.ExecuteNonQuery();
            message = "Examen correctamente añadido";

            return message;
        }

        public string DeleteExamFromLaboratory(int laboratoryId, int examId)
        {
            string message;
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"delete from labs_exams where labs_exams.id_lab = @labId and labs_exams.id_exam = @examId;";
            command.Parameters.Add(new SqlParameter("@labId", laboratoryId));
            command.Parameters.Add(new SqlParameter("@examId", examId));

            command.ExecuteNonQuery();
            message = "Se ha borrado el examen perteneciente al laboratorio";

            return message;
        }

        public List<Exam> GetAllExamsFromLab(int id)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from labs_exam where id_lab = @id";
            command.Parameters.Add(new SqlParameter("@id", id));
            var reader = command.ExecuteReader();

            var exams = new List<Exam>();

            while (reader.Read())
            {
                int examId = reader.GetInt32(1);

                exams.Add(new Exam(examId));
            }

            reader.Close();
            return exams;
        }
    }
}
