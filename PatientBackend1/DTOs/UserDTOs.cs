using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace patientBackend1.DTOs.patientDTOs
{
    public class UserDTO
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string? Gender { get; set; }
    }
}