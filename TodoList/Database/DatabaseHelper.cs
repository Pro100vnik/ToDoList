using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodoList.Database
{
    public class DatabaseHelper
    {
        private LocalDbContext Database { get; set; }

        public DatabaseHelper()
        {
            InitializeDatabase();
        }

        public void InitializeDatabase()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var folder = "TodoList";
            var dbName = "localDb.mdf";
            var path = Path.Combine(documents, folder, dbName);

            var exists = File.Exists(path);
            if (!exists)
            {
                Directory.CreateDirectory(Path.Combine(documents, folder));
                var emptyDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbName);

                File.Copy(emptyDbPath, path);
            }

            Database = LocalDbContext.FactoryMethod(PrepareConnectionString(path));

            var todos = Database.Todos.ToList();
        }

        private string PrepareConnectionString(string path)
        {
            return $@"Data Source=.\SQLEXPRESS;
                          AttachDbFilename={path};
                          Integrated Security=True;
                          Connect Timeout=0;
                          User Instance=True";
        }

        public void CreateTodo(string description, DateTime datetime)
        {
            var todo = new Todo()
            {
                Description = description,
                Date = datetime
            };

            Database.Todos.Add(todo);
            Database.SaveChanges();
        }

        public void EditTodo(Todo todo)
        {
            var toEdit = Database.Todos.Single(t => t.Id == todo.Id);
            toEdit.Date = todo.Date;
            toEdit.Description = todo.Description;

            Database.SaveChanges();
        }

        public ObservableCollection<Todo> GetTodos()
        {
            return Database.Todos.Local;
        }

        public List<Todo> GetTodos(DateTime date)
        {
            return Database.Todos.Local.Where(t => t.Date.Date == date.Date).ToList();
        }

        public void DeleteTodo(int id)
        {
            try
            {
                var toRemove = GetTodos().SingleOrDefault(t => t.Id == id);
                if (toRemove != null)
                {
                    Database.Todos.Remove(toRemove);
                    Database.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //Dwa lub wiecej elementow o tym samym id
            }
        }
    }
}
