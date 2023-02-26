using DataAccessLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogicLayer
{
    public class ExamService
    {
        private ExamRepository ExamRepository { get; set; }
        ConnectionManager connectionManager;

        public ExamService(string connectionString)
        {
            connectionManager = new ConnectionManager(connectionString);
            ExamRepository = new ExamRepository(connectionManager.Connection);
        }

        public GenericResponse<Exam> GetAll()
        {
            List<Exam> patientList = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                patientList = DataAccessLayer.ExamRepository.GetAll();
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                connectionManager.CloseDataBase();
            }
            if (patientList == null)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(patientList);
        }

        public GenericResponse<Patient> SavePatient(Patient patient)
        {
            string message = "";
            if (patient != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    message = ExamRepository.Save(patient);
                }
                catch (Exception e)
                {
                    message = "Ocurrio un error: " + e.Message;
                }
                finally
                {
                    connectionManager.CloseDataBase();
                }
            }
            else
            {
                message = "El paciente tiene valor NULL";
            }
            return new GenericResponse<Patient>(message);
        }

        public GenericResponse<Patient> UpdatePatient(Patient patient)
        {
            string message = "Edición Exitosa";
            if (patient != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    ExamRepository.Update(patient);
                }
                catch (Exception e)
                {
                    message = "Ocurrio un error: " + e.Message;
                }
                finally
                {
                    connectionManager.CloseDataBase();
                }
            }
            else
            {
                message = "El paciente tiene valor NULL";
            }
            return new GenericResponse<Patient>(message);
        }

        public GenericResponse<Patient> DeletePatient(Patient patient)
        {
            string message = "Borrado Exitoso";
            Patient patientDeleted;
            if (patient != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    patientDeleted = ExamRepository.Delete(patient);
                }
                catch (Exception e)
                {
                    message = "Ocurrio un error: " + e.Message;
                }
                finally
                {
                    connectionManager.CloseDataBase();
                }
            }
            else
            {
                message = "El paciente tiene valor NULL";
            }
            return new GenericResponse<Patient>(message);
        }

        public GenericResponse<Patient> GetAllFilterId(string id)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = ExamRepository.GetAll();
                patientsListFilter = patients.Where(patient => patient.Id.ToString().Contains(id)).ToList();
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                connectionManager.CloseDataBase();
            }
            if (patientsListFilter == null || patientsListFilter.Count <= 0)
                return new GenericResponse<Patient>(message);
            else
                return new GenericResponse<Patient>(patientsListFilter);
        }
    }
}
