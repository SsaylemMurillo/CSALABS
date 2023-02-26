using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Laboratory
    {
        public int OrderId { get; set; }
        public List<Exam> Exams { get; set; }
        public Patient Patient { get; set; }
        public String Date { get; set; }
        public String Result { get; set; }
        public String Campus { get; set; }

        public Laboratory(List<Exam> exams, Patient patient, string result, string date, string campus)
        {
            this.Exams = exams;
            this.Patient = patient;
            Result = result;
            Date = date;
            Campus = campus;
        }
        public Laboratory(List<Exam> exams, Patient pacient, string result, string date, string campus, int orderId)
        {
            this.Exams = exams;
            this.Patient = pacient;
            Result = result;
            Date = date;
            Campus = campus;
            this.OrderId = orderId;
        }
        public Laboratory(int orderId)
        {
            this.OrderId = orderId;

        }
        public Laboratory()
        {
        }

    }
}
