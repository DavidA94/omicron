using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    [DataContract]
    [Serializable]
    public class AppDataContract
    {
        public AppDataContract(int id, string ssn, string firstname, string lastname, string phone, DateTime date_submitted, 
            bool changeable = false)
        {
            ID = id;
            SSN = ssn;
            FirstName = firstname;
            LastName = lastname;
            Phone = phone;
            DateSubmitted = date_submitted;
            Changeable = changeable;
        }

        [DataMember]
        public int ID { get; private set; }

        [DataMember]
        public string SSN { get; private set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public DateTime DateSubmitted { get; set; }

        [DataMember]
        public bool Changeable { get; private set; }
    }
}
