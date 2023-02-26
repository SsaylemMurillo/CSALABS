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
        private LabsExamsRepository LabsExamRepository { get; set; }
        ConnectionManager connectionManager;

        public ExamService(string connectionString)
        {
            connectionManager = new ConnectionManager(connectionString);
            ExamRepository = new ExamRepository(connectionManager.Connection);
        }

        public GenericResponse<Exam> GetAll()
        {
            List<Exam> examList = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                examList = ExamRepository.GetAll();
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                connectionManager.CloseDataBase();
            }
            if (examList == null)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(examList);
        }

        public GenericResponse<Exam> SaveExam(Exam exam)
        {
            string message = "";
            if (exam != null)
            {
                try
                {
                    
                    connectionManager.OpenDataBase();
                    message = ExamRepository.Save(exam);
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
                message = "El examen tiene valor NULL";
            }
            return new GenericResponse<Exam>(message);
        }

        public GenericResponse<Patient> UpdatePatient(Exam exam)
        {
            string message = "Edición Exitosa";
            if (exam != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    ExamRepository.Update(exam);
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

        public GenericResponse<Exam> DeletePatient(Exam exam)
        {
            string message = "Borrado Exitoso";
            Exam patientDeleted;
            if (exam != null)
            {
                try
                {
                    connectionManager.OpenDataBase();
                    patientDeleted = ExamRepository.Delete(exam);
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
                message = "El examen tiene valor NULL";
            }
            return new GenericResponse<Exam>(message);
        }

        public GenericResponse<Exam> GetAllFilterId(string id)
        {
            List<Exam> examListFilter = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                var patients = ExamRepository.GetAll();
                examListFilter = patients.Where(patient => patient.Id.ToString().Contains(id)).ToList();
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                connectionManager.CloseDataBase();
            }
            if (examListFilter == null || examListFilter.Count <= 0)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(examListFilter);
        }
        public GenericResponse<Exam> GetAllExamLaboratory(int id_laboratory)
        {
            List<Exam> examList = null;
            string message = "";
            try
            {
                connectionManager.OpenDataBase();
                examList = LabsExamRepository.GetAllExamsFromLab(id_laboratory);
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                connectionManager.CloseDataBase();
            }
            if (examList == null)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(examList);
        }
    }
}
