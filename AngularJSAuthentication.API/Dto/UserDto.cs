﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Dto
{
    [DataContract]
    public class UserDto
    {
        public UserDto() { }

        public UserDto(string id, string username, IEnumerable<RoleDto> roles, string email, string firstName, string lastName, 
            string phoneNumber, Boolean isLocked)
        {
            Id = id;
            UserName = username;
            Roles = roles;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            IsLocked = isLocked;
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public IEnumerable<RoleDto> Roles { get; set; }

        [DataMember]
        public string Email { get; set; } 

        [DataMember]
        public Boolean IsLocked { get; set; }
    }
}