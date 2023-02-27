using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class GenericResponse<T>
    {
        public string Message { get; set; }
        public List<T> DataList { get; set; }
        public T ObjectResponse { get; set; }


        public GenericResponse(string message)
        {
            Message = message;
        }

        public GenericResponse(List<T> list)
        {
            DataList = list;
        }

        public GenericResponse(T obj)
        {
            ObjectResponse = obj;
        }
    }
}
