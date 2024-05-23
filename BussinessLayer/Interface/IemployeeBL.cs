using ModelLayer.Employeemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
   public interface IemployeeBL 
    {
        public bool RegisterEmployee(RegisterModel employeemodel);
        public IEnumerable<RegisterModel> GetEmployees();
        public bool Update_employee(RegisterModel registerModel);
        public RegisterModel GetById(int id);
        public bool DeleteEmployee(int id);
        public RegisterModel GetByName(string name);
        public RegisterModel LogIn(LoginModel loginModel);
        public RegisterModel GetName(string name);
        public IEnumerable<RegisterModel> GetNameList(string name);
        public List<RegisterModel> GetSameNameList(string EmpName);
        public RegisterModel GetSameName(string EmpName);
       
    }
}
