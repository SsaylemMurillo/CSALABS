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
        private ConnectionManager ConnectionManager { get; set; }

        public LaboratoryService(string connectionString)
        {
            ConnectionManager = new ConnectionManager(connectionString);
            LabRepository = new LaboratoryRepository(ConnectionManager.Connection);
            LabsExamsRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ExamService = new ExamService(ConnectionManager);
        }

        public LaboratoryService(ConnectionManager connectionManager)
        {
            ConnectionManager = connectionManager;
            LabRepository = new LaboratoryRepository(ConnectionManager.Connection);
            LabsExamsRepository = new LabsExamsRepository(ConnectionManager.Connection);
            ExamService = new ExamService(ConnectionManager);
        }


        public GenericResponse<Laboratory> GetAll()
        {
            List<Laboratory> labList = null;
            string message = "Lista de Laboratorios Vacía";
            try
            {
                if (!ConnectionManager.IsOpen)
                    ConnectionManager.OpenDataBase();
                labList = LabRepository.GetAll();
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                if (ConnectionManager.IsOpen)
                    ConnectionManager.CloseDataBase();
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
                if (!ConnectionManager.IsOpen)
                    ConnectionManager.OpenDataBase();
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
                if (ConnectionManager.IsOpen)
                    ConnectionManager.CloseDataBase();
            }
            return new GenericResponse<Laboratory>(message);
        }

        public GenericResponse<Laboratory> SearchLaboratories(Patient patient)
        {
            string message = "";
            try
            {
                if (!ConnectionManager.IsOpen)
                    ConnectionManager.OpenDataBase();
                // After saving the labs, needs to save the exams and then everything in labs_exams table
                var labs = LabRepository.GetAllLabsFromPatient(patient.Id);
                if (labs != null && labs.Count > 0)
                {
                    foreach (var laboratory in labs)
                    {
                        if (!ConnectionManager.IsOpen)
                            ConnectionManager.OpenDataBase();
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
                if (ConnectionManager.IsOpen)
                    ConnectionManager.CloseDataBase();
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
                    if (!ConnectionManager.IsOpen)
                        ConnectionManager.OpenDataBase();
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
                    if (ConnectionManager.IsOpen)
                        ConnectionManager.CloseDataBase();
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
                if (!ConnectionManager.IsOpen)
                    ConnectionManager.OpenDataBase();
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
                    ExamService examService = new ExamService(ConnectionManager);
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
                if (ConnectionManager.IsOpen)
                    ConnectionManager.CloseDataBase();
            }
            return new GenericResponse<Laboratory>(message);
        }

        public GenericResponse<Laboratory> DeleteAllLaboratoryOfPatient(Patient patient)
        {
            string message = "Borrado Exitoso";
            if (patient != null)
            {
                try
                {
                    if (!ConnectionManager.IsOpen)
                        ConnectionManager.OpenDataBase();
                    // After deleting the laboratory needs to delete all exams of it, and labs_exams references.
                    var labs = LabRepository.GetAllLabsFromPatient(patient.Id);
                    if (labs != null)
                    {
                        foreach (var item in labs)
                        {
                            LabsExamsRepository.Delete(item);
                        }
                        LabRepository.DeleteAllLabsFromAPatient(patient);
                        return new GenericResponse<Laboratory>(labs);
                    }
                    else
                    {
                        message = "No se pudo borrar el laboratorio";
                    }
                }
                catch (Exception e)
                {
                    message = "Ocurrio un error: " + e.Message;
                }
                finally
                {
                    if (ConnectionManager.IsOpen)
                        ConnectionManager.CloseDataBase();
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
                    if (!ConnectionManager.IsOpen)
                        ConnectionManager.OpenDataBase();
                    // After deleting the laboratory needs to delete all exams of it, and labs_exams references.
                    var response = LabsExamsRepository.Delete(lab);
                    if (response != null)
                    {
                        labDeleted = LabRepository.Delete(lab);
                        if (labDeleted != null)
                        {
                            return new GenericResponse<Laboratory>(labDeleted);
                        }
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
                    if (ConnectionManager.IsOpen)
                        ConnectionManager.CloseDataBase();
                }
            }
            else
            {
                message = "El laboratorio tiene valor NULL";
            }
            return new GenericResponse<Laboratory>(message);
        }

        public GenericResponse<Laboratory> DeleteExamsFromLaboratory(Laboratory laboratory, Exam exam)
        {
            string message = "Borrado Exitoso";
            if (laboratory != null && exam != null)
            {
                try
                {
                    if (!ConnectionManager.IsOpen)
                        ConnectionManager.OpenDataBase();
                    // After deleting the laboratory needs to delete all exams of it, and labs_exams references.
                    var response = LabsExamsRepository.DeleteExamFromLaboratory(laboratory.Id, exam.Id);
                    if (response != null)
                    {
                        return new GenericResponse<Laboratory>(message);
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
                    if (ConnectionManager.IsOpen)
                        ConnectionManager.CloseDataBase();
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
