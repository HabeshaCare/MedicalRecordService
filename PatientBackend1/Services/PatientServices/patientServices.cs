using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using patientBackend1.Models;
using patientBackend1.DTOs.patientDTOs;
using patientBackend1.Services.FileServices;
using PatientBackend1.utils;
using PatientBackend1.Models;

using patientBackend1.Models.DTOs.UserDTOs;
using PatientBackend1.Services.PatientServices;
using System.Collections.ObjectModel;

namespace patientBackend1.Services.UserServices
{
    public class PatientServices :MongoDBService, IPatientService
    {
       IMongoCollection<Patient> _collection;
        private object p;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService; 

        public PatientServices(IOptions<MongoDBSettings> options, IFileService fileService, IMapper mapper):base(options){
            _collection = GetCollection<Patient>("Patients");
            _mapper = mapper;
            _fileService = fileService;
        }

  private async Task<(int,string?,Patient?)>GetPatient(string patientId){
    try{
        var result = await _collection.FindAsync(p=> p.Id == patientId);
        Patient patient = (await result.ToListAsync()).FirstOrDefault();
        return (1,null,patient);
    }
    catch (Exception ex)
            {
                return (0, ex.Message, null);
            }
  }
  public async Task<(int, string?, UsagePatientDTO?)> GetpatientrById(string patientId)
        {
            var (status, message, patient) = await GetPatient(patientId);
            if (status == 1 && patient != null)
            {
                return (status, message, _mapper.Map<UsagePatientDTO>(patient));
            }

            return (status, message, null);
        }


  public async Task<(int, string?,UsagePatientDTO[])> GetPatients (){
    try{
      var patientsFound = await _collection.Find(_ => true).ToListAsync();
      var dtoPatients=_mapper.Map<UsagePatientDTO[]>(patientsFound);
      return (1,"",dtoPatients);
      
    }catch(Exception e){
      return (0,e.Message,null);
    }    
  }   

  public async Task<(int, string, UsagePatientDTO?)> UpdatePatient(UsagePatientDTO model, string patientId)
        {
            var filter = Builders<Patient>.Filter.Eq(p => p.Id, patientId);
            
            try
            {
                if (model != null)
                {

                    
                 var (status, message, patient) = await GetPatient(patientId);

                    if (status == 0 || patient == null)
                        return (status, message ?? "User doesn't Exist", null);

                    _mapper.Map(model, patient);

                    var options = new FindOneAndReplaceOptions<Patient>
                    {
                        ReturnDocument = ReturnDocument.After
                    };

                    var rawUser = await _collection.FindOneAndReplaceAsync(filter, patient, options);

                    UsagePatientDTO updatedPatient = _mapper.Map<UsagePatientDTO>(rawUser);
                    return (1, "User updated Successfully", updatedPatient);
                }
                return (0, "Invalid Input", null);
            }
            catch (Exception ex)
            {
                return (1, ex.Message, null);
            }

        }
       public async Task<bool>UpdatePatientSubscriptionInSystemAsync(Patient patient)
{
    // Replace this with your actual asynchronous data access logic using MongoDBService
    try
    {
        var patientCollection = _collection.Find<Patient>("Patients"); // 
        

        // Find the existing patient document for update
        var filter = Builders<Patient>.Filter.Eq(p => p.Id, patient.Id);
        var existingPatient = await _collection.Find(filter).FirstOrDefaultAsync();

        if (existingPatient != null)
        {
            // Use AutoMapper to map patient data to a UsagePatientDTO
            var usagePatientDto = _mapper.Map<UsagePatientDTO>(patient);//patient to UsagePatientDTO

            

            // Update the existing patient document in the database (assuming UsagePatientDTO has relevant data)
            await _collection.ReplaceOneAsync(filter, patient);; // Update patient document (replace with more granular update if needed)
              
    }
        return true;
    }
    catch (Exception ex)
            {
                return false;
            }
  }



       
    }
}



