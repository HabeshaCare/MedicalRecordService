using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PatientBackend1.Models;



namespace patientBackend1.Models
{
    public class Prescriptions
    {
        public string? Id { get; set; }
        public required string Diagnosis { get; set; }
        public required string MedicineName { get; set; }
        public required string DoctorId { get; set; }
        public required DateTime Date { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PrescriptionStatus  Status { get; set; } = PrescriptionStatus.Unsold;
        public enum StatusEnum
        {
            Sold,
            Unsold
        }
    }
}