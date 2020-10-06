using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyData.Models
{
    public class CompanySearchParameters
    {
        [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Keyword should be minimum 2 characters and a maximum of 100 characters")]
        public virtual string Keyword { get; set; }
        [DataType(DataType.Date)]
        public virtual DateTime? EmployeeDateOfBirthFrom { get; set; }
        [DataType(DataType.Date)]
        public virtual DateTime? EmployeeDateOfBirthTo { get; set; }
        public virtual List<JobTitle> EmployeeJobTitles { get; set; }
    }
}
