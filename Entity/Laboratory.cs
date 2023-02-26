using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Laboratory
    {
        public int Id { get; set; }
        public List<Exam> Exams { get; set; }
        public Patient Patient { get; set; }
        public DateTime LabDate { get; set; }
        public String Result { get; set; }
        public String Place { get; set; }

        public Laboratory(int id, int patientId, string result, string labDate, string place)
        {
            Id = id;
            Exams = new List<Exam>();
            Patient = new Patient();
            Patient.Id = patientId;
            Result = result;
            LabDate = Convert.ToDateTime(labDate);
            Place = place;
        }

        public Laboratory(int id, Patient patient, string result, string labDate, string place)
        {
            Id = id;
            Exams = new List<Exam>();
            Patient = patient;
            Result = result;
            LabDate = Convert.ToDateTime(labDate);
            Place = place;
        }

        public Laboratory(List<Exam> exams, Patient patient, string result, string labDate, string place, int orderId)
        {
            Exams = exams;
            Patient = patient;
            Result = result;
            LabDate = Convert.ToDateTime(labDate);
            Place = place;
            this.Id = orderId;
        }

        public Laboratory(List<Exam> exams, Patient patient, string result, string labDate, string place)
        {
            Exams = exams;
            Patient = patient;
            Result = result;
            LabDate = Convert.ToDateTime(labDate);
            Place = place;
        }
        public Laboratory(int orderId)
        {
            Id = orderId;

        }
        public Laboratory()
        {
        }
    }
}
