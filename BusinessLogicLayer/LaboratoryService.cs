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
        private LaboratoryRepository LabRepository { get; set; }
        private LabsExamsRepository LabsExamsRepository { get; set; }

        ConnectionManager connectionManager;

        public LaboratoryService(string connectionString)
        {
            connectionManager = new ConnectionManager(connectionString);
            LabRepository = new LaboratoryRepository(connectionManager.Connection);
            LabsExamsRepository = new LabsExamsRepository(connectionManager.Connection);
        }

        public GenericResponse<Laboratory> GetAll()
        {
            List<Laboratory> labList = null;
            string message = "Lista de Laboratorios Vacía";
            try
            {
                connectionManager.OpenDataBase();
                labList = LabRepository.GetAll();
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
                    message = LabRepository.Save(lab);
                    if (message != null)
                    {
                        // Save exams using for loop
                        // Save lab_exam row in labs_exams
                        ExamService examService = new ExamService(connectionManager);
                        foreach (var exam in lab.Exams)
                        {
                            var examResponse = examService.SaveExam(exam);
                            if (examResponse.ObjectResponse != null)
                            {
                                LabsExamsRepository.SaveExamFromLaboratory(lab.Id, exam.Id);
                            }
                        }
                        message = "laboratorio Registrado correctamente";
                    }
                    else
                    {
                        message = "No se pudo almacenar el laboratorio";
                    }

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

        public GenericResponse<Laboratory> UpdateLaboratory(Laboratory lab)
        {
            string message = "Edición Exitosa";
            if (lab != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    message = LabRepository.Update(lab);
                    if (message != null)
                    {
                        ExamService examService = new ExamService(connectionManager);
                        foreach (var exam in lab.Exams)
                        {
                            var examResponse = examService.UpdateExam(exam);
                            if (examResponse.ObjectResponse != null)
                            {
                                LabsExamsRepository.Update(lab.Id, exam.Id);
                            }
                        }
                        message = "laboratorio Registrado correctamente";
                    }
                    else
                    {
                        message = "No se pudo almacenar el laboratorio";
                    }
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

        public GenericResponse<Laboratory> DeleteLaboratory(Laboratory lab)
        {
            string message = "Borrado Exitoso";
            Laboratory labDeleted;
            if (lab != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    // After deleting the laboratory needs to delete all exams of it, and labs_exams references.
                    labDeleted = LabRepository.Delete(lab);
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
