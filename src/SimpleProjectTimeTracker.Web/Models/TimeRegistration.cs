﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProjectTimeTracker.Web.Models
{
    public class TimeRegistration
    {
        public int Id { get; set; } 
        [Required]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public decimal HoursWorked { get; set; }
        public bool Accounted { get; set; }
    }
}
