using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFisrtSampleProject.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseCategoryId { get; set; }
        public virtual CourseCategory CourseCategory { get; set; }
    }
}
