﻿using System.Collections.Generic;
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
using System.Globalization;

namespace projectmanager.Service.Tests.Repository
{
    public class ProjectBaseTest
    {
        protected ProjectController ProjectController;
	    protected Mock<IContext> MockContext;
	    protected Mock<DbSet<Projects>> MockSet;
        protected Counter _counter;
        protected ProjectsModel proj;

        public ProjectBaseTest()
        {
            MockSet = new Mock<DbSet<Projects>>();
            MockContext = new Mock<IContext>();

            ProjectController = new ProjectController(MockContext.Object);

        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");

            var userparam = new Users
            {
                User_ID = 1,
                FirstName = "Rahul",
                LastName = "Thai",
                Employee_ID = "359696"

            };

            var tasks = new List<Tasks>() {
               new Tasks() {  Task_ID = 1,
                    Task ="Task first testing",
                    Start_Date =DateTime.ParseExact("09-08-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    End_Date =DateTime.ParseExact("09-13-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="13",
                    Project_ID=1,
                    Status = true,
                    TaskStatus= null,
                    User_ID = 1,
                    ParentTask= new ParentTask{ Parent_ID = 2, Parent_Task ="Task First testing" }
            },
            new Tasks() {  Task_ID = 4,
                    Task ="Task for cleanup and testing",
                    Start_Date =DateTime.ParseExact("05-06-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    End_Date =DateTime.ParseExact("05-22-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="7",
                    Project_ID=1,
                    Status = true,
                    TaskStatus = "Completed",
                    User_ID = 3,
                    ParentTask = new ParentTask()
                }

            };

            var projects = new List<Projects>() {
            new Projects() {  Project_ID = 1,
                    Project ="Dotnet",
                    StartDate =DateTime.ParseExact("09-08-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    EndDate =DateTime.ParseExact("09-13-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="10",
                    Status = true,
                    User_ID = 1,
                    //Users = (ICollection<Users>)userparam,
                    Tasks = tasks
            },
            new Projects() {  Project_ID = 4,
                    Project ="Java",
                    StartDate =DateTime.ParseExact("05-06-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    EndDate =DateTime.ParseExact("05-22-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="10",
                    Status = true,
                    User_ID = 1,
                    //Users = (ICollection<Users>)userparam,
                    Tasks = tasks
            }
            };
            var queryable = projects.AsQueryable();

            proj = new ProjectsModel();

            MockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(queryable.Expression);
            MockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            MockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

            MockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(queryable.Provider);
            MockSet.As<IEnumerable<Projects>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Projects>(queryable.Provider));
            MockSet.As<IDbAsyncEnumerable<Projects>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Projects>(queryable.GetEnumerator()));
            MockContext.Setup(c => c.SaveChangesAsync()).Returns(() => Task.Run(() => { return 1; })).Verifiable();

            MockSet.Setup(m => m.Add(It.IsAny<Projects>())).Callback((Projects project) => projects.Add(project));
            MockSet.Setup(m => m.Remove(It.IsAny<Projects>())).Callback((Projects project) => projects.Remove(project));
            MockSet.Setup(m => m.Include("Tasks")).Returns(MockSet.Object);
            MockSet.Setup(m => m.Include("Users")).Returns(MockSet.Object);


            MockContext.Setup(m => m.Projects).Returns(MockSet.Object);
        }
        [SetUp]
        public void Setup()
        {

            var userparam = new Users
            {
                User_ID = 1,
                FirstName = "Rahul",
                LastName = "Thai",
                Employee_ID = "359696"

            };

            var tasks = new List<Tasks>() {
               new Tasks() {  Task_ID = 1,
                    Task ="Task first testing",
                    Start_Date =DateTime.ParseExact("09-08-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    End_Date =DateTime.ParseExact("09-13-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="13",
                    Project_ID=1,
                    Status = true,
                    TaskStatus= null,
                    User_ID = 1,
                    ParentTask= new ParentTask{ Parent_ID = 2, Parent_Task ="Task First testing" }
            },
            new Tasks() {  Task_ID = 4,
                    Task ="Task for cleanup and testing",
                    Start_Date =DateTime.ParseExact("05-06-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    End_Date =DateTime.ParseExact("05-22-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="7",
                    Project_ID=1,
                    Status = true,
                    TaskStatus = "Completed",
                    User_ID = 3,
                    ParentTask = new ParentTask()
                }

            };

            var projects = new List<Projects>() {
            new Projects() {  Project_ID = 1,
                    Project ="Dotnet",
                    StartDate =DateTime.ParseExact("09-08-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    EndDate =DateTime.ParseExact("09-13-2015","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="10",
                    Status = true,
                    User_ID = 1,
                    //Users = (ICollection<Users>)userparam,
                    Tasks = tasks
            },
            new Projects() {  Project_ID = 4,
                    Project ="Java",
                    StartDate =DateTime.ParseExact("05-06-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    EndDate =DateTime.ParseExact("05-22-2016","MM-dd-yyyy",CultureInfo.InvariantCulture),
                    Priority ="10",
                    Status = true,
                    User_ID = 1,
                    //Users = (ICollection<Users>)userparam,
                    Tasks = tasks
            }
            };
            var queryable = projects.AsQueryable();

            proj = new ProjectsModel();

            MockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(queryable.Expression);
	        MockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
	        MockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

	        MockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(queryable.Provider);
	        MockSet.As<IEnumerable<Projects>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Projects>(queryable.Provider));
            MockSet.As<IDbAsyncEnumerable<Projects>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Projects>(queryable.GetEnumerator()));
            MockContext.Setup(c => c.SaveChangesAsync()).Returns(() => Task.Run(() => { return 1; })).Verifiable();

            MockSet.Setup(m => m.Add(It.IsAny<Projects>())).Callback((Projects project) => projects.Add(project));
	        MockSet.Setup(m => m.Remove(It.IsAny<Projects>())).Callback((Projects project) => projects.Remove(project));
            MockSet.Setup(m => m.Include("Tasks")).Returns(MockSet.Object);
            MockSet.Setup(m => m.Include("Users")).Returns(MockSet.Object);


            MockContext.Setup(m => m.Projects).Returns(MockSet.Object);

			
            //ProjectController.dbContext = MockContext.Object;
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
