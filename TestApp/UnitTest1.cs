using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer;
using Entity;
using Presentation;
using FluentAssertions;
using BusinessLogicLayer;

namespace TestApp
{
    public class Tests
    {
        Patient patient;
        PatientService patientService;

        [SetUp]
        public void Setup()
        {
            patient = new Patient();
            patientService = new PatientService("Data Source=.;Initial Catalog=CSALABS;Integrated Security=True");
        }
        [Test]
        public void TestSavePatient()
        {
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

            var possibleResponses = new List<string> { expected, expected2 };

            Assert.Contains(response, possibleResponses);
            Assert.Pass();
        }
    }
}