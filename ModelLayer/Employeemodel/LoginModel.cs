using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Employeemodel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "ID CANNOT BE EMPTY..")]
        public int EMPLOYEEID { get; set; }
        [Required(ErrorMessage = "NAME CANNOT BE EMPTY..")]
        public string EMPLOYEENAME { get; set; }
    }
}
