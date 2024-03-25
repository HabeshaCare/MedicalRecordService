using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using patientBacken1.Models;

namespace patientBackend1.DTOs.patientDTOs
{
    public class UsageUserDTO : UserDTO
    {
       
        public string Profession { get; set; } = "";
        public string Fullname { get; set; } = "";
        public string Phonenumber { get; set; } = "";
        public string? City { get; set; }
        public int? Age { get; set; }
        public string? ImageUrl { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
    }
}