using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using patientBackend1.DTOs.patientDTOs;


namespace patientBackend1.Services.UserServices
{
    public interface IUserService
    {
        Task<(int, string?, UsageUserDTO?)> GetUserById(string id);
        Task<(int, string, UsageUserDTO?)> UpdateUser(UpdateUserDTO model, string userId);
        Task<(int, string, UsageUserDTO?)> UploadProfile(string userId, IFormFile? image);
    }
}