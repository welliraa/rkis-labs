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

namespace WPFLabs.Components
{
    /// <summary>
    /// Interaction logic for TaskBlock.xaml
    /// </summary>
    public partial class TaskBlock : UserControl
    {
        public TaskModel? Task;

        public TaskBlock()
        {
            InitializeComponent();
        }

        public void LoadData(TaskModel task)
        {
            this.Task = task;
            InitComponents();
        }
        
        private void InitComponents()
        {
            if (this.Task == null)
            {
                return;
            }
            TaskTitleTextBlock.Text = this.Task.Name;
            TaskTimeTextBlock.Text = this.Task.Time.ToShortTimeString();

            CompletedIcon.Visibility = Visibility.Collapsed;
            UncompletedIcon.Visibility = Visibility.Collapsed;

            if (this.Task.Completed)
            {
                CompletedIcon.Visibility = Visibility.Visible;
                TaskTitleTextBlock.TextDecorations = TextDecorations.Strikethrough;
                TaskTimeTextBlock.TextDecorations = TextDecorations.Strikethrough;
            }
            else
            {
                UncompletedIcon.Visibility = Visibility.Visible;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            TaskBlockBackground.Background = new SolidColorBrush(
                Color.FromRgb(237, 237, 237)
            );
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            TaskBlockBackground.Background = new SolidColorBrush(
                Color.FromRgb(255, 255, 255)
            );
        }
    }
}
