using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace patientBackend1.Models
{
    public class MedicalRecord
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }

    public int Height { get; set; }
    public int Weight { get; set; }
    public string GeneralAppearance { get; set; }
    public string DoctorId { get; set; }
    public DateTime Date { get; set; }

    public List<MedicalReport> Reports { get; set; } = new List<MedicalReport>();  // Initialize an empty list
    public List<Prescriptions> Prescribed { get; set; } = new List<Prescriptions>();  // Initialize an empty list

    public List<LabTestResult> Tests { get; set; } = new List<LabTestResult>();  // Initialize an empty list
}

}
