using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFisrtSampleProject.Entities
{
    //[Table("MyCourseCategories")] //> we can change the name of table in sql db and put it this name
    public class CourseCategory
    {
        [Key]
        public int CourseCategoryId { get; set; }
       // [Column("MyTitle", Order = 1, TypeName = "varchar")] > we can change the name of column in sql db and put it this name
        public string Title { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
