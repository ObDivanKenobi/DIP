using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using CodeOk.Models;

namespace CodeOk.Configuration
{
    class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IPasswordHasher>().To<DefaultPasswordHasher>();
            Bind<IStorage<User>>().To<FileUserStorage>();
            Bind<ICollection<IValidator<string>>>().ToConstant(
                new List<IValidator<string>> {
                new NotEmpty("Пароль не может быть пустой строкой"),
                new AlphanumericOnly("В пароле допустимы только латинские буквы и цифры"),
                new RequireMinLength(5, "Минимальная длина пароля - 5 символов"),
                new RestrictMaxLength(20, "Максимальная длина пароля - 20 символов"),
                new RequireLowercase("Пароль должен содержать хотя бы одну заглавную букву"),
                new RequireUppercase("Пароль должен содержать хотя бы одну строчную букву"),
                new RequireNumeric("Пароль должен содержать хотя бы одну цифру")})
                .Named("PasswordValidators");
            Bind<ICollection<IValidator<string>>>().ToConstant(
                new List<IValidator<string>> {
                new NotEmpty("Имя пользователя не может быть пустой строкой"),
                new AlphanumericOnly("В имени пользователя допустимы только латинские буквы и цифры"),
                new RequireMinLength(5, "Минимальная длина имени пользователя - 5 символов"),
                new RequireLowercase("Имя пользователя должно содержать хотя бы одну заглавную букву"),
                new RequireUppercase("Имя пользователя должно содержать хотя бы одну строчную букву")
                })
                .Named("UserValidators");
            Bind<UserManager>().ToSelf();
        }
    }
}
