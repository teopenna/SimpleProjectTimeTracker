﻿using System;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
