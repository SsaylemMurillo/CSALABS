using DataAccessLayer;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAppFramework
{
    [TestClass]
    public class LabsExamRepoTest
    {
        private ConnectionManager ConnectionManager = new ConnectionManager("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");
        private LabsExamsRepository labsExamRepository;


        
        [TestMethod]
        public void SaveUnExistentLab()
        {
            labsExamRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            var response = labsExamRepository.SaveExamFromLaboratory(6, 123);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Lab/Examen correctamente añadido", response);
            ConnectionManager.CloseDataBase();
        }
        
        
        
        [TestMethod]
        public void SaveExistentLab()
        {
            labsExamRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            var response = labsExamRepository.SaveExamFromLaboratory(6, 123);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Lab/Examen ya existente", response);
            ConnectionManager.CloseDataBase();
        }
        

        [TestMethod]
        public void SearchExistentLab()
        {
            labsExamRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            var response = labsExamRepository.SearchExamFromLaboratory(6, 123);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(true, response);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void SearchUnExistentLab()
        {
            labsExamRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            var response = labsExamRepository.SearchExamFromLaboratory(2, 123);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(false, response);
            ConnectionManager.CloseDataBase();
        }
       

        [TestMethod]
        public void DeleteExistentLab()
        {
            labsExamRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            var response = labsExamRepository.DeleteExamFromLaboratory(6, 123);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Se ha borrado el examen perteneciente al laboratorio", response);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void DeleteUnExistentLab()
        {
            labsExamRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            var response = labsExamRepository.DeleteExamFromLaboratory(2, 123);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("No se pudo borrar el laboratorio/examen", response);
            ConnectionManager.CloseDataBase();
        }
        
    }
}
