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
    public class UsersControllerTests : UserBaseTest
    {
        private const int runtimeMilliseconds = 600000;
        private const int Iterations = 13;
        public UsersControllerTests()
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
        [PerfBenchmark(Description = "Test to ensure GetAll Users.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void GetAllUsersTest()
        {
            var users = UsersController.GetAllUsers();
            users.Should().NotBeNull();

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Get Users for a given key.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void GetUserTest()
        {
            var users = UsersController.GetUserAsync(2);
            users.Should().NotBeNull();

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Insert Users record.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void InsertUserTest()
        {
            proj.FirstName = "Aadhya";
            proj.LastName = "Jain";
            proj.Employee_ID = "AJ001";

            var result = UsersController.InsertUserAsync(proj);
            result.Result.Should().BeTrue();

            MockContext.Object.Users.Count().Equals(3);
            //MockContext.Verify(m => m.SaveChangesAsync(), Times.Once);

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Update User record.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void UpdateUserTest()
        {
            //UsersModel proj = new UsersModel();
            proj.User_ID = 1;
            proj.FirstName = "Rahul K";
            proj.LastName = "Jain";
            proj.Employee_ID = "RT001";

            var result = UsersController.UpdateUser(proj);
            result.Result.Should().BeTrue();
            var firstName = MockContext.Object.Users.Where(x => x.User_ID == 1).Select(x => x.FirstName).FirstOrDefault();

            firstName.Should().NotBe("Rahul");

        }

        [Test()]
        [PerfBenchmark(Description = "Test to ensure Delete User record.",
        NumberOfIterations = Iterations, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = runtimeMilliseconds, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.Gen2)]
        public void DeleteUserTest()
        {
            var initialcount = MockContext.Object.Users.Where(x => x.User_ID == 1).Count();

            initialcount.Should().Equals(1);

            var result = UsersController.DeleteUser(1);
            result.Result.Should().BeTrue();

            var count = MockContext.Object.Users.Where(x => x.User_ID == 1).Count();

            count.Should().Equals(0);
        }
    }
}