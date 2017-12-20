using CodeOk.Configuration;
using CodeOk.Models;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeOk
{
    public partial class LoginForm : Form
    {
        //UserManager userManager = new UserManager(new List<IValidator<string>> {
        //        new NotEmpty("Пароль не может быть пустой строкой"),
        //        new AlphanumericOnly("В пароле допустимы только латинские буквы и цифры"),
        //        new RequireMinLength(5, "Минимальная длина пароля - 5 символов"),
        //        new RestrictMaxLength(20, "Максимальная длина пароля - 20 символов"),
        //        new RequireLowercase("Пароль должен содержать хотя бы одну заглавную букву"),
        //        new RequireUppercase("Пароль должен содержать хотя бы одну строчную букву"),
        //        new RequireNumeric("Пароль должен содержать хотя бы одну цифру")},
        //    new List<IValidator<string>> {
        //        new NotEmpty("Имя пользователя не может быть пустой строкой"),
        //        new AlphanumericOnly("В имени пользователя допустимы только латинские буквы и цифры"),
        //        new RequireMinLength(5, "Минимальная длина имени пользователя - 5 символов"),
        //        new RequireLowercase("Имя пользователя должно содержать хотя бы одну заглавную букву"),
        //        new RequireUppercase("Имя пользователя должно содержать хотя бы одну строчную букву")
        //    },
        //    new DefaultPasswordHasher(), new FileUserStorage(ConfigurationManager.AppSettings["UserStorage"])
        //    );

        UserManager userManager;

        public LoginForm()
        {
            InitializeComponent();

            IKernel ninjectKernel = new StandardKernel(new NinjectConfig());

            var storage = ninjectKernel.Get<IStorage<User>>(new ConstructorArgument("path", ConfigurationManager.AppSettings["UserStorage"]));
            var hasher = ninjectKernel.Get<IPasswordHasher>();
            var usernameValidators = ninjectKernel.Get<ICollection<IValidator<string>>>("PasswordValidators");
            var passwordValidators = ninjectKernel.Get<ICollection<IValidator<string>>>("UserValidators");

            userManager = new UserManager(passwordValidators, usernameValidators, hasher, storage);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBoxLogin.Text) || string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageBox.Show("Не введено имя пользователя или пароль!", "Ошибка аутентификации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!userManager.Authenticate(new User(textBoxLogin.Text), textBoxPassword.Text))
                MessageBox.Show("Неверно введено имя пользователя или пароль!", "Ошибка аутентификации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                textBoxLogin.Clear();
                textBoxPassword.Clear();
                MessageBox.Show("Вы успешно вошли в систему!", "Аутентификация пройдена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabelForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Для восстановления пароля обратитесь к администратору.", "Восстановление доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkLabelForgotPassword_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Для восстановления пароля обратитесь к администратору.", "Восстановление доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm register = new RegisterForm(userManager);
            register.ShowDialog();
        }
    }
}
