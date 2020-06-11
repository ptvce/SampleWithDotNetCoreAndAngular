using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFisrtSampleProject.Entities
{
    public class CourseClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public ICollection<CourseClassDay> CourseClassDay { get; set; }
    }
    public class CourseClassDay
    {
        public int Id { get; set; }
        public Days Day { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public int CourseClassId { get; set; }
        public virtual CourseClass CourseClass { get; set; }
    }
}
