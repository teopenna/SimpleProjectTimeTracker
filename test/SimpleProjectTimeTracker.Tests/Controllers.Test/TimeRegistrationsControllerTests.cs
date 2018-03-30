using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleProjectTimeTracker.Web.Exceptions;
using SimpleProjectTimeTracker.Web.Infrastructure;
using SimpleProjectTimeTracker.Web.Models;
using SimpleProjectTimeTracker.Web.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    public class TimeRegistrationsControllerTests
    {
        protected TimeRegistrationsController Sut { get; }
        protected Mock<ITimeRegistrationService> MockTimeRegistrationService { get; }

        public TimeRegistrationsControllerTests()
        {
            MockTimeRegistrationService = new Mock<ITimeRegistrationService>();
            Sut = new TimeRegistrationsController(MockTimeRegistrationService.Object);
        }

        [Fact]
        public void ControllerWithNullServiceShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TimeRegistrationsController(null));
        }

        public class ViewTimeRegistrations : TimeRegistrationsControllerTests
        {
            [Fact]
            public async void ShouldReturnOkWithTimeRegistrationItems()
            {
                var expectedTimeRegistrations = new List<TimeRegistration>()
                {
                    new TimeRegistration
                    {
                        Id = 1,
                        ProjectId = 1,
                        ProjectName = "Sample web project for BUILD event",
                        CustomerName = "Microsoft",
                        Date = new DateTime(2018, 3, 24),
                        HoursWorked = 6.5m,
                        Accounted = false
                    },
                    new TimeRegistration
                    {
                        Id = 2,
                        ProjectId = 2,
                        ProjectName = "Creation of a Picking List application",
                        CustomerName = "Amazon",
                        Date = new DateTime(2018, 3, 23),
                        HoursWorked = 5.5m,
                        Accounted = false
                    }
                };

                MockTimeRegistrationService
                    .Setup(t => t.ReadAllAsync(CancellationToken.None))
                    .ReturnsAsync(expectedTimeRegistrations);

                var result = await Sut.GetTimeRegistrations(CancellationToken.None);

                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedTimeRegistrations, okResult.Value);
            }
        }

        public class Create : TimeRegistrationsControllerTests
        {
            private readonly TimeRegistration _expectedTimeRegistration;

            public Create()
            {
                _expectedTimeRegistration = new TimeRegistration
                {
                    ProjectId = 1,
                    ProjectName = "Example project creation for BUILD session",
                    CustomerName = "Microsoft",
                    Date = new DateTime(2018, 3, 18),
                    HoursWorked = 7.5m
                };
            }

            [Fact]
            public async void ShouldReturnCreatedAtActionResultWithTheCreatedTimeRegistration()
            {
                MockTimeRegistrationService
                    .Setup(t => t.CreateAsync(_expectedTimeRegistration, CancellationToken.None))
                    .ReturnsAsync(_expectedTimeRegistration);

                var result = await Sut.Create(_expectedTimeRegistration, CancellationToken.None);

                var createdResult = Assert.IsType<CreatedAtActionResult>(result);
                Assert.Same(_expectedTimeRegistration, createdResult.Value);
            }

            [Fact]
            public async void ShouldReturnBadRequestObjectResultIfModelStateIsInvalid()
            {
                Sut.ModelState.AddModelError("WorkedHours", "Worked Hours must be greater than 0");

                var result = await Sut.Create(_expectedTimeRegistration, CancellationToken.None);

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }

        public class Update : TimeRegistrationsControllerTests
        {
            private readonly TimeRegistration _expectedTimeRegistration;

            public Update()
            {
                _expectedTimeRegistration = new TimeRegistration
                {
                    ProjectId = 1,
                    ProjectName = "Example project creation for BUILD session",
                    CustomerName = "Microsoft",
                    Date = new DateTime(2018, 3, 18),
                    HoursWorked = 7.5m
                };
            }

            [Fact]
            public async void ShouldReturnOkActionResultWithUpdatedTimeRegistration()
            {
                MockTimeRegistrationService
                    .Setup(t => t.UpdateAsync(_expectedTimeRegistration.Id, _expectedTimeRegistration, CancellationToken.None))
                    .ReturnsAsync(_expectedTimeRegistration);

                var result = await Sut.Update(_expectedTimeRegistration.Id, _expectedTimeRegistration, CancellationToken.None);

                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(_expectedTimeRegistration, okResult.Value);
            }

            [Fact]
            public async void ShouldReturnBadRequestObjectResultIfModelStateIsInvalid()
            {
                Sut.ModelState.AddModelError("WorkedHours", "Worked Hours must be greater than 0");

                var result = await Sut.Update(1, _expectedTimeRegistration, CancellationToken.None);

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }

            [Fact]
            public async void ShouldReturnNotFoundResultIfTimeRegistrationDoesntExist()
            {
                MockTimeRegistrationService
                    .Setup(t => t.UpdateAsync(100, _expectedTimeRegistration, CancellationToken.None))
                    .Throws(new TimeRegistrationNotFoundException("Example project creation for BUILD session"));

                var result = await Sut.Update(100, _expectedTimeRegistration, CancellationToken.None);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
            }
        }

        public class Delete : TimeRegistrationsControllerTests
        {
            public Delete()
            {

            }

            [Fact]
            public async void ShouldReturnNotFoundResultIfTimeRegistrationDoesntExist()
            {
                MockTimeRegistrationService
                    .Setup(t => t.DeleteAsync(100, CancellationToken.None))
                    .Throws(new TimeRegistrationNotFoundException("Example project creation for BUILD session"));

                var result = await Sut.Delete(100, CancellationToken.None);

                var notFoundResult = Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
