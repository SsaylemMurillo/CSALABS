using DataAccessLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class PatientService
    {
        public GenericResponse<Patient> GenericResponsePatient { get; set; }
        private PatientRepository patientRepository { get; set; }
        ConnectionManager connectionManager;

        public PatientService(string connectionString)
        {
            connectionManager = new ConnectionManager(connectionString);
            patientRepository = new PatientRepository(connectionManager.Connection);
        }

        public GenericResponse<Patient> GetAll()
        {
            List<Patient> patientList = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                patientList = patientRepository.GetAll();
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
                return new GenericResponse<Patient>("");
            else
                return new GenericResponse<Patient>(patientList);
        }

        public GenericResponse<Patient> SavePatient(Patient patient)
        {
            string message = "";
            if (patient != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    message = patientRepository.Save(patient);
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
                    patientRepository.Update(patient);
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
                    patientDeleted = patientRepository.Delete(patient);
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
                var patients = patientRepository.GetAll();
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

        public GenericResponse<Patient> GetAllFilterIdType(string idType)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAll();
                patientsListFilter = patients.Where(patient => patient.IdType.ToString().Contains(idType)).ToList();
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

        public GenericResponse<Patient> GetAllFilterFirstName(string firstName)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAll();
                patientsListFilter = patients.Where(patient => patient.FirstName.ToString().Contains(firstName)).ToList();
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

        public GenericResponse<Patient> GetAllFilterAddress(string address)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAll();
                patientsListFilter = patients.Where(patient => patient.Address.ToString().Contains(address)).ToList();
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

        public GenericResponse<Patient> GetAllFilterBornDate(string bornDate)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAll();
                patientsListFilter = patients.Where(patient => patient.BornDate.ToString().Contains(bornDate)).ToList();
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
