using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WPFLabs.Repository
{
    internal class UserRepository
    {
        private static UserRepository? UserRepositoryInstance;

        private List<UserModel> _users = new List<UserModel>();

        private UserCredentialsValidator _emailValidator = new UserCredentialsValidator(
            UserCredentialsValidator.CredentialsType.Email
        );
        private UserCredentialsValidator _passwordValidator = new UserCredentialsValidator(
            UserCredentialsValidator.CredentialsType.Password
        );
        private UserCredentialsValidator _nameValidator = new UserCredentialsValidator(
            UserCredentialsValidator.CredentialsType.Name
        );

        public static UserRepository GetInstance()
        {
            if (UserRepositoryInstance == null)
            {
                UserRepositoryInstance = new UserRepository();
            }

            return UserRepositoryInstance;
        }

        public UserModel? GetUserByUsername(string username)
        {
            return _users.Find((user) => user.Name == username);
        }

        public UserModel? GetUserByEmail(string email)
        {
            return _users.Find((user) => user.Email == email);
        }

        public UserModel Login(string email, string password)
        {
            var userResult = GetUserByEmail(email);

            if (userResult == null)
            {
                throw new Exception("Неверный e-mail или пароль!");
            }

            if (userResult.Password != password)
            {
                throw new Exception("Неверный e-mail или пароль!");
            }

            return userResult;
        }

        public UserModel Register(UserModel user, string passwordConfirmation)
        {
            if (user.Password != passwordConfirmation)
            {
                throw new Exception("Пароли не совпадают!");
            }

            if (!_emailValidator.IsValid(user.Email))
            {
                throw new Exception("Неверный формат Email!");
            }

            if (!_passwordValidator.IsValid(user.Password))
            {
                throw new Exception("Недопустимый пароль!");
            }

            if (!_nameValidator.IsValid(user.Name))
            {
                throw new Exception("Недопустимое имя!");
            }

            if (GetUserByEmail(user.Email) != null)
            {
                throw new Exception("Пользователь с таким e-mail уже существует!");
            }

            if (GetUserByUsername(user.Name) != null)
            {
                throw new Exception("Пользователь с таким именем уже существует!");
            }

            user.Id = _users.Count + 1;
            _users.Add(user);
            return user;
        }
    }
}
