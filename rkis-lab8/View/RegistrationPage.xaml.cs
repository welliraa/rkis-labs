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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLabs.Repository;

namespace WPFLabs.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var name = UsernameTextBox.Text;
            var password = PasswordTextBox.Password;
            var confirmPassword = ConfirmPasswordTextBox.Password;

            try
            {
                var user = UserRepository.GetInstance()
                                         .Register(
                    new Entities.UserModel()
                    {
                        Id = 0,
                        Email = email,
                        Name = name,
                        Password = password
                    }, confirmPassword);

                LocalStateRepository.GetInstance().SetUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var page = new MainEmptyPage();
            LocalStateRepository.GetInstance().Frame?.Navigate(page);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var page = new LoginPage();
            LocalStateRepository.GetInstance().Frame?.Navigate(page);
        }
    }
}
