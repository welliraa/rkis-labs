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
    /// Логика взаимодействия для MainEmptyWindow.xaml
    /// </summary>
    public partial class MainEmptyWindow : Window
    {
        public MainEmptyWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var taskCreation = new TaskCreationWindow();
            var result = taskCreation.ShowDialog();

            if (result == null)
            {
                return;
            }

            if (!(bool)result)
            {
                return;
            }

            Hide();
            new MainWindow().Show();
            Close();
        }
    }
}
