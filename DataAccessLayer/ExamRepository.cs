using Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ExamRepository : ICRUD<Exam>
    {
        public DbConnection _connection { get; set; }

        public ExamRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public Exam Search(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from exam where id_exam = @id;";
            command.Parameters.Add(new SqlParameter("@id", exam.Id));
            var reader = command.ExecuteReader();

            Exam examnSearched = null;

            while (reader.Read())
            {
                int examId = reader.GetInt32(0);
                string examMeasures = reader.GetString(1);
                string examName = reader.GetString(2);
                string examDescription = reader.GetString(3);

                examnSearched = new Exam(examId, examMeasures, examName, examDescription);
            }
            reader.Close();
            return examnSearched;
        }

        public string Save(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"insert into exam (id_exam, value_measures, name, description, results) " +
                "values (@examId, @examMeasures, @examName, @examDescription, @examResults)";
            command.Parameters.Add(new SqlParameter("@id_exam", exam.Id));
            command.Parameters.Add(new SqlParameter("@examMeasures", exam.ValuesMeasures));
            command.Parameters.Add(new SqlParameter("@examName", exam.Name));
            command.Parameters.Add(new SqlParameter("@examDescription", exam.Description));
            command.Parameters.Add(new SqlParameter("@examResults", ""));

            int fila = command.ExecuteNonQuery();
            if (fila == 1)
            {
                return "Se anexó la tabla examenes con nuevo exam de id: " + exam.Id;
            }
            return "Error en tabla examenes al registrar al examen " + exam.Name + " con id: " + exam.Id;
        }

        public Exam Delete(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"delete from exam where id_exam = @examId;";
            command.Parameters.Add(new SqlParameter("@examId", exam.Id));
            int fila = command.ExecuteNonQuery();
            if (fila == 1)
            {
                return exam;
            }
            return null;
        }

        public String Update(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = $"update exam set id_exam= @examId,value_measures = @examValueMeasures, " +
                $"name = @examName, description = @examDescription, results = @examResults";
            command.Parameters.Add(new SqlParameter("@idExam", exam.Id));
            command.Parameters.Add(new SqlParameter("@valueMeasures", exam.ValuesMeasures));
            command.Parameters.Add(new SqlParameter("@name", exam.Name));
            command.Parameters.Add(new SqlParameter("@description", exam.Description));
            command.Parameters.Add(new SqlParameter("@examResults", ""));
            int fila = command.ExecuteNonQuery();
            if (fila == 1)
            {
                return "Se actualizó la tabla examenes con nuevo exam de id: \" + exam.Id;";
            }
            return "Error en tabla examenes al actualizar al examen " + exam.Name + " con id: " + exam.Id;
        }

        public List<Exam> GetAll()
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from exam";
            var exams = new List<Entity.Exam>();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int examId = reader.GetInt32(0);
                string examMeasures = reader.GetString(1);
                string examName = reader.GetString(2);
                string examDescription = reader.GetString(3);
                var exam = new Exam(examId, examMeasures, examName, examDescription);
                exams.Add(exam);
            }
            return exams;
        }
    }
}
