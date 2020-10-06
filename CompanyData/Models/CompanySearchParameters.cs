using System;
using System.Collections.Generic;

namespace CompanyData.Models
{
    public class CompanySearchParameters
    {
        public virtual string Keyword { get; set; }
        public virtual DateTime? EmployeeDateOfBirthFrom { get; set; }
        public virtual DateTime? EmployeeDateOfBirthTo { get; set; }
        public virtual List<JobTitle> EmployeeJobTitles { get; set; }
    }
}
