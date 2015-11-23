﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebService.DataContracts
{
    [DataContract]
    [Serializable]
    public class ValidUserContract
    {
        /// <summary>
        /// Creates a new ValidUserContract
        /// </summary>
        /// <param name="userType">The type of user.</param>
        public ValidUserContract(UserType userType)
        {
            // No need to make a Guid for an invalid user.
            if(userType != UserType.INVALID)
            {
                GUID = Guid.NewGuid();
            }

            UserType = userType;
        }

        /// <summary>
        /// The GUID to be used when recommunicating with the web service.
        /// </summary>
        [DataMember]
        public Guid GUID { get; private set; }

        /// <summary>
        /// The type of user.
        /// </summary>
        [DataMember]
        public UserType UserType { get; set; }
    }
}