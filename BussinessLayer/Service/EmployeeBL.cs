using BussinessLayer.Interface;
using ModelLayer.Employeemodel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class EmployeeBL : IemployeeBL
    {
        private readonly IEmployeeRL iemployeeRL;
        public  EmployeeBL(IEmployeeRL iemployeeRL)
        {
            this.iemployeeRL = iemployeeRL;            
        }

        public bool DeleteEmployee(int id)
        {
           return this.iemployeeRL.DeleteEmployee(id);
        }

        public RegisterModel GetById(int id)
        {
            return iemployeeRL.GetById(id);
        }

        public RegisterModel GetByName(string name)
        {
            return iemployeeRL.GetByName(name);
        }

        public IEnumerable<RegisterModel> GetEmployees()
        {
            return iemployeeRL.GetEmployees();
        }

        public RegisterModel GetName(string name)
        {
            return iemployeeRL.GetName(name);
        }

        

        public IEnumerable<RegisterModel> GetNameList(string name)
        {
           return iemployeeRL.GetNameList(name);
        }

        public RegisterModel GetSameName(string EmpName)
        {
            return iemployeeRL.GetSameName(EmpName);
        }

        public List<RegisterModel> GetSameNameList(string EmpName)
        {
            return iemployeeRL.GetSameNameList(EmpName);
        }

        public RegisterModel LogIn(LoginModel loginModel)
        {
            return iemployeeRL.LogIn(loginModel);
        }

        public bool RegisterEmployee(RegisterModel employeemodel)
        {
            return iemployeeRL.RegisterEmployee(employeemodel);
        }

        public bool Update_employee(RegisterModel registerModel)
        {
            return iemployeeRL.Update_employee(registerModel);
        }
    }
}
