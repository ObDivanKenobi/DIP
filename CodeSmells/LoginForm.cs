using CodeSmells.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeSmells
{
    public partial class LoginForm : Form
    {
        UserManager userManager = new UserManager();

        public LoginForm()
        {
            InitializeComponent();
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
            RegisterForm register = new RegisterForm();
            register.ShowDialog();
        }
    }
}
