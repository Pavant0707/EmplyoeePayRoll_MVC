using BussinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Employeemodel;
using System.Xml.Linq;

namespace EmployeePayRoll.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IemployeeBL iemployeeBl;

        public EmployeeController(IemployeeBL iemployeeBl)
        {
            this.iemployeeBl = iemployeeBl;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Index()
        {
            List<RegisterModel> lstEmployee = new List<RegisterModel>();
            lstEmployee = iemployeeBl.GetEmployees().ToList();
            return View(lstEmployee);

            // return View();
        }

        [HttpGet]
        [Route("Create")]

        public IActionResult Create()
        {
            ViewBag.data = "Add Employee";
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] RegisterModel employee)
        {
            if (ModelState.IsValid)
            {
                iemployeeBl.RegisterEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //[HttpGet("GetEmployees")]
        //public IActionResult GetEmployees()
        //{
        //    List<RegisterModel> lstEmployee = new List<RegisterModel>();
        //    lstEmployee = iemployeeBl.GetEmployees().ToList();

        //    return View(lstEmployee);
        //}
        [HttpGet]
        [Route("list/{name}")]
        public IActionResult GetNameList(string name)
        {
            List<RegisterModel> lstEmployee1 = new List<RegisterModel>();
            lstEmployee1 = iemployeeBl.GetNameList(name).ToList();
            return View(lstEmployee1);
        }
        [HttpPost]
        public IActionResult GetNameList1(string name)
        {
            if (name == null)
            {
                return NotFound();
            }
            RegisterModel employee = (RegisterModel)iemployeeBl.GetNameList(name);
            if (employee == null)
            {
                return NotFound("something went wrong");
            }
            return View(employee);

        }

        [HttpGet]
        [Route("Update/{id}")]


        //update
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RegisterModel employee = iemployeeBl.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        [HttpPost]
        [Route("Update/{id}")]
        public IActionResult Edit(RegisterModel registerModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    iemployeeBl.Update_employee(registerModel);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(registerModel);
            }

        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int Id)
        {
            //Id = (int)HttpContext.Session.GetInt32("Id");
            if (Id == null)
            {
                return NotFound();

            }
            RegisterModel empModel = iemployeeBl.GetById(Id);

            if (empModel == null)
            {
                return NotFound("Something Went Wrong");
            }
            return View(empModel);

        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RegisterModel employee = iemployeeBl.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            iemployeeBl.DeleteEmployee(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            if (name == null)
            {
                return NotFound();

            }
            RegisterModel empModel = iemployeeBl.GetByName(name);

            if (empModel == null)
            {
                return NotFound("Something Went Wrong");
            }
            return View(empModel);

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login(LoginModel model)
        {
            var res = iemployeeBl.LogIn(model);
            if (res != null)
            {
                HttpContext.Session.SetInt32("Id", res.EMPLOYEEID);
                return RedirectToAction("GetById", new { Id = res.EMPLOYEEID });
            }

            else
            {
                return Content("Invalid Credintails");
            }

        }
        [HttpGet]
        [Route("GetName/{name}")]
        public IActionResult GetName(string name)
        {
            if (name == null)
            {
                return NotFound();

            }
            RegisterModel empModel = iemployeeBl.GetName(name);

            if (empModel == null)
            {
                return NotFound("Something Went Wrong");
            }
            return View(empModel);

        }
        //[HttpGet]
        //[Route("samename/{name}")]
        //public IActionResult GetSameNameList(string name)
        //{
        //    List<RegisterModel> lstEmployeename= new List<RegisterModel>();
        //    lstEmployeename = iemployeeBl.GetSameNameList(name).ToList();
        //    return View(lstEmployeename);
        //}
        //[HttpPost]
        //public IActionResult GetSameNameList1(string name)
        //{
        //    if (name == null)
        //    {
        //        return NotFound();
        //    }
        //    RegisterModel employee = (RegisterModel)iemployeeBl.GetSameNameList(name);
        //    if (employee == null)
        //    {
        //        return NotFound("something went wrong");
        //    }
        //    return View(employee);

        //}
        [HttpGet("Same")]
       
        public IActionResult Same()
        {
            return View();
        }

        [HttpPost("Same")]
       
        public IActionResult Same(string EmpName)
        {
            List<RegisterModel> employeeLst = new List<RegisterModel>();
            RegisterModel emp = new RegisterModel();

            employeeLst=iemployeeBl.GetSameNameList(EmpName);
            RegisterModel empModel = iemployeeBl.GetSameName(EmpName);
            emp = iemployeeBl.GetSameName(EmpName);

            int count = iemployeeBl.GetSameNameList(EmpName).Count();
            if (count == 1)
            {
                if (emp != null)
                {
                    return View("GetByName", emp);
                }
            }
            else if (employeeLst != null)
            {
                return View("Index", employeeLst);
            }
            else
            {
                return null;
            }
            return View();
        }


        

        [HttpGet("getallbyname")] /*https://localhost:7121/getallbyname?name=RAM*/
        public IActionResult GetAllEmployeeByName(string name)
        {
            List<RegisterModel> lst = new List<RegisterModel>();
            lst = iemployeeBl.GetSameNameList(name).ToList();
            return View(lst);
        }

        [HttpPost]
        public IActionResult GetAllEmploYeeByName(string name)
        {
            if (name == null)
            {
                return NotFound();
            }
            RegisterModel employee = (RegisterModel)iemployeeBl.GetSameName(name);
            if (employee == null)
            {
                return NotFound("something went wrong");
            }
            return View(employee);
        }
    }
}
    

