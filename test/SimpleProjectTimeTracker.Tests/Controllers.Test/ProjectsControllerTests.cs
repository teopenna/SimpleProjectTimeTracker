using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleProjectTimeTracker.Web.Models;
using SimpleProjectTimeTracker.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    public class ProjectsControllerTests
    {
        protected ProjectsController Sut { get; }
        protected Mock<IProjectService> MockPProjectService { get; }

        public ProjectsControllerTests()
        {
            MockPProjectService = new Mock<IProjectService>();
            Sut = new ProjectsController(MockPProjectService.Object);
        }

        [Fact]
        public void ControllerWithNullServiceShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProjectsController(null));
        }

        public class ReadAllAsync : ProjectsControllerTests
        {
            [Fact]
            public async void ShouldReturnOkWithProjectItems()
            {
                var expectedProjects = new List<Project>()
                {
                    new Project
                    {
                        CustomerName = "Microsoft",
                        Name = "Sample web project for BUILD event",
                        DueDate = new DateTime(2018, 7, 31),
                        HourlyRate = 45
                    },
                    new Project
                    {
                        CustomerName = "Amazon",
                        Name = "Creation of a Picking List application",
                        DueDate = new DateTime(2018, 9, 30),
                        HourlyRate = 47
                    }
                };

                MockPProjectService
                    .Setup(p => p.ReadAllAsync(CancellationToken.None))
                    .ReturnsAsync(expectedProjects);

                var result = await Sut.GetProjects(CancellationToken.None);

                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedProjects, okResult.Value);
            }
        }
    }
}
