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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var password = PasswordTextBox.Password;

            try
            {
                var user = UserRepository.GetInstance()
                                         .Login(email, password);
                LocalStateRepository.GetInstance().SetUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var page = new MainPage();
            LocalStateRepository.GetInstance().Frame?.Navigate(page);
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var page = new RegistrationPage();
            if (LocalStateRepository.GetInstance().Frame == null)
            {
                MessageBox.Show("pizda");
                return;
            }
            LocalStateRepository.GetInstance().Frame?.Navigate(page);
        }
    }
}
