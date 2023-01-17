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
            if (patientList == null || patientList.Count <= 0)
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
    }
}
