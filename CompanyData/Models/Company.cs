using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyData.Models
{
    public class Company
    {
        public virtual long ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Company Name should be minimum 2 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public virtual string Name { get; set; }
        [Range(705, 9999, ErrorMessage = "Correct EstablishmentYear is required")]
        public virtual int EstablishmentYear { get; set; }
        private ISet<Employee> employees;
        public virtual ISet<Employee> Employees
        {
            get { return this.employees; }
            set 
            {
                if (value != null)
                {
                    if (this.employees == null)
                        this.employees = new HashSet<Employee>();
                    foreach (var employee in value)
                    {
                        employee.Company = this;
                        this.employees.Add(employee);
                    }
                }
                else
                    employees = value;
            } 
        }

    }
}
