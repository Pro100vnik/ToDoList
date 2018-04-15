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
using TodoList.Database;

namespace TodoList.Controls
{
    public partial class TodoControl : UserControl
    {
        public Todo Todo { get; }
        public event Action<Todo> DeleteEntryClicked;
        public event Action<Todo> EditEntryClicked;

        public TodoControl(Todo todo)
        {
            InitializeComponent();
            Todo = todo;

            DateTb.Text = todo.Date.Date.ToShortDateString();
            DescriptionTb.Text = todo.Description;

            EditEntryBtn.MouseDown += EditEntryBtn_MouseDown;
            DeleteEntryBtn.MouseDown += DeleteEntryBtn_MouseDown;
        }

        private void EditEntryBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditEntryClicked?.Invoke(Todo);
        }

        private void DeleteEntryBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteEntryClicked?.Invoke(Todo);
        }
    }
}
