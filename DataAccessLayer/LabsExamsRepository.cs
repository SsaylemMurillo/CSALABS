﻿using Entity;
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

        public string Update(Exam exam)
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
                return "Actualizacion Exitosa";
            }
            return "Error en tabla examenes al actualizar al examen " + exam.Name + " con id: " + exam.Id;
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
            command.CommandText = $"select id_exam from labs_exam where id_lab = @id";
            command.Parameters.Add(new SqlParameter("@id", id));
            var reader = command.ExecuteReader();

            var exams = new List<Exam>();

            while (reader.Read())
            {
                int examId = reader.GetInt32(0);
                exams.Add(new Exam(examId));
            }

            reader.Close();
            return exams;
        }
    }
}
