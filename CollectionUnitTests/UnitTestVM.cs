using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectionUI.Model;
using CollectionUI.ViewModel;
using CollectionUI;
using System;
using System.Globalization;

namespace CollectionUnitTests
{

    [TestClass]
    public class UnitTestVM
    {

        [TestMethod]
        public void ViewModelTest_1()
        {
            //arr
            VMTasksCollection myViewModel = new VMTasksCollection();
            VMTask testTask = new VMTask("test", DateTime.Now, DateTime.Now.AddDays(3), TaskPriority.Important, false);

            //act
            bool canExecute = myViewModel.AddTask.CanExecute(testTask);
            myViewModel.AddTask.Execute(testTask);
            bool canExecuteNull = myViewModel.AddTask.CanExecute(null);

            //assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void CreateTaskConverter_Test()
        {
            //arr
            CreateTaskConverter converter = new CreateTaskConverter();
            DateTime plannedDate = DateTime.Now.AddDays(3);

            //act
            MyTask task = (MyTask)converter.Convert(new object[] { "test", plannedDate, 1 }, typeof(object), null, CultureInfo.InvariantCulture);

            //Assert
            Assert.AreEqual(task.PlannedCompletionDate, plannedDate);

        }
    }
}
