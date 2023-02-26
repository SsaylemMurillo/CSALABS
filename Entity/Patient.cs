using System;

namespace Entity
{
    public class Patient
    {
        public int Id { get; set; }
        public string IdType { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime ExpeditionDate { get; set; }
        public string ExpeditionPlace { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string Nacionality { get; set; }

        public Patient()
        {

        }

        public Patient(int id, string idType, string firstName, 
            string secondName, string lastName, string secondLastName,
            string bornDate, string expeditionDate, string expeditionPlace, 
            int phone, string address, string nacionality)
        {
            Id = id;
            IdType = idType;
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            SecondLastName = secondLastName;
            BornDate = Convert.ToDateTime(bornDate);
            ExpeditionDate = Convert.ToDateTime(expeditionDate);
            ExpeditionPlace = expeditionPlace;
            Phone = phone;
            Address = address;
            Nacionality = nacionality;
        }
    }
}
