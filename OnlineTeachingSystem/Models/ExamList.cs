using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.Models
{
    public class ExamList
    {
        [Key]
        public string ExamName { get; set; }
        public int Grade { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
    }
}