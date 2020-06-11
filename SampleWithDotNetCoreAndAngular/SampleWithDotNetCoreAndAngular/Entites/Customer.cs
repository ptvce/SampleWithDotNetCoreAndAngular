using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Entites
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }
        [Required,MaxLength(128)]
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        [MaxLength(256)]
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public int CategoryId { get; set; }
        //navigation property
        public virtual Categories Category { get; set; }
    }
}
