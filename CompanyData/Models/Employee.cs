using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyData.Models
{
    public class Employee
    {
        public virtual long ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "FirstName should be minimum 2 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public virtual string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "LastName should be minimum 2 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public virtual string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public virtual DateTime? DateOfBirth { get; set; }
        [Required]
        [Range(0, 3, ErrorMessage = "JobTitle is not correct")]
        public virtual JobTitle? JobTitle { get; set; }
        public virtual Company Company { get; set; }
    }
    public enum JobTitle
    {
        Administrator, 
        Developer, 
        Architect, 
        Manager
    }
}
