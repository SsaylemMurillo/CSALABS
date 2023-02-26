using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ValuesMeasures { get; set; }

        public Exam(int id, string name, string description, string valuesMeasures)
        {
            Id = id;
            Name = name;
            Description = description;
            this.ValuesMeasures = valuesMeasures;
        }

        public Exam()
        {
        }
    }
}
