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
using TodoList.Database;

namespace TodoList.Windows
{
    public partial class AddEditTaskWindow : Window
    {
        private const string DefaultTaskDescription = "New Todo description...";
        private const string CreateTaskButtonText = "Create task";
        private const string EditTaskButtonText = "Edit task";

        public event Action<DateTime, string> TodoCreated;
        public event Action<Todo> TodoEdited;

        public AddEditTaskWindow(Todo todo = null)
        {
            InitializeComponent();

            if (todo == null)
            {
                this.Title = CreateTaskButtonText;

                TaskDatePicker.SelectedDate = DateTime.Now;
                DescriptionTb.Text = DefaultTaskDescription;
                AddEditTaskButton.Content = CreateTaskButtonText;
                AddEditTaskButton.Click += (sender, args) =>
                {
                    if (Validate())
                    {
                        TodoCreated?.Invoke(TaskDatePicker.SelectedDate.Value, DescriptionTb.Text);
                        Close();
                    }
                };
            }
            else
            {
                this.Title = EditTaskButtonText;

                TaskDatePicker.SelectedDate = todo.Date;
                DescriptionTb.Text = todo.Description;
                AddEditTaskButton.Content = EditTaskButtonText;
                AddEditTaskButton.Click += (sender, args) =>
                {
                    if (Validate())
                    {
                        todo.Date = TaskDatePicker.SelectedDate.Value;
                        todo.Description = DescriptionTb.Text;
                        TodoEdited?.Invoke(todo);
                        Close();
                    }
                };
            }
        }

        private bool Validate()
        {
            var dateCondition = TaskDatePicker.SelectedDate.HasValue && TaskDatePicker.SelectedDate.Value.Date >= DateTime.Now.Date;
            var descriptionCondition = !string.IsNullOrEmpty(DescriptionTb.Text) && !string.IsNullOrWhiteSpace(DescriptionTb.Text);

            var errorMessage = "";

            if (!dateCondition)
            {
                errorMessage += "Date is incorrect! ";
            }
            if (!descriptionCondition)
            {
                errorMessage += "Description cannot be empty! ";
            }

            if (string.IsNullOrEmpty(errorMessage))
            {
                return true;
            }

            MessageBox.Show(this, errorMessage, "Error", MessageBoxButton.OK);
            return false;
        }
    }
}
