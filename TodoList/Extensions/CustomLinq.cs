using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Extensions
{
    public static class CustomLinq
    {
        public static void AddUnique<T>(this ICollection<T> self, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (!self.Contains(item))
                {
                    self.Add(item);
                }  
            }    
        }
    }
}
