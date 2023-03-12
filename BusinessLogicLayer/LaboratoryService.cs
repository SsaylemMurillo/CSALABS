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
        private ExamService ExamService { get; set; }

        ConnectionManager connectionManager;

        public LaboratoryService(string connectionString)
        {
            connectionManager = new ConnectionManager(connectionString);
            LabRepository = new LaboratoryRepository(connectionManager.Connection);
            LabsExamsRepository = new LabsExamsRepository(connectionManager.Connection);
            ExamService = new ExamService(connectionManager);
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

        public GenericResponse<Laboratory> SearchLaboratory(Laboratory laboratory)
        {
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                // After saving the labs, needs to save the exams and then everything in labs_exams table
                var lab = LabRepository.Search(laboratory);
                if (lab != null)
                {
                    return new GenericResponse<Laboratory>(lab);
                }
                else
                {
                    message = "No se encontró el laboratorio";
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
            return new GenericResponse<Laboratory>(message);
        }

        public GenericResponse<Laboratory> SearchLaboratories(Patient patient)
        {
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                // After saving the labs, needs to save the exams and then everything in labs_exams table
                var labs = LabRepository.GetAllLabsFromPatient(patient.Id);
                if (labs != null && labs.Count > 0)
                {
                    foreach (var laboratory in labs)
                    {
                        var exams = LabsExamsRepository.GetAllExamsFromLab(laboratory.Id);
                        if (exams != null)
                        {
                            foreach (Exam item in exams)
                            {
                                var examDB = ExamService.SearchExam(item);

                                if (examDB.ObjectResponse != null)
                                {
                                    laboratory.Exams.Add(examDB.ObjectResponse);
                                }
                            }
                        }
                    }
                    return new GenericResponse<Laboratory>(labs);
                }
                else
                {
                    message = "No se encontraron laboratorios para el paciente"; 
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
            return new GenericResponse<Laboratory>(message);
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
                        foreach (var exam in lab.Exams)
                        {
                            var examResponse = ExamService.SaveExam(exam);
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
            try
            {
                connectionManager.OpenDataBase();
                if (LabRepository.Search(lab) != null)
                    message = LabRepository.Update(lab);
                else
                {
                    message = LabRepository.Save(lab);
                    if (message != null) {
                        var lastAddedLab = LabRepository.GetLastLaboratoryCreated();
                        if (lastAddedLab != null)
                        {
                            lab.Id = lastAddedLab.Id;
                        }
                    }
                }
                if (message != null)
                {
                    ExamService examService = new ExamService(connectionManager);
                    foreach (var exam in lab.Exams)
                    {
                        var examResponse = LabsExamsRepository.SaveExamFromLaboratory(lab.Id, exam.Id);
                    }
                    message = "Laboratorio Actualizado correctamente";
                }
                else
                {
                    message = "No se pudo actualizar el laboratorio";
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
