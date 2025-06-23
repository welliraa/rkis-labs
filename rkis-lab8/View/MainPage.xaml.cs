using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace WPFLabs.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private enum CategoryType
        {
            Tasks, History
        };
        private CategoryType Category = CategoryType.History;


        public MainPage()
        {
            var stateRepo = LocalStateRepository.GetInstance();
            
            
           // var testUser = UserRepository.GetInstance().GetUserByEmail("user@mail.com");
           // if (testUser == null)
           // {
           //     testUser = UserRepository.GetInstance().Register(
           //         new UserModel()
           //         {
           //             Id = 1,
           //             Email = "user@mail.com",
           //             Password = "123456",
           //             Name = "Alex"
           //         },
           //         "123456"
           //     );
           // }
           // 
           // 
           // stateRepo.SetUser(testUser);

            

            if (!stateRepo.IsAuthorized())
            {
                var page = new LoginPage();
                LocalStateRepository.GetInstance().Frame?.Navigate(page);
                return;
            }

            var currentUser = stateRepo.GetCurrentUser();

            if (currentUser == null)
            {
                // wtf??
                return;
            }

            if (currentUser.Tasks.Count == 0)
            {
                var page = new MainEmptyPage();
                LocalStateRepository.GetInstance().Frame?.Navigate(page);
                return;
            }

            InitializeComponent();

            stateRepo.AddCategory("Дом");
            stateRepo.AddCategory("Работа");
            stateRepo.AddCategory("Учёба");
            stateRepo.AddCategory("Отдых");


            UserNameTextBlock.Text = currentUser.Name;

            InitCategories(stateRepo.GetCategories());
            InitTasks(stateRepo.GetTasks());

            stateRepo.TasksChanged += StateRepo_TasksChanged;
        }

        private void StateRepo_TasksChanged()
        {
            List<TaskModel> newTasks = LocalStateRepository.GetInstance().GetTasks();

            if (Category == CategoryType.History)
            {
                newTasks = LocalStateRepository.GetInstance().GetCompletedTasks();
            }

            InitTasks(newTasks);
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
                try
                {
                    categoryTextBlock.Foreground = new SolidColorBrush(roundRobinColors[categoryIndex % categories.Count]);
                }
                catch
                {

                }

                CategoriesStackPanel.Children.Add(categoryTextBlock);
            }
        }

        private void TasksTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Category = CategoryType.Tasks;
            StateRepo_TasksChanged();
        }

        private void HistoryTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Category = CategoryType.History;
            StateRepo_TasksChanged();
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

            Category = CategoryType.Tasks;
            StateRepo_TasksChanged();
        }
    }
}
