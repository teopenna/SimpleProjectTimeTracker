using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleProjectTimeTracker.Web.Models;
using SimpleProjectTimeTracker.Web.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    public class ProjectsControllerTests
    {
        protected ProjectsController Sut { get; }
        protected Mock<ITimeRegistrationService> MockPProjectService { get; }

        public ProjectsControllerTests()
        {
            MockPProjectService = new Mock<ITimeRegistrationService>();
            Sut = new ProjectsController(MockPProjectService.Object);
        }

        
        public void ControllerWithNullServiceShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProjectsController(null));
        }

        public class ReadAllAsync : ProjectsControllerTests
        {
            
        }
    }
}
