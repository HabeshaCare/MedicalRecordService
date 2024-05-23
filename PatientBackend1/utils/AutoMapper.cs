using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using patientBackend1.DTOs.MedDTOs;
using patientBackend1.Models;
using patientBackend1.Models.DTOs.UserDTOs;
using PatientBackend1.Models;

namespace PatientBackend1.utils
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Used to check if a property is not null before trying to map.
            
            CreateMap<Patient, UsagePatientDTO>().ReverseMap();
            CreateMap<MedicalRecord, MedRecDTO>().ReverseMap();
            
            
            
        }
    }
}