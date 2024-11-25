using SimpleAuthApp.Utility.DTOs;
using System.ComponentModel.DataAnnotations;

namespace SimpleAuthApp.Services.Validators
{
    public class UserValidator
    {
        public string? ValidateRegistration(UserRequest user)
        {
            // Validate username
            string username = user.Username;
            string password = user.Password;
            string email = user.Email;

            if (string.IsNullOrWhiteSpace(username))
                return "Username is required.";

            if (username.Length < 3 || username.Length > 20)
                return "Username must be between 3 and 20 characters.";

            // Validate email
            var emailValidation = new EmailAddressAttribute();
            if (!emailValidation.IsValid(email))
                return "Invalid email address.";

            // Validate password
            if (string.IsNullOrWhiteSpace(password))
                return "Password is required.";

            if (password.Length < 6)
                return "Password must be at least 6 characters long.";

            // If all validations pass
            return null;
        }

        public string? ValidateLogin(string username, string password)
        {
            // Validate username
            if (string.IsNullOrWhiteSpace(username))
                return "Username is required.";

            // Validate password
            if (string.IsNullOrWhiteSpace(password))
                return "Password is required.";

            // If all validations pass
            return null;
        }
    }
}
