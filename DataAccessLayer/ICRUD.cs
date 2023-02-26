using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
namespace DataAccessLayer
{
    public interface ICRUD<T> //pacientes, laboratorios, examenes
    {
        string Save(T obj);
        T Search(T obj);
        T Delete(T obj);
        string Update(T obj);
        List<T> GetAll();
    }
}