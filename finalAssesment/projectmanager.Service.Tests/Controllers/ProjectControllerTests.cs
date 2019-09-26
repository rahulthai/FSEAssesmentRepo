using Moq;
using NUnit.Framework;
using projectmanager.Service.Tests.Repository;
using System;
using System.Data.Entity;
using projectmanager.Service.Controllers;
using projectmanager.Service.Models;
using System.Web.Http;
using projectmanager.DAL;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;
using AutoMapper;
using projectmanager.Service.MapperConfiguration;
using NuGet.Modules;
using Newtonsoft.Json;

namespace projectmanager.Service.Controllers.Tests

{
    [TestFixture()]
    public class ProjectControllerTests : ProjectBaseTest
    {
        public ProjectControllerTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperWebApiProfile>();
            });
        }
        
        [Test()]
        public void GetAllProjectsTest()
        {
            var projects =  ProjectController.GetAllProjects();
            projects.Should().NotBeNull();

        }

        [Test()]
        public void GetProjectTest()
        {
            var projects = ProjectController.GetProject(4);
            projects.Should().NotBeNull();

        }

        [Test()]
        public void InsertProjectTest()
        {
            ProjectsModel proj = new ProjectsModel();
            proj.Project = "New Skill";
            proj.StartDate = DateTime.Parse("06/23/2019");
            proj.EndDate = DateTime.Parse("08/31/2019");
            proj.Priority = "15";

            var result = ProjectController.InsertProject(proj);
            result.Should().BeTrue();
            //Assert.AreEqual(MockContext.Object.Projects.Count(), 3);

            MockContext.Object.Projects.Count().Equals(3);
            MockContext.Verify(m => m.SaveChanges(), Times.Once);

        }

        [Test()]
        public void UpdateProjectTest()
        {
            ProjectsModel proj = new ProjectsModel();
            proj.Project_ID = 1;
            proj.Project = "Dotnet Updated Name and Dates";
            proj.StartDate = DateTime.Parse("06/23/2019");
            proj.EndDate = DateTime.Parse("08/31/2019");
            proj.Priority = "10";

            var result = ProjectController.UpdateProject(proj);
            result.Should().BeTrue();
            var project = MockContext.Object.Projects.Where(x => x.Project_ID == 1).Select(x => x.Project).FirstOrDefault();

            project.Should().NotBe("Dotnet");

        }

        [Test()]
        public void DeleteProjectTest()
        {
            var initialcount = MockContext.Object.Projects.Where(x => x.Project_ID == 1).Count();

            initialcount.Should().Equals(1);

            var result = ProjectController.DeleteProject(1);
            result.Should().BeTrue();

            var count = MockContext.Object.Projects.Where(x => x.Project_ID == 1).Count();

            count.Should().Equals(0);
        }
    }
}