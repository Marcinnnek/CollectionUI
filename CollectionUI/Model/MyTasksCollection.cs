using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollectionUI.Model
{
    public class MyTasksCollection :IEnumerable<MyTask> // IEnumerable implementowane po to aby można było skożystać z LINQ
    {
        private List<MyTask> tasks = new List<MyTask>();

        //CRUD
        public void AddTask(MyTask newTask)
        {
            tasks.Add(newTask);
        }
        public bool DeleteTask(MyTask task)
        {
            return tasks.Remove(task);
        }

        public int CountTask { get { return tasks.Count; } }

        public MyTask this[int index] //indekser - tylko do odczytu
        {
            get { return tasks[index]; }    
        }

        public IEnumerator<MyTask> GetEnumerator()
        {
            return tasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

      
    }
}
