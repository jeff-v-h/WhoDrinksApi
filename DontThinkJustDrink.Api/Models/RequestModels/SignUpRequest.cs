﻿namespace DontThinkJustDrink.Api.Models.RequestModels
{
    public class SignUp
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}