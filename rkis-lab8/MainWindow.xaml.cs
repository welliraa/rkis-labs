using Entities;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPFLabs.Components;
using WPFLabs.Repository;
using WPFLabs.View;

namespace WPFLabs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {      

        public event PropertyChangedEventHandler? PropertyChanged;

        private enum CategoryType 
        { 
            Tasks, History
        };
        private CategoryType Category = CategoryType.History;


        public MainWindow()
        {
            var stateRepo = LocalStateRepository.GetInstance();
            InitializeComponent();

            stateRepo.Frame = MainFrame;
            Loaded += MainWindow_Loaded;

            MainFrame.Navigating += MainFrame_Navigating;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.3);
            ta.DecelerationRatio = 0.7;
            ta.To = new Thickness(0, 0, 0, 0);

            if (e.NavigationMode == NavigationMode.New)
            {
                ta.From = new Thickness(500, 0, 0, 0);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(0, 0, 500, 0);
            }
            
            (e.Content as Page).BeginAnimation(MarginProperty, ta);
        }

    }
}