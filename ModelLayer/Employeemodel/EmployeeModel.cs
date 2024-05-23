using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Employeemodel
{
    public class RegisterModel
    {
        public int EMPLOYEEID {  get; set; }

        [Required(ErrorMessage ="NAME CANNOT BE EMPTY..")]
        //[MaxLength(50,ErrorMessage="name must not exe")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces")]
        public string EMPLOYEENAME {  get; set; }

        [Required(ErrorMessage ="Image cannot be empty")]
        public string PROFILEIMAGE {  get; set; }
        [Required(ErrorMessage="Gender can not be empty")]
        public string GENDER {  get; set; }
        public string DEPARTMENT {  get; set; }
        [Required(ErrorMessage="salary can not be empty")]
        public long SALARY {  get; set; }
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage="Notes can not be empty")]
        public string Notes {  get; set; }
    }
}
