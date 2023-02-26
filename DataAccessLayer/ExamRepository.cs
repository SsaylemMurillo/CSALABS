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
        public Patient Buscar(string id)
        {
            return null;
        }

        //public Paciente MappingPaciente(var reader)
        //{
        //    int idPaciente = reader.GetInt32(0);
        //    string apellidos = reader.GetString(1);
        //    string nombres = reader.GetString(2);
        //    string tipo_id = reader.GetString(3);
        //    string nacionalidad = reader.GetString(4);
        //    int telefono = Int32.Parse(reader.GetString(5));

        //    return new Paciente(nacionalidad, tipo_id, telefono, "11/11/2003", nombres, apellidos, idPaciente);
        //}

        public Exam Search(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from exam where codigo_examen = @id;";
            command.Parameters.Add(new SqlParameter("@id", exam.Id));
            var reader = command.ExecuteReader();

            Exam pacienteReturn = null;

            while (reader.Read())
            {
                int codigo_examen = reader.GetInt32(0);
                string valores_medidas = reader.GetString(1);
                string nombre = reader.GetString(2);
                string descripcion = reader.GetString(3);

                pacienteReturn = new Exam(codigo_examen, nombre, descripcion, valores_medidas);
            }
            reader.Close();
            return pacienteReturn;
        }

        public string Save(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"insert into exam (codigo_examen, valores_medidas, nombre, descripcion) " +
                "values (@codigo_examen, @valores_medidas, @nombre, @descripcion)";
            command.Parameters.Add(new SqlParameter("@id_exam", exam.Id));
            command.Parameters.Add(new SqlParameter("@values_measures", exam.ValuesMeasures));
            command.Parameters.Add(new SqlParameter("@name", exam.Name));
            command.Parameters.Add(new SqlParameter("@description", exam.Description));

            int fila = command.ExecuteNonQuery();
            if (fila == 1)
            {
                return "Se Actualizo la tabla examenes con nuevo exam de id: " + exam.Id;
            }
            return "Error en tabla exam al registrar al exam " + exam.Name + " de id: " + exam.Id;
        }

        public Exam Delete(Exam exam)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"delete from exam where id_exam = @id;";
            command.Parameters.Add(new SqlParameter("@id", exam.Id));
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

            command.CommandText = $"update exam set valores_medidas = @valoresMedidas, nombre = @nombre,descripcion = @descripcion ";
            command.Parameters.Add(new SqlParameter("@valoresMedidas", exam.ValuesMeasures));
            command.Parameters.Add(new SqlParameter("@nombre", exam.Name));
            command.Parameters.Add(new SqlParameter("@descripcion", exam.Description));
            int fila = command.ExecuteNonQuery();
            if (fila == 1)
            {
                return "";
            }
            return "";
        }

        public List<Exam> GetAll()
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from exam";
            var ListaExamen = new List<Entity.Exam>();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int codigoExamen = reader.GetInt32(0);
                string valoresMedidas = reader.GetString(1);
                string nombre = reader.GetString(2);
                string descripcion = reader.GetString(3);
                var examen = new Entity.Exam(codigoExamen, nombre, descripcion, valoresMedidas);
                ListaExamen.Add(examen);
            }
            return ListaExamen;
        }
        //public List<Exam> Todos(int id)
        //{
        //    string _sql = string.Format("select * from laboratorio_examenes where id_lab = @id");
        //    var cmd = new SqlCommand(_sql, conexion);
        //    AbrirConnexion();
        //    var listaExamenes = new List<Entity.Exam>();

        //    var reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        int cod_examen = reader.GetInt32(0);
        //        string valoresMedidas = reader.GetString(1);
        //        string nombre = reader.GetString(2);
        //        string descripcion = reader.GetString(3);
        //        var examen = new Entity.Exam(cod_examen, nombre, descripcion, valoresMedidas);
        //        listaExamenes.Add(examen);
        //    }
        //    CerrarConnexion();
        //    return listaExamenes;
        //}
    }
}
