using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Database
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Todo()
        {

        }

        public Todo(string description)
        {
            Description = description;
            Date = DateTime.Now;
        }

        public Todo(string description, DateTime date)
        {
            Description = description;
            Date = date;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
