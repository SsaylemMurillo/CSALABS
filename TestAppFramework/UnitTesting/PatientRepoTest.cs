using DataAccessLayer;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAppFramework
{
    [TestClass]
    public class PatientRepoTest
    {
        private ConnectionManager ConnectionManager = new ConnectionManager("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");
        private PatientRepository patientRepository;
        
        
        [TestMethod]
        public void SaveExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 1003315190;
            patient.FirstName = "Miguel";
            patient.SecondName = "David";
            patient.LastName = "Miguel";
            patient.SecondLastName = "David";
            patient.IdType = "CC";
            patient.Address = "CRA 19B3 #6A-52";
            patient.BornDate = Convert.ToDateTime("06/06/1999");
            patient.Nacionality = "CC";
            patient.ExpeditionDate = Convert.ToDateTime("09/12/2003");
            patient.ExpeditionPlace = "VALLEDUPAR, CESAR";
            patient.Phone = 300835389;
            var response = patientRepository.Save(patient);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Ha ocurrido un error inesperado", response);
            ConnectionManager.CloseDataBase();
        }

        

        [TestMethod]
        public void SaveUnExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 101315190;
            patient.FirstName = "Miguel";
            patient.SecondName = "David";
            patient.LastName = "Miguel";
            patient.SecondLastName = "David";
            patient.IdType = "CC";
            patient.Address = "CRA 19B3 #6A-52";
            patient.BornDate = Convert.ToDateTime("06/06/1999");
            patient.Nacionality = "CC";
            patient.ExpeditionDate = Convert.ToDateTime("09/12/2003");
            patient.ExpeditionPlace = "VALLEDUPAR, CESAR";
            patient.Phone = 300835389;
            var response = patientRepository.Save(patient);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Paciente Correctamente Añadido", response);
            ConnectionManager.CloseDataBase();
        }
        

        
        [TestMethod]
        public void SearchExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 1003315190;
            var response = patientRepository.Search(patient);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1003315190, response.Id);
            ConnectionManager.CloseDataBase();
        }


        [TestMethod]
        public void SearchUnExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 1020;
            var response = patientRepository.Search(patient);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1020, response.Id);
            ConnectionManager.CloseDataBase();
        }

        
        
        
        [TestMethod]
        public void UpdateExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 1003315190;
            patient.FirstName = "Miguel";
            patient.SecondName = "David";
            patient.LastName = "Miguel";
            patient.SecondLastName = "David";
            patient.IdType = "CC";
            patient.Address = "CRA 19B3 #6A-52";
            patient.BornDate = Convert.ToDateTime("06/06/1999");
            patient.Nacionality = "CC";
            patient.ExpeditionDate = Convert.ToDateTime("09/12/2003");
            patient.ExpeditionPlace = "VALLEDUPAR, CESAR";
            patient.Phone = 300835389;
            var response = patientRepository.Update(patient);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Paciente Actualizado", response);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void UpdateUnExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 10020;
            patient.FirstName = "Miguel";
            patient.SecondName = "David";
            patient.LastName = "Miguel";
            patient.SecondLastName = "David";
            patient.IdType = "CC";
            patient.Address = "CRA 19B3 #6A-52";
            patient.BornDate = Convert.ToDateTime("06/06/1999");
            patient.Nacionality = "CC";
            patient.ExpeditionDate = Convert.ToDateTime("09/12/2003");
            patient.ExpeditionPlace = "VALLEDUPAR, CESAR";
            patient.Phone = 300835389;
            var response = patientRepository.Update(patient);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("No fue posible actualizar al paciente", response);
            ConnectionManager.CloseDataBase();
        }
        
        
        
        [TestMethod]
        public void DeleteExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 1004315190;
            var response = patientRepository.Delete(patient);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1004315190, response.Id);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void DeleteUnExistentPatient()
        {
            patientRepository = new PatientRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Patient patient = new Patient();
            patient.Id = 1020;
            var response = patientRepository.Delete(patient);
            // Assert
            Assert.IsNull(response);
            ConnectionManager.CloseDataBase();
        }
        
    }
}
