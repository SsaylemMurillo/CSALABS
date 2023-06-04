using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Entity;
using BusinessLogicLayer;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Data.Common;
using System.Collections.Generic;

namespace TestAppFramework.IntegrationTesting
{
    [TestClass]
    public class LaboratoryServiceTesting
    {
        private ConnectionManager connectionManager = new ConnectionManager("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");
      
        [TestMethod]
        public void S005_SaveLaboratory()
        {
            // Arrange
            var service = new LaboratoryService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");
            
            var patient = new Patient
            {
                Id = 1003315190,
            };

            var lab = new Laboratory();
            lab.Patient = patient;
            lab.Exams = new List<Exam>();
            lab.Place = "Mikasa";

            var response = service.SaveLaboratory(lab);
            // Assert
            Assert.AreEqual("laboratorio Registrado correctamente", response.Message);
        }
        
        [TestMethod]
        public void S006_EditLaboratory()
        {
            // Arrange
            var service = new LaboratoryService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");

            var patient = new Patient
            {
                Id = 1003315190,
            };

            var lab = new Laboratory();
            lab.Id = 2;
            lab.Patient = patient;
            lab.Exams = new List<Exam>();
            lab.Place = "TUCASA";
            lab.Result = "oiga uste esta que se muere";

            var response = service.UpdateLaboratory(lab);
            // Assert
            Assert.AreEqual("Laboratorio Actualizado correctamente", response.Message);
        }
        
        [TestMethod]
        public void S007_SearchLaboratory()
        {
            // Arrange
            var service = new LaboratoryService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");

            var patient = new Patient
            {
                Id = 1003315190,
            };

            var lab = new Laboratory();
            lab.Id = 2;
            lab.Patient = patient;
            lab.Exams = new List<Exam>();
            lab.Place = "Mikasa";

            var response = service.SearchLaboratory(lab);
            // Assert
            Assert.AreEqual(lab.Id, response.ObjectResponse.Id);
        }

        
        [TestMethod]
        public void S008_DeleteLaboratory()
        {
            // Arrange
            var service = new LaboratoryService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");

            var patient = new Patient
            {
                Id = 1003315190,
            };

            var lab = new Laboratory();
            lab.Id = 2;
            lab.Patient = patient;
            lab.Exams = new List<Exam>();
            lab.Place = "Mikasa";

            var response = service.DeleteLaboratory(lab);
            // Assert
            Assert.AreEqual(lab.Id, response.ObjectResponse.Id);
        }
        
    }
}
