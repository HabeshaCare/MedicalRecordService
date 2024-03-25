using System.Threading.Tasks;
using patientBackend1.DTOs;
using patientBackend1.DTOs.MedDTOs;
using patientBackend1.Models;

namespace patientBackend1.Services.MedicalServices
{
    public interface IMedRecServices
    {
        Task <MedicalRecord>CreateMedicalRecord(MedRecDTO medRecDTO);
        Task<MedicalRecord?> GetMedicalRecord(string medRecId );
        Task<MedRecDTO?> UpdateMedicalRecord(MedRecDTO model, string medicalRecordId);
         Task<bool> AddPrescription(string medicalRecordId, Prescriptions prescription);
        // Include other methods relevant to medical record operations
    }
}