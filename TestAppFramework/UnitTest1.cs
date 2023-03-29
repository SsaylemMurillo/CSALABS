using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Entity;
using BusinessLogicLayer;
using DataAccessLayer;
using Presentation;

namespace TestAppFramework
{
    [TestClass]
    public class UnitTest1
    {
        Patient patient;
        PatientService patientService;


        [TestMethod]
        public void RegisterPatient()
        {
            patient = new Patient();
            patientService = new PatientService("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");

            patient.Id = 123456789;
            patient.IdType = "CC";
            patient.FirstName = "pepi";
            patient.SecondName = "pepito";
            patient.LastName = "elpepe";
            patient.SecondLastName = "elpepito";
            patient.BornDate = new DateTime(1999, 12, 12);
            patient.ExpeditionDate = new DateTime(2019, 12, 12);
            patient.ExpeditionPlace = "Medellin";
            patient.Phone = 123456789;
            patient.Address = "Calle 123";
            patient.Nacionality = "Colombia";


            var response = patientService.SavePatient(patient);
            var expected = "Paciente Correctamente Añadido";
            var expected2 = "Ha ocurrido un error inesperado";

            //var possibleResponses = new List<string> { expected, expected2 };

            //Assert.AreEqual(expected, response.Message);
            Assert.AreEqual(expected2, response.Message);
        }

    }
}
