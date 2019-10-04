using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using projectmanager.Service.Models;
using System.Web.Http;
using projectmanager.DAL;
using Moq;
using NUnit.Framework;
using projectmanager.Service.Controllers;
using System;
using System.Threading.Tasks;
using projectmanager.Service.Tests.Utils;
using NBench;
using AutoMapper;

namespace projectmanager.Service.Tests.Repository
{
    public class TasksBaseTest
    {
        protected TasksController TasksController;
	    protected Mock<IContext> MockContext;
	    protected Mock<DbSet<Tasks>> MockSet;
	    protected Mock<DbSet<ParentTask>> MockSetParenttask;
        protected Counter _counter;
        protected TasksModel taskObject;
        protected ParentTaskModel parent;

        public TasksBaseTest()
        {
            MockSet = new Mock<DbSet<Tasks>>();
            MockSetParenttask = new Mock<DbSet<ParentTask>>();
            MockContext = new Mock<IContext>();

            TasksController = new TasksController(MockContext.Object);

        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
            var parenttaskparam = new ParentTask
            {
                Parent_ID = 1,
                Parent_Task = "Task first testing"
            };
            var userparam = new Users
            {
                User_ID = 1,
                FirstName = "Rahul",
                LastName ="Thai",
                Employee_ID ="359696"

            };
            var tasks = new List<Tasks>() {
            new Tasks() {  Task_ID = 1,
                    Task ="Task first testing",
                    Start_Date =DateTime.Parse("09-08-2015"),
                    End_Date =DateTime.Parse("09-13-2015"),
                    Priority ="13",
                    Project_ID=4,
                    Status = true,
                    TaskStatus= null,
                    User_ID = 1,
                    ParentTask= new ParentTask{ Parent_ID = 2, Parent_Task ="Task First testing" }
            },
            new Tasks() {  Task_ID = 4,
                    Task ="Task for cleanup and testing",
                    Start_Date =DateTime.Parse("05-06-2016"),
                    End_Date =DateTime.Parse("05-22-2016"),
                    Priority ="7",
                    Project_ID=2,
                    Status = true,
                    TaskStatus = "Completed",
                    User_ID = 3,
                    ParentTask = new ParentTask()
                }
            };
            var queryable = tasks.AsQueryable();

            taskObject = new TasksModel();

            MockSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(queryable.Expression);
            MockSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            MockSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

            MockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(queryable.Provider);
            MockSet.As<IEnumerable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Tasks>(queryable.Provider));
            MockSet.As<IDbAsyncEnumerable<Tasks>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Tasks>(queryable.GetEnumerator()));
                
            MockContext.Setup(c => c.SaveChangesAsync()).Returns(() => Task.Run(() => { return 1; })).Verifiable();

            MockSet.Setup(m => m.Add(It.IsAny<Tasks>())).Callback((Tasks task) => tasks.Add(task));
            MockSet.Setup(m => m.Remove(It.IsAny<Tasks>())).Callback((Tasks task) => tasks.Remove(task));
            MockSet.Setup(m => m.Include("ParentTask")).Returns(MockSet.Object);
            MockSet.Setup(m => m.Include("Users")).Returns(MockSet.Object);
            MockSet.Setup(m => m.Include("Projects")).Returns(MockSet.Object);
            MockContext.Setup(m => m.Tasks).Returns(MockSet.Object);
            setupParenttask();
        }
        [SetUp]
        public void Setup()
        {
            var parenttaskparam = new ParentTask
            {
                Parent_ID = 1,
                Parent_Task = "Task first testing"
            };
            var tasks = new List<Tasks>() {
            new Tasks() {  Task_ID = 1,
                    Task ="Task first testing",
                    Start_Date =DateTime.Parse("09-08-2015"),
                    End_Date =DateTime.Parse("09-13-2015"),
                    Priority ="13",
                    Project_ID=4,
                    Status = true,
                    TaskStatus= null,
                    User_ID = 1,
                    ParentTask= new ParentTask{ Parent_ID = 2, Parent_Task ="Task First testing" }
            },
            new Tasks() {  Task_ID = 4,
                    Task ="Task for cleanup and testing",
                    Start_Date =DateTime.Parse("05-06-2016"),
                    End_Date =DateTime.Parse("05-22-2016"),
                    Priority ="7",
                    Project_ID=2,
                    Status = true,
                    TaskStatus = "Completed",
                    User_ID = 3,
                    ParentTask= new ParentTask()
            }
            };
            var queryable = tasks.AsQueryable();

            taskObject = new TasksModel();

            MockSet.As<IQueryable<Tasks>>().Setup(m => m.Expression).Returns(queryable.Expression);
	        MockSet.As<IQueryable<Tasks>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
	        MockSet.As<IQueryable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

	        MockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(queryable.Provider);
	        MockSet.As<IEnumerable<Tasks>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSet.As<IQueryable<Tasks>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Tasks>(queryable.Provider));
            MockSet.As<IDbAsyncEnumerable<Tasks>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Tasks>(queryable.GetEnumerator()));
            MockContext.Setup(c => c.SaveChangesAsync()).Returns(() => Task.Run(() => { return 1; })).Verifiable();
            MockSet.Setup(m => m.Include("ParentTask")).Returns(MockSet.Object);
            MockSet.Setup(m => m.Include("Users")).Returns(MockSet.Object);
            MockSet.Setup(m => m.Include("Projects")).Returns(MockSet.Object);

            MockSet.Setup(m => m.Add(It.IsAny<Tasks>())).Callback((Tasks task) => tasks.Add(task));
	        MockSet.Setup(m => m.Remove(It.IsAny<Tasks>())).Callback((Tasks task) => tasks.Remove(task));

           
	        MockContext.Setup(m => m.Tasks).Returns(MockSet.Object);
            setupParenttask();

            //TasksController.dbContext = MockContext.Object;
        }

        public void setupParenttask() {

            var parenttask = new List<ParentTask>() {
            new ParentTask() {  Parent_ID = 1,
                    Parent_Task ="Task first testing"
            },
            new ParentTask() {  Parent_ID = 4,
                    Parent_Task ="Task for cleanup and testing"
            }
            };
            var queryable = parenttask.AsQueryable();

            parent = new ParentTaskModel();

            MockSetParenttask.As<IQueryable<ParentTask>>().Setup(m => m.Expression).Returns(queryable.Expression);
            MockSetParenttask.As<IQueryable<ParentTask>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            MockSetParenttask.As<IQueryable<ParentTask>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

            MockSetParenttask.As<IQueryable<ParentTask>>().Setup(m => m.Provider).Returns(queryable.Provider);
            MockSetParenttask.As<IEnumerable<ParentTask>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSetParenttask.As<IQueryable<ParentTask>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<ParentTask>(queryable.Provider));
            MockSetParenttask.As<IDbAsyncEnumerable<ParentTask>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<ParentTask>(queryable.GetEnumerator()));
            MockContext.Setup(c => c.SaveChangesAsync()).Returns(() => Task.Run(() => { return 1; })).Verifiable();

            MockSetParenttask.Setup(m => m.Add(It.IsAny<ParentTask>())).Callback((ParentTask parent) => parenttask.Add(parent));
            MockSetParenttask.Setup(m => m.Remove(It.IsAny<ParentTask>())).Callback((ParentTask parent) => parenttask.Remove(parent));


            MockContext.Setup(m => m.ParentTask).Returns(MockSetParenttask.Object);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Mapper.Reset();
        }

        [PerfCleanup]
        public void Cleanup()
        {
            // Not implemeted
        }
    }
}
