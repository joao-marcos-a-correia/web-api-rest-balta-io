using SpaUserControl.Common.Validation;
using SpaUserControl.Common.Resources;
using System;

namespace SpaUserControl.Domain.Models
{
    public class User
    {
        #region Construtor
        protected User() { }
        public User(string name, string email)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Email = email;
        }
        #endregion

        #region Properties
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        #endregion

        #region Methods
        public void SetPassword(string password, string confirmPassword)
        {
            AssertionConcern.AssertArgumentNotNull(password, Errors.InvalidPassword);
            AssertionConcern.AssertArgumentNotNull(confirmPassword, Errors.InvalidPasswordConfirmation);
            AssertionConcern.AssertArgumentEquals(password, confirmPassword, Errors.PasswordNotMatch);
            AssertionConcern.AssertArgumentLength(password, 6, 20, Errors.InvalidPassword);

            this.Password = PasswordAssertionConcern.Encrypt(password);
        }

        public string ResetPassword()
        {
            string password = Guid.NewGuid().ToString().Substring(0, 8);
            this.Password = PasswordAssertionConcern.Encrypt(password);

            return password;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentLength(this.Name, 3, 250, Errors.InvalidUserName);
            EmailAssertionConcern.AssertIsValid(this.Email);
            PasswordAssertionConcern.AssertIsValid(this.Password);
        }
        #endregion
    }
}
