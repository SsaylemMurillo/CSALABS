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
    public class ExamReporitory : ICRUD<Exam>
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

        public Exam Save(int id)
        {
            DbCommand command = new SqlCommand();
            command.Connection = _connection;
            OpenDataBase();
            command.CommandText = $"select * from examen where codigo_examen = @id;";
            command.Parameters.Add(new SqlParameter("@id", id));
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
            Close();
            return pacienteReturn;
        }

        public string Insertar(Exam examen)
        {
            DbCommand command = new SqlCommand();
            command.Connection = conexion;
            AbrirConnexion();
            command.CommandText = $"insert into examen (codigo_examen, valores_medidas, nombre, descripcion) " +
                "values (@codigo_examen, @valores_medidas, @nombre, @descripcion)";
            command.Parameters.Add(new SqlParameter("@codigo_examen", examen.Id));
            command.Parameters.Add(new SqlParameter("@valores_medidas", examen.valoresMedidas));
            command.Parameters.Add(new SqlParameter("@nombre", examen.Nombre));
            command.Parameters.Add(new SqlParameter("@descripcion", examen.Descripcion));
            int fila = command.ExecuteNonQuery();
            CerrarConnexion();
            if (fila == 1)
            {
                return "Se Actualizo la tabla examenes con nuevo examen de id: " + examen.Id;
            }
            return "Error en tabla examen al registrar al examen " + examen.Nombre + " de id: " + examen.Id;
        }

        public string Eliminar(Exam examen)
        {
            DbCommand command = new SqlCommand();
            command.Connection = conexion;
            AbrirConnexion();
            command.CommandText = $"delete from examen where codigo_examen = @id;";
            command.Parameters.Add(new SqlParameter("@id", examen.Id));
            int fila = command.ExecuteNonQuery();
            CerrarConnexion();
            if (fila == 1)
            {
                return "Se Actualizo la informacion de la tabla examenen de id: " + examen.Id;
            }
            return "Error en tabla pacientes al borrar el examen de id: " + examen.Id;
        }

        public string Actualizar(Exam examen)
        {
            DbCommand command = new SqlCommand();
            command.Connection = conexion;
            AbrirConnexion();

            command.CommandText = $"update examen set valores_medidas = @valoresMedidas, nombre = @nombre,descripcion = @descripcion ";
            command.Parameters.Add(new SqlParameter("@valoresMedidas", examen.valoresMedidas));
            command.Parameters.Add(new SqlParameter("@nombre", examen.Nombre));
            command.Parameters.Add(new SqlParameter("@descripcion", examen.Descripcion));
            int fila = command.ExecuteNonQuery();
            CerrarConnexion();
            if (fila == 1)
            {
                return "Se Actualizo la informacion de la tabla examen con codigo: " + examen.Id;
            }
            return "Error en tabla pacientes al actualizar el examen con codigo: " + examen.Id;
        }

        public List<Exam> Todos()
        {
            string _sql = string.Format("select * from examen");
            var cmd = new SqlCommand(_sql, conexion);
            AbrirConnexion();
            var ListaExamen = new List<Entity.Exam>();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int codigoExamen = reader.GetInt32(0);
                string valoresMedidas = reader.GetString(1);
                string nombre = reader.GetString(2);
                string descripcion = reader.GetString(3);
                var examen = new Entity.Exam(codigoExamen, nombre, descripcion, valoresMedidas);
                ListaExamen.Add(examen);
            }
            CerrarConnexion();
            return ListaExamen;
        }
        public List<Exam> Todos(int id)
        {
            string _sql = string.Format("select * from laboratorio_examenes where id_lab = @id");
            var cmd = new SqlCommand(_sql, conexion);
            AbrirConnexion();
            var listaExamenes = new List<Entity.Exam>();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int cod_examen = reader.GetInt32(0);
                string valoresMedidas = reader.GetString(1);
                string nombre = reader.GetString(2);
                string descripcion = reader.GetString(3);
                var examen = new Entity.Exam(cod_examen, nombre, descripcion, valoresMedidas);
                listaExamenes.Add(examen);
            }
            CerrarConnexion();
            return listaExamenes;
        }
    }
}
