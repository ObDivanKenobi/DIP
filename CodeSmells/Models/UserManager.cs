using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Models
{
    public class UserManager
    {
        Validator PasswordValidator { get; set; } = new Validator()
        {
            RequireLength = true,
            RequiredLength = 10,
            ErrorMessage = "Пароль должен состоять не менее чем из 10 символов, а так же состоять только из латинских букв и цифр."
        };
        Validator UsernameValidator { get; set; } = new Validator()
        {
            RequireUppercase = true,
            RequireLength = true,
            RequiredLength = 5,
            ErrorMessage = "Имя пользователя должно состоять не менее чем из 10 символов, состоять только из латинских букв и цифр, а так же включать буквы в верхнем регистре."
        };
        PasswordHasher PasswordHasher { get; set; } = new PasswordHasher();
        UserStorage UserStorage { get; set; } = new UserStorage(ConfigurationManager.AppSettings["UserStorage"]);

        public bool Authenticate(User user, string password)
        {
            if(!ValidateUser(user, password).Succeed)
                return false;

            return UserStorage.FindUserByLogin(user.Username, user.PasswordHash) != null;
        }

        public QueryResult AddUser(User user, string password)
        {
            QueryResult validationResult = ValidateUser(user, password);
            if (!validationResult.Succeed)
                return validationResult;

            user.PasswordHash = PasswordHasher.GetHash(password);
            return UserStorage.Save(user);
        }
        
        QueryResult ValidateUser(User user, string password)
        {
            List<string> errors = new List<string>();
            bool isValid = UsernameValidator.IsValid(user.Username);
            if (!isValid)
                errors.Add(UsernameValidator.ErrorMessage);

            if (!PasswordValidator.IsValid(password))
            {
                isValid &= false;
                errors.Add(PasswordValidator.ErrorMessage);
            }

            return new QueryResult(isValid, errors);
        }
    }
}
