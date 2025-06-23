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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLabs.Repository;

namespace WPFLabs.Components
{
    /// <summary>
    /// Interaction logic for TaskPreview.xaml
    /// </summary>
    public partial class TaskPreview : UserControl
    {
        public delegate void TaskDeletedDelegate();
        public event TaskDeletedDelegate? TaskDeleted;

        private TaskModel? task;

        public TaskPreview()
        {
            InitializeComponent();
        }

        public void LoadData(TaskModel task)
        {
            this.task = task;
            InitComponents();
        }

        private void InitComponents()
        {
            if (task == null)
            {
                return;
            }
            
            TitleTextBlock.Text = task.Name;
            TimeTextBlock.Text = task.Time.ToShortTimeString();
            DateTextBlock.Text = task.Date.ToShortDateString();
            DescriptionTextBlock.Text = task.Description;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var stateRepo = LocalStateRepository.GetInstance();
            var repoTask = stateRepo.GetTask(task.Id);
            repoTask.Completed = !task.Completed;
            stateRepo.UpdateTasks();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var stateRepo = LocalStateRepository.GetInstance();
            stateRepo.RemoveTask(task);
            TaskDeleted?.Invoke();
        }
    }
}
