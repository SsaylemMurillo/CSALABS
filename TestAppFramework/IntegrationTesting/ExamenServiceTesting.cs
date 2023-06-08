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
    public class ExamenServiceTesting
    {
        private ConnectionManager connectionManager = new ConnectionManager("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");

        /*
        [TestMethod]
        public void S009_SaveExam()
        {
            // Arrange
            var service = new ExamService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");

            var exam = new Exam()
            {
                Id = 45,
                Name = "Urocultivo",
                ValuesMeasures="lts/gl",
                Description = "cultivo de orina"
            };

            var response = service.SaveExam(exam);
            // Assert
            Assert.AreEqual("Guardado exitosamente", response.Message);
        }
        
        [TestMethod]
        public void S0010_UpdateExam()
        {
            // Arrange
            var service = new ExamService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");

            var exam = new Exam()
            {
                Id = 45,
                Name = "El rehen",
                ValuesMeasures = "lts/gl",
                Description = "cultivo de orina"
            };

            var response = service.UpdateExam(exam);
            // Assert
            Assert.AreEqual("Actualizacion Exitosa", response.Message);
        }

        [TestMethod]
        public void S0011_SearchExam()
        {
            // Arrange
            var service = new ExamService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");

            var exam = new Exam()
            {
                Id = 45,
            };

            var response = service.SearchExam(exam);
            // Assert
            Assert.AreEqual(exam.Id, response.ObjectResponse.Id);
        }
        
        
        [TestMethod]
        public void S0012_DeleteExam()
        {
            // Arrange
            var service = new ExamService("Data Source =.; Initial Catalog = CSALABS; Integrated Security = True");

            var exam = new Exam()
            {
                Id = 45,
                Name = "Urocultivo",
                ValuesMeasures = "lts/gl",
                Description = "cultivo de orina"
            };

            var response = service.DeleteExam(exam);
            // Assert
            Assert.AreEqual("Borrado Exitoso", response.Message);
        }
        */
    }
}
