using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class GenericResponse
    {
        public string Message { get; set; }
        public List<object> DataList { get; set; }

        public List<Patient> PatientDataList { get; set; }

        public GenericResponse(string message)
        {
            Message = message;
        }

        public GenericResponse(List<object> list)
        {
            DataList = list;
        }

        public GenericResponse(List<Patient> patientList)
        {
            PatientDataList = patientList;
        }
    }
}
