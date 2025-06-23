using Entities;
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
using WPFLabs.Repository;

namespace WPFLabs
{
    /// <summary>
    /// Логика взаимодействия для TaskCreationWindow.xaml
    /// </summary>
    public partial class TaskCreationWindow : Window
    {
        public TaskCreationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = LocalStateRepository.GetInstance().GetCurrentUser();

            if (currentUser == null)
            {
                return;
            }

            var selectedDate = TaskDatePicker.SelectedDate;

            if (selectedDate == null)
            {
                MessageBox.Show("Не выбрана дата!");
                return;
            }

            try
            {
                currentUser.Tasks.Add(new TaskModel()
                {
                    Id = currentUser.Tasks.Count() + 1,
                    Category = CategoryTextBox.Text,
                    Name = NameTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    Date = DateOnly.FromDateTime((DateTime)selectedDate),
                    Time = TimeOnly.Parse(TaskTimeTextBox.Text)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
