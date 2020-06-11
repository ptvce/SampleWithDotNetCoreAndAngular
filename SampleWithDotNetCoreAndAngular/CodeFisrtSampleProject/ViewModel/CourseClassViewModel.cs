using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFisrtSampleProject.ViewModel
{
    public class CourseClassViewModel
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public CourseClassDayViewModel[] Days { get; set; }
    }
    public class CourseClassDayViewModel
    {
        public int DayId { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
    }
}
