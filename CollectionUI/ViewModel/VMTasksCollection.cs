using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CollectionUI.ViewModel
{
    //using CollectionWPF_07.Model;
    public class VMTasksCollection
    {
        private Model.MyTasksCollection modelTasks;

        public ObservableCollection<VMTask> TaskList { get; } = new ObservableCollection<VMTask>();
        private void CopyTasksFromModel()
        {
            TaskList.CollectionChanged -= ModelSynchronisation; //Ważne, przy zmianie w kolekcji wywoływana jest podpięta metoda ModelSynchronisation
            TaskList.Clear();
            foreach (Model.MyTask task in modelTasks)
            {
                TaskList.Add(new VMTask(task));
            }
            TaskList.CollectionChanged += ModelSynchronisation; //Ważne
        }

        //private string filePatch = "zadania.xml";
        private string filePatch = "tasks.json";

        public VMTasksCollection()
        {
            if (System.IO.File.Exists(filePatch))
            {
                modelTasks = Model.FileJson.LoadFileWithTasks(filePatch);
            }
            else
            {
                modelTasks = new Model.MyTasksCollection();
            }

            //Test
            //modelTasks.AddTask(new Model.MyTask("Pierwsze", DateTime.Now, DateTime.Now.AddDays(3), Model.TaskPriority.Critical));
            //modelTasks.AddTask(new Model.MyTask("Drugie", DateTime.Now, DateTime.Now.AddDays(5), Model.TaskPriority.Important));
            //modelTasks.AddTask(new Model.MyTask("Trzecie", DateTime.Now, DateTime.Now.AddDays(7), Model.TaskPriority.LessImportant));

            CopyTasksFromModel();
        }

        private void ModelSynchronisation(object sender, NotifyCollectionChangedEventArgs e) //dane z ViewModel do Model czyli jeśli widok zmieni dane w view model to zostanie to przesłane do modelu!
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    VMTask newTask = (VMTask)e.NewItems[0];
                    if (newTask != null)
                    {
                        modelTasks.AddTask(newTask.GetSingleTaskModel());
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    VMTask deleteTask = (VMTask)e.OldItems[0];
                    if (deleteTask != null)
                    {
                        modelTasks.DeleteTask(deleteTask.GetSingleTaskModel());
                    }
                    break;
            }
        }
        public ICommand SaveTasksOnClose
        {
            get
            {
                return new RelayCommand(
                    (object o) => { Model.FileJson.SaveFileWithTasksXml(modelTasks, filePatch); },
                    (object o) => { return true; });
            }
        }

        private ICommand deleteTask;
        public ICommand DeleteTask
        {
            get
            {
                if (deleteTask == null)
                {
                    deleteTask = new RelayCommand(
                    (object o) =>
                    {
                        int taskIndex = (int)o;
                        VMTask task = TaskList[taskIndex];
                        TaskList.Remove(task);
                    },
                    (object o) =>
                    {
                        if (o == null)
                        {
                            return false;
                        }
                        int taskIndex = (int)o;
                        return taskIndex >= 0;
                    });
                }
                return deleteTask;
            }
        }

        private ICommand addTask;
        public ICommand AddTask
        {
            get
            {
                if (addTask == null)
                {
                    addTask = new RelayCommand(
                        (object o) =>
                        {
                            VMTask task = o as VMTask;
                            if (task != null)
                            {
                                TaskList.Add(task);
                            }
                        },
                        (object o) =>
                            {
                                Debug.WriteLine("Can Execute: " + ((o as VMTask) != null));
                                if ((o as VMTask) != null)
                                {
                                    Debug.WriteLine(o.ToString());
                                }
                                return (o as VMTask) != null;
                            }
                        );
                }
                return addTask;
            }
        }

    }
}
