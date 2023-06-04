using DataAccessLayer;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.ConstrainedExecution;

namespace TestAppFramework
{
    [TestClass]
    public class LabRepoTest
    {
        private ConnectionManager ConnectionManager = new ConnectionManager("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");
        private LaboratoryRepository laboratoryRepository;

        
        [TestMethod]
        public void SaveUnExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory(123,  "VALLEDUPAR, CESAR");
            var response = laboratoryRepository.Save(laboratory);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Laboratorio Correctamente Añadido", response);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void SaveExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory(123, "VALLEDUPAR, CESAR");
            var response = laboratoryRepository.Save(laboratory);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("El laboratorio no se pudo crear", response);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void SearchExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory() { Id = 5 };
            var response = laboratoryRepository.Search(laboratory);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(laboratory.Id, response.Id);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void SearchUnExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory() { Id = 2 };
            var response = laboratoryRepository.Search(laboratory);
            // Assert
            Assert.IsNull(response);
            ConnectionManager.CloseDataBase();
        }
        

        [TestMethod]
        public void DeleteExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory() { Id = 5 };
            var response = laboratoryRepository.Delete(laboratory);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(laboratory.Id, response.Id);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void DeleteUnExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory() { Id = 2 };
            var response = laboratoryRepository.Delete(laboratory);
            // Assert
            Assert.IsNull(response);
            ConnectionManager.CloseDataBase();
        }
        

        [TestMethod]
        public void UpdateUnExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory(123, "VALLEDUPAR, CESAR") {Id = 2};
            var response = laboratoryRepository.Update(laboratory);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("No se pudo actualizar el laboratorio", response);
            ConnectionManager.CloseDataBase();
        }
        
        [TestMethod]
        public void UpdateExistentLab()
        {
            laboratoryRepository = new LaboratoryRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Laboratory laboratory = new Laboratory(123, "EL RITO") { Id = 5 };
            var response = laboratoryRepository.Update(laboratory);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Laboratorio Actualizado Correctamente", response);
            ConnectionManager.CloseDataBase();
        }
        
    }
}
