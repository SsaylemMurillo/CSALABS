using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Person
    {
        public string Name { get; set; }
        public string LastNames { get; set; }
        public int Id { get; set; }

        public Person()
        {
            this.Name = "";
            this.LastNames = "";
            this.Id = 0;
        }
        public Person(string names, string lastNames, int id)
        {
            this.Name = names;
            this.LastNames = lastNames;
            this.Id = id;
        }
        public override string ToString()
        {
            return this.Id + ";" + this.Name + ";" + this.LastNames;
        }
    }
}
