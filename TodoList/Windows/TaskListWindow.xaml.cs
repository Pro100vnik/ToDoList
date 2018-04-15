using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TodoList.Controls;
using TodoList.Database;
using TodoList.Extensions;

namespace TodoList.Windows
{
    public partial class TaskListWindow : Window
    {
        public TaskListWindow()
        {
            InitializeComponent();
            
            Conf.DatabaseHelper.InitializeDatabase();

            RefreshDatesCombobox();
            
            AddTodoBtn.MouseDown += AddTodoBtn_MouseDown;
        }

        private void RefreshDatesCombobox()
        {
            var days = GetTaskDays();
            DatesCb.ItemsSource = days;

            if (days != null || days.Count > 0)
            {
                GetTasksForDay(GetSelectedDay());
                DatesCb.SelectionChanged += DatesCb_SelectionChanged;
            }

            DatesCb.SelectedItem = days.FirstOrDefault();
        }

        private void DatesCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetTasksForDay(GetSelectedDay());
        }

        private DateTime? GetSelectedDay()
        {
            return DatesCb.SelectedItem as DateTime?;
        }

        private List<DateTime> GetTaskDays()
        {
            var todos = Conf.DatabaseHelper.GetTodos();

            var dates = new List<DateTime>();
            foreach (var todo in todos)
            {
                dates.Add(todo.Date);
            }

            var days = new List<DateTime>();
            days.AddUnique(dates);
            return days;
        }

        private void GetTasksForDay(DateTime? date)
        {
            EntitiesStackPanel.Children.Clear();

            if (date != null)
            {
                var todos = Conf.DatabaseHelper.GetTodos(date.Value);
                foreach (var todo in todos)
                {
                    var todoControl = new TodoControl(todo);
                    todoControl.EditEntryClicked += TodoControl_EditEntryClicked;
                    todoControl.DeleteEntryClicked += TodoControl_DeleteEntryClicked;
                    todoControl.Margin = new Thickness(10);

                    EntitiesStackPanel.Children.Add(todoControl);
                }
            }
        }

        private void TodoControl_EditEntryClicked(Todo todo)
        {
            var editWindow = new AddEditTaskWindow(todo);
            editWindow.TodoEdited += (Todo editedTodo) =>
            {
                Conf.DatabaseHelper.EditTodo(editedTodo);
                GetTasksForDay(GetSelectedDay());
                RefreshDatesCombobox();
            };
            editWindow.Show();
        }

        private void TodoControl_DeleteEntryClicked(Todo todo)
        {
            Conf.DatabaseHelper.DeleteTodo(todo.Id);
            GetTasksForDay(GetSelectedDay());
            RefreshDatesCombobox();
        }

        private void AddTodoBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var addWindow = new AddEditTaskWindow(null);
            addWindow.TodoCreated += (DateTime date, string desc) =>
            {
                Conf.DatabaseHelper.CreateTodo(desc, date);
                GetTasksForDay(GetSelectedDay());
                RefreshDatesCombobox();
            };
            addWindow.Show();
        }
    }
}
