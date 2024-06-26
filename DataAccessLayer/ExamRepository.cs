﻿using Entity;
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

                examnSearched = new Exam(examId, examName, examDescription, examMeasures);
            }
            reader.Close();
            return examnSearched;
        }

        public string Save(Exam exam)
        {
            string message="";
            if (Search(exam) == null)
            {
                DbCommand command = new SqlCommand();
                command.Connection = _connection;
                command.CommandText = $"insert into exam (id_exam, value_measures, name, description, results) " +
                    "values (@examId, @examMeasures, @examName, @examDescription, @examResults)";
                command.Parameters.Add(new SqlParameter("@examId", exam.Id));
                command.Parameters.Add(new SqlParameter("@examMeasures", exam.ValuesMeasures));
                command.Parameters.Add(new SqlParameter("@examName", exam.Name));
                command.Parameters.Add(new SqlParameter("@examDescription", exam.Description));
                command.Parameters.Add(new SqlParameter("@examResults", "aroaroaro"));

                int fila = command.ExecuteNonQuery();

                if (fila == 1)
                {
                    message="Guardado exitosamente";
                }
            }
            else
            {
                message="Error en tabla examenes al registrar al examen";
            }
            return message;
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

        public string Update(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = $"update exam set value_measures = @examValueMeasures, " +
                $"name = @examName, description = @examDescription, results = @examResults where id_exam = @examId;";
            command.Parameters.Add(new SqlParameter("@examId", exam.Id));
            command.Parameters.Add(new SqlParameter("@examValueMeasures", exam.ValuesMeasures));
            command.Parameters.Add(new SqlParameter("@examName", exam.Name));
            command.Parameters.Add(new SqlParameter("@examDescription", exam.Description));
            command.Parameters.Add(new SqlParameter("@examResults", ""));
            int fila = command.ExecuteNonQuery();
            if (fila == 1)
            {
                return "Actualizacion Exitosa";
            }
            return "Error en tabla examenes al actualizar al examen";
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
                var exam = new Exam(examId, examName, examDescription, examMeasures);
                exams.Add(exam);
            }
            return exams;
        }
    }
}
