using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOk.Models
{
    public class UserManager
    {
        public ICollection<IValidator<string>> PasswordValidators { get; }
        public ICollection<IValidator<string>> UsernameValidators { get; } 
        public IPasswordHasher PasswordHasher { get; }
        public IStorage<User> UserStorage { get; }

        public UserManager(ICollection<IValidator<string>> passwordValidators, ICollection<IValidator<string>> usernameValidators, 
            IPasswordHasher passwordHasher, IStorage<User> storage)
        {
            PasswordValidators = passwordValidators;
            UsernameValidators = usernameValidators;
            PasswordHasher = passwordHasher;
            UserStorage = storage;
        }

        public bool Authenticate(User user, string password)
        {
            user.PasswordHash = PasswordHasher.GetHash(password);
            return UserStorage.FindByLogin(user.Username, user.PasswordHash) != null;
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
            bool isValid = true;

            foreach (var validator in UsernameValidators)
            {
                if (!validator.IsValid(user.Username))
                {
                    isValid &= false;
                    errors.Add(validator.ErrorMessage);
                }
            }

            foreach (var validator in PasswordValidators)
            {
                if (!validator.IsValid(password))
                {
                    isValid &= false;
                    errors.Add(validator.ErrorMessage);
                }
            }

            return new QueryResult(isValid, errors);
        }
    }
}
