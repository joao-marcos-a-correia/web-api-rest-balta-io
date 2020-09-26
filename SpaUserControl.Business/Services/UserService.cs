using SpaUserControl.Common.Resources;
using SpaUserControl.Common.Validation;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Models;
using SpaUserControl.Domain.Services;
using System;

namespace SpaUserControl.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public User GetByEmail(string email)
        {
            var user = _userRepository.Get(email);

            return user;
        }
        public void Register(string name, string email, string password, string confirmPassword)
        {
            var hasUser = GetByEmail(email);
            if (hasUser != null)
                throw new Exception(Errors.DuplicateEmail);

            var user = new User(name, email);
            user.SetPassword(password, confirmPassword);
            user.Validate();

            _userRepository.Create(user);
        }
        public User Authenticate(string email, string password)
        {
            var user = GetByEmail(email);

            if (user.Password != PasswordAssertionConcern.Encrypt(password))
                throw new Exception(Errors.InvalidCredentials);

            return user;
        }
        public void ChangeInformation(string email, string name)
        {
            var user = GetByEmail(email);

            user.ChangeName(name);
            user.Validate();

            _userRepository.Update(user);
        }
        public void ChangePassword(string email, string password, string newPassword, string confirmNewPassword)
        {
            var user = Authenticate(email, password);

            user.SetPassword(newPassword, confirmNewPassword);
            user.Validate();

            _userRepository.Update(user);
        }
        public string ResetPassword(string email)
        {
            var user = GetByEmail(email);
            var password = user.ResetPassword();
            user.Validate();

            _userRepository.Update(user);
            return password;
        }
        public void Dispose()
        {
            _userRepository.Dispose();
        }
    }
}
