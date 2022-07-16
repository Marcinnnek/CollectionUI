using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionUI.Model
{
    public enum TaskPriority : byte { LessImportant, Important, Critical }
    public class MyTask //DTO - obiekt do przechowywania danych, przenoszenia danych między warstwami
    {
        public MyTask(string description, DateTime createDate, DateTime plannedCompletionDate, TaskPriority priority, bool isCompleted=false)
        {
            Description = description;
            CreateDate = createDate;
            PlannedCompletionDate = plannedCompletionDate;
            Priority = priority;
            IsCompleted = isCompleted;
        }

        public string Description { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime PlannedCompletionDate { get; private set; }
        public TaskPriority Priority { get; private set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return Description + " pryiority: " + Priority.ToString() + ", data utworzenia: " + CreateDate.ToString() +
                ", planowany termin realizacji: " + PlannedCompletionDate.ToString() +
                ", czy zreazlizowane: " + (IsCompleted ? "tak" : "nie");
           
        }
        public void DebugTask()
        {
            Debug.Write(Description + " pryiority: " + Priority.ToString() + ", data utworzenia: " + CreateDate.ToString() +
                ", planowany termin realizacji: " + PlannedCompletionDate.ToString() +
                ", czy zreazlizowane: " + (IsCompleted ? "tak" : "nie"));
        }
    }
}
