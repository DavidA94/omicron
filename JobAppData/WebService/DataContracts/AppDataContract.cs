using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace WebService.DataContracts
{
    [DataContract]
    [Serializable]
    public class AppDataContract
    {
        /// <summary>
        /// Creates a new AppDataContract
        /// </summary>
        /// <param name="id">The ID of the user tied to the application</param>
        /// <param name="ssn">The applicant's SSN</param>
        /// <param name="firstname">The applicant's first name</param>
        /// <param name="lastname">The applicant's last name</param>
        /// <param name="phone">The applicant's phone number</param>
        /// <param name="date_submitted">The applicant's submission date</param>
        /// <param name="changeable">Indicates whether or not the data can be sent back and be changed.</param>
        public AppDataContract(int id, string ssn, string firstname, string lastname, string phone, DateTime date_submitted, 
            bool changeable = false)
        {
            ID = id;
            SSN = ssn;
            FirstName = firstname;
            LastName = lastname;
            Phone = Regex.Replace(phone, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            DateSubmitted = date_submitted;
            Changeable = changeable;
        }

        /// <summary>
        /// The ID of the applicant.
        /// </summary>
        [DataMember]
        public int ID { get; private set; }

        /// <summary>
        /// The applicant's SSN
        /// </summary>
        [DataMember]
        public string SSN { get; private set; }

        /// <summary>
        /// The applicant's first name
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// The applicant's last name
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// The applicant's phone number
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// The applicant's submission date
        /// </summary>
        [DataMember]
        public DateTime DateSubmitted { get; private set; }

        /// <summary>
        /// Indicates whether or not the data can be sent back and be changed.
        /// </summary>
        [DataMember]
        public bool Changeable { get; private set; }
    }
}
