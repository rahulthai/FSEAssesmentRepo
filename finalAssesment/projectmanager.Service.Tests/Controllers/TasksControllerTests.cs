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
using System.Globalization;

namespace projectmanager.Service.Controllers.Tests
{
    [TestFixture()]
    public class TasksControllerTests: TasksBaseTest
    {
            //setting the run time to 10 mins
            private const int runtimeMilliseconds = 600000;
            private const int Iterations = 13;
            public TasksControllerTests()
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
            [CounterMeasurement("TestCounter")]
            [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
            [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
            public void Benchmark()
            {
                _counter.Increment();
            }

            [Test()]
            [PerfBenchmark(Description = "Test to ensure GetAll Tasks.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
            [CounterMeasurement("TestCounter")]
            [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
            [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
            public void GetAllTasksTest()
            {
                //_counter.Increment();
                var projects = TasksController.GetAllTasks();
                projects.Should().NotBeNull();

            }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure GetAll ParentTasks.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void GetAllParentTasksTest()
        {
            //_counter.Increment();
            var projects = TasksController.GetParentTasks();
            projects.Should().NotBeNull();

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure GetAll Tasks by project Id.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void GetAllTasksByProjectIdTest()
        {
            //_counter.Increment();
            var projects = TasksController.getTasksListByProjectId(4);
            projects.Should().NotBeNull();

        }

        [Test()]
            [PerfBenchmark(Description = "Test to ensure Get Tasks for a given key.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
            [CounterMeasurement("TestCounter")]
            [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
            [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
            public void GetTaskTest()
            {
                var projects = TasksController.GetTaskByID(4);
                projects.Should().NotBeNull();

            }

            [Test()]
            [PerfBenchmark(Description = "Test to ensure Insert Parent Tasks record.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
            [CounterMeasurement("TestCounter")]
            [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
            [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
            public void InsertParentTaskTest()
            {
                //TasksModel proj = new TasksModel();
                taskObject.Task = "New todo task";
                taskObject.Project_ID = 4;
                taskObject.IsParentTask= true;


                var result = TasksController.InsertTaskAsync(taskObject);
                result.Result.Should().BeTrue();
                //Assert.AreEqual(MockContext.Object.Tasks.Count(), 3);

                MockContext.Object.Tasks.Count().Equals(3);
                //MockContext.Verify(m => m.SaveChangesAsync(), Times.Once);

            }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Insert Tasks record.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void InsertTaskTest()
        {
            //TasksModel proj = new TasksModel();
            taskObject.Task = "New Task addition test";
            taskObject.Start_Date = DateTime.ParseExact("06/23/2019", "MM/dd/yyyy", CultureInfo.InstalledUICulture);
            taskObject.End_Date = DateTime.ParseExact("08/31/2019", "MM/dd/yyyy", CultureInfo.InstalledUICulture);
            taskObject.Priority = "12";
            taskObject.Project_ID = 4;
            taskObject.User_ID = 1;
            taskObject.Parent_ID = 2;
            taskObject.IsParentTask = false;


            var result = TasksController.InsertTaskAsync(taskObject);
            result.Result.Should().BeTrue();
            //Assert.AreEqual(MockContext.Object.Tasks.Count(), 3);

            MockContext.Object.Tasks.Count().Equals(3);
            //MockContext.Verify(m => m.SaveChangesAsync(), Times.Once);

        }

        [Test()]
            [PerfBenchmark(Description = "Test to ensure Update Parent Task record.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
            [CounterMeasurement("TestCounter")]
            [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
            [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
            public void UpdateParentTaskTest()
            {
                //TasksModel proj = new TasksModel();
                taskObject.Task_ID = 1;
                taskObject.Task = "New todo task";
                taskObject.Project_ID = 4;
                taskObject.IsParentTask = true;


            var result = TasksController.UpdateTask(taskObject);
                result.Result.Should().BeTrue();
                var project = MockContext.Object.Tasks.Where(x => x.Task_ID == 1).Select(x => x.Task).FirstOrDefault();

                project.Should().NotBe("Task first testing");

            }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Update Task record.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void UpdateTaskTest()
        {
            //TasksModel proj = new TasksModel();
            taskObject.Task_ID = 1;
            taskObject.Task = "New Task addition test updated";
            taskObject.Start_Date = DateTime.ParseExact("06/23/2019", "MM/dd/yyyy", CultureInfo.InstalledUICulture);
            taskObject.End_Date = DateTime.ParseExact("08/31/2019", "MM/dd/yyyy", CultureInfo.InstalledUICulture);
            taskObject.Priority = "12";
            taskObject.Project_ID = 4;
            taskObject.User_ID = 1;
            taskObject.Parent_ID = 2;
            taskObject.IsParentTask = false;


            var result = TasksController.UpdateTask(taskObject);
            result.Result.Should().BeTrue();
            var task = MockContext.Object.Tasks.Where(x => x.Task_ID == 1).Select(x => x.Task).FirstOrDefault();

            task.Should().NotBe("Task first testing");

        }

        [Test()]
            [PerfBenchmark(Description = "Test to ensure Delete Task record.",
            NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
            RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
            [CounterMeasurement("TestCounter")]
            [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
            [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
            public void DeleteTaskTest()
            {
                var initialcount = MockContext.Object.Tasks.Where(x => x.Task_ID == 1 && x.TaskStatus==null).Count();

                initialcount.Should().Equals(1);

                var result = TasksController.DeleteTask(1);
                result.Result.Should().BeTrue();

                var count = MockContext.Object.Tasks.Where(x => x.Task_ID == 1 && x.TaskStatus==null).Count();

                count.Should().Equals(0);
            }
        }
}