﻿using DontThinkJustDrink.Api.Models.Database;
using DontThinkJustDrink.Api.Models.RequestModels;
using DontThinkJustDrink.Api.Models.ResponseModels;
using System.Threading.Tasks;

namespace DontThinkJustDrink.Api.Managers.Interfaces
{
    public interface IUsersManager
    {
        Task<User> GetUser(string id);
        Task<User> GetUserByEmail(string email);
        Task<string> CreateUser(CreateUserRequest request);
        Task UpdateUser(string id, UpdateUserRequest request);
        Task<string> SignUpUser(SignUpRequest request);
        Task<LoginResponse> Authenticate(string email, string password);
    }
}