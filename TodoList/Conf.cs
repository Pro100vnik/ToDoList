using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Database;

namespace TodoList
{
    public static class Conf
    {
        public static DatabaseHelper DatabaseHelper { get; } = new DatabaseHelper();
    }
}
