using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionUI.ViewModel
{
    using CollectionUI;
    using CollectionUI.Model;
    using System.Windows.Input;

    public class VMTask : ObservedObject

    {
        private Model.MyTask singleTaskModel;

        #region Properties
        public string Description
        {
            get
            {
                return singleTaskModel.Description;
            }
        }

        public Model.TaskPriority Priority
        {
            get
            {
                return singleTaskModel.Priority;
            }

        }
        public DateTime CreateDate
        {
            get
            {
                return singleTaskModel.CreateDate;
            }
        }
        public DateTime PlannedCompletionDate
        {
            get
            {
                return singleTaskModel.PlannedCompletionDate;
            }
        }

        public bool IsCompleted 
        { 
            get 
            { 
                return singleTaskModel.IsCompleted; 
            }
        }

        public bool OnTimeCompleted
        {
            get
            {
                return !IsCompleted && (DateTime.Now > PlannedCompletionDate);
            }
        }

        #endregion
        public VMTask(Model.MyTask model)
        {
            this.singleTaskModel = model;
        }

        public VMTask(string description, DateTime createDate, DateTime plannedCompletionDate, Model.TaskPriority priority, bool isCompleted)
        {
            singleTaskModel = new Model.MyTask(description, createDate, plannedCompletionDate, priority, isCompleted);
        }

        public Model.MyTask GetSingleTaskModel()
        {
            return singleTaskModel;
        }

        #region Commands
        private ICommand markAsIsCompleted;
        public ICommand MarkAsIsCompleted
        {
            get
            {
                if (markAsIsCompleted == null)
                {
                    markAsIsCompleted = new RelayCommand(
                        (object o) =>
                        {
                            singleTaskModel.IsCompleted = true;
                            onPropertyChanged(nameof(IsCompleted), nameof(OnTimeCompleted));
                        },
                        (object o) => 
                        {
                            return !singleTaskModel.IsCompleted; 
                        });
                }
                return markAsIsCompleted;
            }
        }

        private ICommand markAsIsNotCompleted;
        public ICommand MarkAsIsNotCompleted
        {
            get
            {
                if (markAsIsNotCompleted == null)
                {
                    markAsIsNotCompleted = new RelayCommand(
                        (object o) =>
                        {
                            singleTaskModel.IsCompleted = false;
                            onPropertyChanged(nameof(IsCompleted), nameof(OnTimeCompleted));
                        },
                        (object o) =>
                        {
                            return singleTaskModel.IsCompleted;
                        });
                }
                return markAsIsNotCompleted;
            }
        }
        #endregion
        public override string ToString()
        {
            return singleTaskModel.ToString();
        }
    }
}
