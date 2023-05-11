using DataAccessLayer;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAppFramework
{
    [TestClass]
    public class ExamRepoTest
    {
        private ConnectionManager ConnectionManager = new ConnectionManager("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");
        private ExamRepository examRepository;

        /*
        [TestMethod]
        public void SaveUnExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 857;
            exam.Description = "evaluación del estado de salud general y detección de amplia variedad de enfermedades, incluida la anemia, las infecciones y la leucemia.";
            exam.ValuesMeasures = "26,7 a 49,2 U/g Hb";
            exam.Name = "Hemograma completo";
            
            var response = examRepository.Save(exam);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Guardado exitosamente", response);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void SaveExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 1003315190;
            exam.Description = "evaluación del estado de salud general y detección de amplia variedad de enfermedades, incluida la anemia, las infecciones y la leucemia.";
            exam.ValuesMeasures = "26,7 a 49,2 U/g Hb";
            exam.Name = "Hemograma completo";

            var response = examRepository.Save(exam);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Error en tabla examenes al registrar al examen", response);
            ConnectionManager.CloseDataBase();
        }
        */

        /*
        [TestMethod]
        public void SearchExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 123;

            var response = examRepository.Search(exam);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(123, response.Id);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void SearchUnExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 1003315190;

            var response = examRepository.Search(exam);
            // Assert
            Assert.IsNull(response);
            ConnectionManager.CloseDataBase();
        }
        */

        /*
        [TestMethod]
        public void DeleteExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 123;

            var response = examRepository.Delete(exam);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(123, response.Id);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void DeleteUnExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 1003315190;

            var response = examRepository.Delete(exam);
            // Assert
            Assert.IsNull(response);
            ConnectionManager.CloseDataBase();
        }
        

        [TestMethod]
        public void UpdateUnExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 857;
            exam.Description = "evaluación del estado de salud general y detección de amplia variedad de enfermedades, incluida la anemia, las infecciones y la leucemia.";
            exam.ValuesMeasures = "26,7 a 49,2 U/g Hb";
            exam.Name = "Hemograma";

            var response = examRepository.Update(exam);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Error en tabla examenes al actualizar al examen", response);
            ConnectionManager.CloseDataBase();
        }

        [TestMethod]
        public void UpdateExistentExam()
        {
            examRepository = new ExamRepository(ConnectionManager.Connection);
            ConnectionManager.OpenDataBase();
            // Act
            Exam exam = new Exam();
            exam.Id = 123;
            exam.Description = "evaluación del estado de salud general y detección de amplia variedad de enfermedades, incluida la anemia, las infecciones y la leucemia.";
            exam.ValuesMeasures = "26,7 a 49,2 U/g Hb";
            exam.Name = "Hemograma";

            var response = examRepository.Update(exam);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Actualizacion Exitosa", response);
            ConnectionManager.CloseDataBase();
        }
        */
    }
}
