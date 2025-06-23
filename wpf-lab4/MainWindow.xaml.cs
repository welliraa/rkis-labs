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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLabs.Components;
using WPFLabs.Repository;

namespace WPFLabs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private UserModel testUser = UserRepository.GetInstance().Register(
                new UserModel()
                {
                    Id = 1,
                    Email = "user@mail.com",
                    Password = "123456",
                    Name = "Alex"
                },
                "123456"
            );

        public MainWindow()
        {
            var stateRepo = LocalStateRepository.GetInstance();

            stateRepo.SetUser(testUser);

            if (!stateRepo.IsAuthorized())
            {
                Hide();
                new LoginWindow().Show();
                Close();
                return;
            }

            InitializeComponent();

            stateRepo.AddCategory("Дом");
            stateRepo.AddCategory("Работа");
            stateRepo.AddCategory("Учёба");
            stateRepo.AddCategory("Отдых");

            var testTask1 = new TaskModel()
            {
                Id = 1,
                Category = "Дом",
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = new TimeOnly(9, 16),
                Description = "test task",
                Name = "Go fishing with Stephen",
                Completed = true,
            };

            var testTask2 = new TaskModel()
            {
                Id = 2,
                Category = "Дом",
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = new TimeOnly(10, 16),
                Description = "test task 22у12пацупцупцупцупцуп",
                Name = "Go touch grass",
                Completed = false,
            };

            stateRepo.AddTask(testTask1);
            stateRepo.AddTask(testTask2);
            stateRepo.AddTask(testTask2);
            stateRepo.AddTask(testTask2);
            stateRepo.AddTask(testTask2);
            stateRepo.AddTask(testTask2);
            stateRepo.AddTask(testTask2);
            stateRepo.AddTask(testTask2);

#pragma warning disable CS8602 // STFU
            string name = stateRepo.GetCurrentUser().Name;
#pragma warning restore CS8602

            UserNameTextBlock.Text = name;

            InitCategories(stateRepo.GetCategories());
            InitTasks(stateRepo.GetTasks());

            stateRepo.TasksChanged += StateRepo_TasksChanged;
        }

        private void StateRepo_TasksChanged()
        {
            InitTasks(LocalStateRepository.GetInstance().GetTasks());
        }

        private void InitTasks(List<TaskModel> tasks)
        {
            TasksStackPanel.Children.Clear();

            foreach (TaskModel task in tasks)
            {
                var taskBlock = new TaskBlock();
                taskBlock.LoadData(task);
                taskBlock.Margin = new Thickness(0, 10, 0, 10);
                taskBlock.Effect = new DropShadowEffect()
                {
                    ShadowDepth = 5,
                    BlurRadius = 10,
                    Opacity = 0.2,
                    Direction = -90
                };

                taskBlock.MouseDown += TaskBlock_MouseDown;

                TasksStackPanel.Children.Add(taskBlock);
            }
        }

        private void TaskBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var taskBlock = sender as TaskBlock;

            if (taskBlock == null)
            {
                return;
            }

            if (taskBlock.Task == null)
            {
                return;
            }

            var task = taskBlock.Task;

            var taskPreview = new TaskPreview();
            taskPreview.LoadData(task);
            taskPreview.Effect = new DropShadowEffect()
            {
                Direction = -90,
                Opacity = 0.3
            };

            TaskPreviewContainer.Children.Clear();
            TaskPreviewContainer.Children.Add(taskPreview);
            taskPreview.TaskDeleted += () =>
            {
                TaskPreviewContainer.Children.Clear();
            };
        }

        private void InitCategories(List<string> categories)
        {
            CategoriesStackPanel.Children.Clear();

            var roundRobinColors = new Color[]
            {
                Colors.Green,
                Colors.Orange,
                Colors.Blue,
                Colors.Magenta,
            };

            for (int categoryIndex = 0; categoryIndex < categories.Count; categoryIndex++)
            {
                var category = categories[categoryIndex];
                var categoryTextBlock = new TextBlock();
                categoryTextBlock.Text = category;
                categoryTextBlock.Margin = new Thickness(20, 0, 20, 0);
                categoryTextBlock.VerticalAlignment = VerticalAlignment.Center;
                categoryTextBlock.FontSize = 16;
                categoryTextBlock.Foreground = new SolidColorBrush(roundRobinColors[categoryIndex % categories.Count]);

                CategoriesStackPanel.Children.Add(categoryTextBlock);
            }
        }
    }
}