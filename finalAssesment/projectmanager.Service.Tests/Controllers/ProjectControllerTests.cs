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
using NBench.Util;
using NBench;

namespace projectmanager.Service.Controllers.Tests

{
    [TestFixture()]
    public class ProjectControllerTests : ProjectBaseTest
    {
        private const int runtimeMilliseconds = 600000;
        private const int Iterations = 13;
        public ProjectControllerTests()
        {
            AutoMapper.Mapper.Reset();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperWebApiProfile>();
            });
        }

        [PerfBenchmark(Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        //[CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
        //[MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        //[GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void Benchmark()
        {
            _counter.Increment();
        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure GetAll Projects.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void GetAllProjectsTest()
        {
            //_counter.Increment();
            var projects =  ProjectController.GetAllProjects();
            projects.Should().NotBeNull();

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Get Projects for a given key.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void GetProjectTest()
        {
            var projects = ProjectController.GetProjectAsync(4);
            projects.Should().NotBeNull();

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Insert Projects record.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void InsertProjectTest()
        {
            //ProjectsModel proj = new ProjectsModel();
            proj.Project = "New Skill Recent";
            proj.StartDate = DateTime.Parse("06/23/2019");
            proj.EndDate = DateTime.Parse("08/31/2019");
            proj.Priority = "12";

            var result = ProjectController.InsertProjectAsync(proj);
            result.Result.Should().BeTrue();
            //Assert.AreEqual(MockContext.Object.Projects.Count(), 3);

            MockContext.Object.Projects.Count().Equals(3);
            //MockContext.Verify(m => m.SaveChangesAsync(), Times.Once);

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Update Project record.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void UpdateProjectTest()
        {
            //ProjectsModel proj = new ProjectsModel();
            proj.Project_ID = 1;
            proj.Project = "Dotnet Updated Name and Dates";
            proj.StartDate = DateTime.Parse("06/23/2019");
            proj.EndDate = DateTime.Parse("08/31/2019");
            proj.Priority = "10";
           

            var result = ProjectController.UpdateProject(proj);
            result.Result.Should().BeTrue();
            var project = MockContext.Object.Projects.Where(x => x.Project_ID == 1).Select(x => x.Project).FirstOrDefault();

            project.Should().NotBe("Dotnet");

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Delete Project record.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void DeleteProjectTest()
        {
            var initialcount = MockContext.Object.Projects.Where(x => x.Project_ID == 1).Count();

            initialcount.Should().Equals(1);

            var result = ProjectController.DeleteProject(1);
            result.Result.Should().BeTrue();

            var count = MockContext.Object.Projects.Where(x => x.Project_ID == 1).Count();

            count.Should().Equals(0);
        }
    }
}