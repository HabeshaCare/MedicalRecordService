using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using patientBackend1.DTOs;
using patientBackend1.DTOs.MedDTOs;
using patientBackend1.Models;
using PatientBackend1.utils;

namespace patientBackend1.Services.MedicalServices
{
    public class MedRecServices : MongoDBService, IMedRecServices
    {
        private readonly IMongoCollection<MedicalRecord> _collection;
        private readonly IMapper _mapper;


        public MedRecServices(IOptions<MongoDBSettings> options, IMapper mapper)
        : base(options)
        {
            _collection = GetCollection<MedicalRecord>("medical_records");
            _mapper = mapper;
        }

        public async Task<MedicalRecord> CreateMedicalRecord(MedRecDTO medRecDTO)
        {
            try
            {
                var medicalRecord = _mapper.Map<MedicalRecord>(medRecDTO);
                await _collection.InsertOneAsync(medicalRecord);
                return medicalRecord;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating medical record: {ex.Message}");
                throw;
            }
        }

        // Get the Medical records
        public async Task<MedicalRecord?> GetMedicalRecord(string medRecId)
        {
            try
            {
                var result = await _collection.FindAsync(M => M.id == medRecId);
                MedicalRecord medrec = (await result.ToListAsync()).FirstOrDefault();
                return medrec;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting medical record: {ex.Message}");
                return null;
            }
        }
        public async Task<MedRecDTO?> UpdateMedicalRecord(MedRecDTO model, string medicalRecordId)
        {
            var filter = Builders<MedicalRecord>.Filter.Eq(mr => mr.id, medicalRecordId);

            try
            {
                if (model != null)
                {
                    var medicalRecord = await GetMedicalRecord(medicalRecordId);

                    if (medicalRecord == null)
                        return null; // Indicate unsuccessful update (record not found)

                    // Map model properties to the retrieved MedicalRecord object
                    _mapper.Map(model, medicalRecord);

                    var options = new FindOneAndReplaceOptions<MedicalRecord>
                    {
                        ReturnDocument = ReturnDocument.After
                    };

                    var rawMedicalRecord = await _collection.FindOneAndReplaceAsync(filter, medicalRecord, options);

                    return _mapper.Map<MedRecDTO>(rawMedicalRecord);
                }

                return null; // Indicate unsuccessful update (invalid input)
            }
            catch (Exception ex)
            {
                // Consider logging the exception for debugging purposes
                return null;
            }

        }

        public async Task<bool> AddPrescription(string medicalRecordId, Prescriptions prescription)
        {
            try
            {
                var medicalRecord = await GetMedicalRecord(medicalRecordId);

                if (medicalRecord == null)
                    return false; // Indicate unsuccessful update (record not found)

                if (prescription == null)
                    return false; // Invalid prescription data

                // using the prescritions class directly w/o DTO
                medicalRecord.Prescribed.Add(prescription);

                // Update the entire medical record with the added prescription
                var updateResult = await _collection.ReplaceOneAsync(
                    filter: Builders<MedicalRecord>.Filter.Eq(mr => mr.id, medicalRecordId),
                    replacement: medicalRecord
                );

                return true; // Check if exactly one document was updated
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Consider logging the exception for debugging purposes
                return false;

            }
        }
    }
}






