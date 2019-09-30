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
    public class UserBaseTest
    {
        protected UsersController UsersController;
	    protected Mock<IContext> MockContext;
	    protected Mock<DbSet<Users>> MockSet;
        protected Counter _counter;
        protected UsersModel proj;

        public UserBaseTest()
        {
            MockSet = new Mock<DbSet<Users>>();
            MockContext = new Mock<IContext>();

            UsersController = new UsersController(MockContext.Object);

        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");

            var users = new List<Users>() {
            new Users() {  User_ID = 1,
                    FirstName ="Rahul",
                    LastName ="Thai",
                    Employee_ID ="RT009"
            },
            new Users() {  User_ID = 2,
                     FirstName ="Rohit",
                    LastName ="Jain",
                    Employee_ID ="RJ3456"}
            };
            var queryable = users.AsQueryable();

            proj = new UsersModel();

            MockSet.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(queryable.Expression);
            MockSet.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            MockSet.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

            MockSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(queryable.Provider);
            MockSet.As<IEnumerable<Users>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Users>(queryable.Provider));
            MockSet.As<IDbAsyncEnumerable<Users>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Users>(queryable.GetEnumerator()));
            MockContext.Setup(c => c.SaveChangesAsync()).Returns(() => Task.Run(() => { return 1; })).Verifiable();

            MockSet.Setup(m => m.Add(It.IsAny<Users>())).Callback((Users project) => users.Add(project));
            MockSet.Setup(m => m.Remove(It.IsAny<Users>())).Callback((Users project) => users.Remove(project));


            MockContext.Setup(m => m.Users).Returns(MockSet.Object);
        }
        [SetUp]
        public void Setup()
        {

            var users = new List<Users>() {
            new Users() {  User_ID = 1,
                    FirstName ="Rahul",
                    LastName ="Thai",
                    Employee_ID ="RT009"
            },
            new Users() {  User_ID = 2,
                     FirstName ="Rohit",
                    LastName ="Jain",
                    Employee_ID ="RJ3456"}
            };
            var queryable = users.AsQueryable();

            proj = new UsersModel();

            MockSet.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(queryable.Expression);
	        MockSet.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
	        MockSet.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

	        MockSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(queryable.Provider);
	        MockSet.As<IEnumerable<Users>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Users>(queryable.Provider));
            MockSet.As<IDbAsyncEnumerable<Users>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Users>(queryable.GetEnumerator()));
            MockContext.Setup(c => c.SaveChangesAsync()).Returns(() => Task.Run(() => { return 1; })).Verifiable();

            MockSet.Setup(m => m.Add(It.IsAny<Users>())).Callback((Users project) => users.Add(project));
	        MockSet.Setup(m => m.Remove(It.IsAny<Users>())).Callback((Users project) => users.Remove(project));

           
	        MockContext.Setup(m => m.Users).Returns(MockSet.Object);

			
            //UserController.dbContext = MockContext.Object;
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
