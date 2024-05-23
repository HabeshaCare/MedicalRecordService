using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using patientBackend1.DTOs.patientDTOs;

namespace patientBackend1.Models.DTOs.UserDTOs
{
    public class UpdatePatientDTO : UpdateUserDTO
{
    public string? NationalId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public required string Location  { get; set; }
    public Package CurrentPackage { get; set; } = new Package();
}
}