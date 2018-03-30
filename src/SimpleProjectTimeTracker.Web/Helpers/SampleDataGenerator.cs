using SimpleProjectTimeTracker.Web.Models;
using System;
using System.Collections.Generic;

namespace SimpleProjectTimeTracker.Web.Helpers
{
    public class SampleDataGenerator
    {
        public static void SeedData(SimpleProjectTimeTrackerDbContext context)
        {
            var project1 = context.Projects.Add(new ProjectEntity
            {
                Id = 1,
                CustomerName = "Microsoft",
                Name = "Sample web project for BUILD event",
                DueDate = new DateTime(2018, 7, 31),
                HourlyRate = 45
            }).Entity;
            
            var project2 = context.Projects.Add(new ProjectEntity
            {
                Id = 2,
                CustomerName = "Amazon",
                Name = "Creation of a Picking List application",
                DueDate = new DateTime(2018, 9, 30),
                HourlyRate = 47
            }).Entity;

            context.TimeRegistrations.Add(new TimeRegistrationEntity
            {
                Id = 1,
                ProjectId = 1,
                Project = project1,
                Date = new DateTime(2018, 3, 24),
                HoursWorked = 6.5m,
                Accounted = false
            });

            context.TimeRegistrations.Add(new TimeRegistrationEntity
            {
                Id = 2,
                ProjectId = 1,
                Project = project1,
                Date = new DateTime(2018, 3, 23),
                HoursWorked = 5.5m,
                Accounted = false
            });

            context.TimeRegistrations.Add(new TimeRegistrationEntity
            {
                Id = 3,
                ProjectId = 2,
                Project = project2,
                Date = new DateTime(2018, 3, 23),
                HoursWorked = 2.5m,
                Accounted = false
            });

            context.SaveChanges();
        }
        
        public static IEnumerable<Project> CreateSampleProjects()
        {
            var sampleProjects = new List<Project>()
            {
                new Project
                {
                    CustomerName = "Microsoft Italia",
                    Name = "Creation of a sample application for BUILD",
                    DueDate = new DateTime(2018, 7, 31)
                },
                new Project
                {
                    CustomerName = "Amazon Italia",
                    Name = "Creation of a Picking List application",
                    DueDate = new DateTime(2018, 5, 31)
                }
            };

            return sampleProjects;
        }
    }
}
