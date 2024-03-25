Pusing System;
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
    public class Prescriptions
    {
        public string id;
        public string diagnosis;
        public string medicineName;
        public string doctorId;
        public DateTime date;
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public StatusEnum status;
        public enum StatusEnum
        {
            Sold,
            Unsold
        }
    }
}