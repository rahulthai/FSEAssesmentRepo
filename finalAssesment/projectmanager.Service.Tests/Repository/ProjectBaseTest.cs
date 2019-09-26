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

namespace projectmanager.Service.Tests.Repository
{
    public class ProjectBaseTest
    {
        protected ProjectController ProjectController;
	    protected Mock<IContext> MockContext;
	    protected Mock<DbSet<Projects>> MockSet;

	    [SetUp]
        public void Setup()
        {
            var projects = new List<Projects>() {
            new Projects() {  Project_ID = 1,
                    Project ="Dotnet",
                    StartDate =DateTime.Parse("09-08-2015"),
                    EndDate =DateTime.Parse("09-13-2015"),
                    Priority ="10"},
            new Projects() {  Project_ID = 4,
                    Project ="Java",
                    StartDate =DateTime.Parse("05-06-2016"),
                    EndDate =DateTime.Parse("05-22-2016"),
                    Priority ="10"}
            };
            var queryable = projects.AsQueryable();

            MockSet = new Mock<DbSet<Projects>>();

	        MockSet.As<IQueryable<Projects>>().Setup(m => m.Expression).Returns(queryable.Expression);
	        MockSet.As<IQueryable<Projects>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
	        MockSet.As<IQueryable<Projects>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);

	        MockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(queryable.Provider);
	        MockSet.As<IEnumerable<Projects>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            MockSet.As<IQueryable<Projects>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<Projects>(queryable.Provider));
            MockSet.As<IDbAsyncEnumerable<Projects>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<Projects>(queryable.GetEnumerator()));


            MockSet.Setup(m => m.Add(It.IsAny<Projects>())).Callback((Projects project) => projects.Add(project));
	        MockSet.Setup(m => m.Remove(It.IsAny<Projects>())).Callback((Projects project) => projects.Remove(project));

            MockContext = new Mock<IContext>();
	        MockContext.Setup(m => m.Projects).Returns(MockSet.Object);

			ProjectController = new ProjectController();
            ProjectController.dbContext = MockContext.Object;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            
        }
    }
}
