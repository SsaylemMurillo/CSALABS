using DataAccessLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class LaboratoryService
    {
        private LaboratoryRepository labRepository { get; set; }
        ConnectionManager connectionManager;

        public LaboratoryService(string connectionString)
        {
            connectionManager = new ConnectionManager(connectionString);
            labRepository = new LaboratoryRepository(connectionManager.Connection);
        }

        public GenericResponse<Laboratory> GetAll()
        {
            List<Laboratory> labList = null;
            string message = "Lista de Laboratorios Vacía";
            try
            {
                connectionManager.OpenDataBase();
                labList = labRepository.GetAll();
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                connectionManager.CloseDataBase();
            }
            if (labList == null)
                return new GenericResponse<Laboratory>(message);
            else
                return new GenericResponse<Laboratory>(labList);
        }

        public GenericResponse<Laboratory> SaveLaboratory(Laboratory lab)
        {
            string message = "";
            if (lab != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    // After saving the labs, needs to save the exams and then everything in labs_exams table
                    message = labRepository.Save(lab);
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
                message = "El laboratorio tiene valor NULL";
            }
            return new GenericResponse<Laboratory>(message);
        }

        public GenericResponse<Laboratory> UpdatePatient(Laboratory lab)
        {
            string message = "Edición Exitosa";
            if (lab != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    message = labRepository.Update(lab);
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
                message = "El laboratorio tiene valor NULL";
            }
            return new GenericResponse<Laboratory>(message);
        }

        public GenericResponse<Laboratory> DeletePatient(Laboratory lab)
        {
            string message = "Borrado Exitoso";
            Laboratory labDeleted;
            if (lab != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    // After deleting the laboratory needs to delete all exams of it, and labs_exams references.
                    labDeleted = labRepository.Delete(lab);
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
                message = "El laboratorio tiene valor NULL";
            }
            return new GenericResponse<Laboratory>(message);
        }
    }
}
