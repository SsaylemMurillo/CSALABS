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
        private PatientRepository patientRepository { get; set; }
        ConnectionManager connectionManager;

        public PatientService(string connectionString)
        {
            connectionManager = new ConnectionManager(connectionString);
            patientRepository = new PatientRepository(connectionManager.Connection);
        }

        public GenericResponse GetAll()
        {
            List<Patient> patientList = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                patientList = patientRepository.GetAllPatients();
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
                return new GenericResponse(message);
            else
                return new GenericResponse(patientList);
        }

        public GenericResponse SavePatient(Patient patient)
        {
            string message = "Guardado Exitoso";
            Patient patientSaved;
            if (patient != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    patientSaved = patientRepository.Save(patient);
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
            return new GenericResponse(message);
        }

        public GenericResponse UpdatePatient(Patient patient)
        {
            string message = "Edición Exitosa";
            if (patient != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    patientRepository.UpdatePatient(patient);
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
            return new GenericResponse(message);
        }

        public GenericResponse DeletePatient(Patient patient)
        {
            string message = "Borrado Exitoso";
            Patient patientDeleted;
            if (patient != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    patientDeleted = patientRepository.DeletePatient(patient);
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
            return new GenericResponse(message);
        }

        public GenericResponse GetAllFilterId(string id)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAllPatients();
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
                return new GenericResponse(message);
            else
                return new GenericResponse(patientsListFilter);
        }

        public GenericResponse GetAllFilterIdType(string idType)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAllPatients();
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
                return new GenericResponse(message);
            else
                return new GenericResponse(patientsListFilter);
        }

        public GenericResponse GetAllFilterFirstName(string firstName)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAllPatients();
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
                return new GenericResponse(message);
            else
                return new GenericResponse(patientsListFilter);
        }

        public GenericResponse GetAllFilterAddress(string address)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAllPatients();
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
                return new GenericResponse(message);
            else
                return new GenericResponse(patientsListFilter);
        }

        public GenericResponse GetAllFilterBornDate(string bornDate)
        {
            List<Patient> patientsListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = patientRepository.GetAllPatients();
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
                return new GenericResponse(message);
            else
                return new GenericResponse(patientsListFilter);
        }
    }
}
