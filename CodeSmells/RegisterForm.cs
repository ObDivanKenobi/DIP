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
    public partial class RegisterForm : Form
    {
        UserManager userManager;

        public RegisterForm()
        {
            InitializeComponent();
            userManager = new UserManager();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string newLogin = textBoxLogin.Text,
                   newPassword = textBoxPassword.Text,
                   newPasswordRepeat = textBoxRepeatPassword.Text;

            if(newPassword != newPasswordRepeat)
            {
                MessageBox.Show("Введённые пароли не совпадают.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxRepeatPassword.Clear();
                return;
            }

            var addingResult = userManager.AddUser(new User(newLogin), newPassword);
            if (!addingResult.Succeed)
            {
                StringBuilder builder = new StringBuilder("Были обнаружены следующие ошибки:\n");
                foreach (string error in addingResult.Errors)
                    builder.AppendLine(error);

                MessageBox.Show(builder.ToString(), "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Вы успешно зарегистрированы. Используйте имя пользователя и пароль, чтобы войти в систему.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
    }
}
