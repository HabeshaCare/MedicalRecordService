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
}
}



