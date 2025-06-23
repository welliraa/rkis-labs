using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFLabs
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private UserCredentialsValidator _emailValidator = new UserCredentialsValidator(
            UserCredentialsValidator.CredentialsType.Email
        );
        private UserCredentialsValidator _passwordValidator = new UserCredentialsValidator(
            UserCredentialsValidator.CredentialsType.Password
        );
        private UserCredentialsValidator _nameValidator = new UserCredentialsValidator(
            UserCredentialsValidator.CredentialsType.Name
        );

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var name = UsernameTextBox.Text;
            var password = PasswordTextBox.Password;
            var confirmPassword = ConfirmPasswordTextBox.Password;

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_emailValidator.IsValid(email))
            {
                MessageBox.Show("Неверный формат Email!", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_passwordValidator.IsValid(password))
            {
                MessageBox.Show("Недопустимый пароль!", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_nameValidator.IsValid(name))
            {
                MessageBox.Show("Недопустимое имя!", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            new MainEmptyWindow().Show();
            Close();
        }
    }
}
