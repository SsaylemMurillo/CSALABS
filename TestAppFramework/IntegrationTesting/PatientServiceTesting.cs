using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Entity;
using BusinessLogicLayer;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Data.Common;

namespace TestAppFramework.IntegrationTesting
{
    [TestClass]
    public class PatientServiceTesting
    {
        private ConnectionManager connectionManager = new ConnectionManager("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");
        
        
        [TestMethod]
        public void S001_AddPatient()
        {
            // Arrange
            var service = new PatientService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");
            var patient = new Patient
            {
                Id = 1003315190,
                FirstName = "Miguel",
                SecondName = "David",
                LastName = "Miguel",
                SecondLastName = "Davi",
                IdType = "CC",
                Address = "CRA 19B3 #6A-52",
                BornDate = Convert.ToDateTime("06/06/1999"),
                Nacionality = "CC",
                ExpeditionDate = Convert.ToDateTime("09/12/2003"),
                ExpeditionPlace = "VALLEDUPAR, CESAR",
                Phone = 300835389
            };

            var response = service.SavePatient(patient);
            connectionManager.OpenDataBase();
            // Assert
            using (var command = new SqlCommand())
            {
                DbConnection connection = connectionManager.Connection;
                command.Connection = (SqlConnection)connection;
                command.CommandText = "SELECT COUNT(*) FROM patient WHERE id = @PacienteId";
                command.Parameters.AddWithValue("@PacienteId", patient.Id);

                var count = (int)command.ExecuteScalar();
                Assert.AreEqual(1, count);
            }
            connectionManager.CloseDataBase();
            Assert.AreEqual("Paciente Correctamente Añadido", response.Message);
        }

        [TestMethod]
        public void S002_ModifyPatient()
        {
            // Arrange
            var service = new PatientService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");
            var patient = new Patient
            {
                Id = 1003315190,
                FirstName = "Miguelito",
                SecondName = "Davidito",
                LastName = "Miguel",
                SecondLastName = "Davi",
                IdType = "CC",
                Address = "CRA 19B3 #6A-52",
                BornDate = Convert.ToDateTime("06/06/1999"),
                Nacionality = "CC",
                ExpeditionDate = Convert.ToDateTime("09/12/2003"),
                ExpeditionPlace = "VALLEDUPAR, CESAR",
                Phone = 300835389
            };

            var response = service.UpdatePatient(patient);
            // Assert
            Assert.AreEqual("Edición Exitosa", response.Message);
        }
        
        
        [TestMethod]
        public void S003_SearchPatient()
        {
            // Arrange
            var service = new PatientService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");
            var patient = new Patient
            {
                Id = 1003315190,
                FirstName = "Miguelito",
                SecondName = "Davidito",
                LastName = "Miguel",
                SecondLastName = "Davi",
                IdType = "CC",
                Address = "CRA 19B3 #6A-52",
                BornDate = Convert.ToDateTime("06/06/1999"),
                Nacionality = "CC",
                ExpeditionDate = Convert.ToDateTime("09/12/2003"),
                ExpeditionPlace = "VALLEDUPAR, CESAR",
                Phone = 300835389
            };

            var response = service.SearchPatient(patient);
            // Assert
            Assert.AreEqual(patient.Id, response.ObjectResponse.Id);
        }
        

        
        [TestMethod]
        public void S004_DeletePatient()
        {
            // Arrange
            var service = new PatientService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");
            var patient = new Patient
            {
                Id = 1003315190,
            };

            var response = service.DeletePatient(patient);
            connectionManager.OpenDataBase();
            // Assert
            using (var command = new SqlCommand())
            {
                DbConnection connection = connectionManager.Connection;
                command.Connection = (SqlConnection)connection;
                command.CommandText = "SELECT COUNT(*) FROM patient WHERE id = @PacienteId";
                command.Parameters.AddWithValue("@PacienteId", patient.Id);

                var count = (int)command.ExecuteScalar();
                Assert.AreEqual(0, count);
            }
            connectionManager.CloseDataBase();
            // Assert
            Assert.AreEqual("Se borro el paciente y sus laboratorios", response.Message);
        }
        
    }
}
    

