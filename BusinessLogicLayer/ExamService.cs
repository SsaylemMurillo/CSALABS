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

        private ConnectionManager ConnectionManager { get; set; }

        public ExamService(string connectionString)
        {
            ConnectionManager = new ConnectionManager(connectionString);
            ExamRepository = new ExamRepository(ConnectionManager.Connection);
            LabsExamRepository = new LabsExamsRepository(ConnectionManager.Connection);
        }

        public ExamService(ConnectionManager connectionManager)
        {
            ConnectionManager = connectionManager;
            ExamRepository = new ExamRepository(ConnectionManager.Connection);
            LabsExamRepository = new LabsExamsRepository(connectionManager.Connection);
        }

        public GenericResponse<Exam> GetAll()
        {
            List<Exam> examList = null;
            string message = "";
            try
            {
                if (!ConnectionManager.IsOpen)
                    ConnectionManager.OpenDataBase();
                examList = ExamRepository.GetAll();
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
            if (examList == null)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(examList);
        }

        public GenericResponse<Exam> SearchExam(Exam exam)
        {
            Exam searchedExam = null;
            string message = null;
            if (exam != null)
            {
                try
                {
                    if (!ConnectionManager.IsOpen)
                        ConnectionManager.OpenDataBase();
                    searchedExam = ExamRepository.Search(exam);
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
                return new GenericResponse<Exam>(searchedExam);
            }
            else
            {
                message = "El examen tiene valor NULL";
            }
            return new GenericResponse<Exam>(message);
        }

        public GenericResponse<Exam> SaveExam(Exam exam)
        {
            string message = null;
            if (exam != null)
            {
                try
                {
                    ConnectionManager.OpenDataBase();
                    message = ExamRepository.Save(exam);
                    return new GenericResponse<Exam>(exam);
                }
                catch (Exception e)
                {
                    message = "Ocurrio un error: " + e.Message;
                }
                finally
                {
                    ConnectionManager.CloseDataBase();
                }
            }
            else
            {
                message = "El examen tiene valor NULL";
            }
            return new GenericResponse<Exam>(message);
        }

        public GenericResponse<Exam> UpdateExam(Exam exam)
        {
            String message="";
            if (exam != null)
            {
                try
                {
                    ConnectionManager.OpenDataBase();
                    message=ExamRepository.Update(exam);

                }
                catch (Exception e)
                {
                    message = "Ocurrio un error: " + e.Message;
                }
                finally
                {
                    ConnectionManager.CloseDataBase();
                }
            }
            else
            {
                message = "El examen tiene valor NULL";
            }
            return new GenericResponse<Exam>(message);
        }

        public GenericResponse<Exam> DeleteAllExamFromLaboratory(int id_laboratory)
        {
            List<Exam> exams = null;
            string message = "";
            try
            {
                if(!ConnectionManager.IsOpen)
                    ConnectionManager.OpenDataBase();
                var response = LabsExamRepository.GetAllExamsFromLab(id_laboratory);
                exams = new List<Exam>();
                if (response != null && response.Count > 0)
                {
                    foreach (var exam in response)
                    {
                        var examSearched = ExamRepository.Search(exam);
                        if (examSearched != null)
                            exams.Add(examSearched);
                    }
                }
                else
                {
                    message = "No fue posible encontrar el laboratorio";
                }
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                if(ConnectionManager.IsOpen)
                    ConnectionManager.CloseDataBase();
            }
            if (exams == null)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(exams);
        }

        public GenericResponse<Exam> DeleteExam(Exam exam)
        {
            string message = "Borrado Exitoso";
            Exam examDeleted;
            if (exam != null)
            {
                try
                {
                    ConnectionManager.OpenDataBase();
                    examDeleted = ExamRepository.Delete(exam);
                    if (examDeleted == null) {
                        message = "El examen no existe";
                    } 
                }
                catch (Exception e)
                {
                    message = "Ocurrio un error: " + e.Message;
                }
                finally
                {
                    ConnectionManager.CloseDataBase();
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
                ConnectionManager.OpenDataBase();
                var exams = ExamRepository.GetAll();
                examListFilter = exams.Where(exam => exam.Id.ToString().Contains(id)).ToList();
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                ConnectionManager.CloseDataBase();
            }
            if (examListFilter == null || examListFilter.Count <= 0)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(examListFilter);
        }
        public GenericResponse<Exam> GetAllExamLaboratory(int id_laboratory)
        {
            List<Exam> exams = null;
            string message = "";
            try
            {
                ConnectionManager.OpenDataBase();
                var response = LabsExamRepository.GetAllExamsFromLab(id_laboratory);
                exams = new List<Exam>();
                if (response != null && response.Count > 0)
                {
                    foreach (var exam in response)
                    {
                        var examSearched = ExamRepository.Search(exam);
                        if (examSearched != null)
                            exams.Add(examSearched);
                    }
                }
                else
                {
                    message = "No fue posible encontrar el laboratorio";
                }
            }
            catch (Exception e)
            {
                message = "Ocurrio un error: " + e.Message;
            }
            finally
            {
                ConnectionManager.CloseDataBase();
            }
            if (exams == null)
                return new GenericResponse<Exam>(message);
            else
                return new GenericResponse<Exam>(exams);
        }
    }
}
