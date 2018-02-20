using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelephoneDirectory.BusinessLayer;
using TelephoneDirectory.BusinessLayer.Result;
using TelephoneDirectory.Entities;
using TelephoneDirectory.Entities.LoginViewModel;
using TelephoneDirectory.Web.ViewModels;
using TelephoneDirectory.Entities.Messages;

namespace TelephoneDirectory.Web.Controllers
{
    public class HomeController : Controller
    {
        AdminManager adminManager = new AdminManager();
        EmployeeManager employeeManager = new EmployeeManager();
        DepartmentManager departmentManager = new DepartmentManager();

        public ActionResult Index()
        {
            BusinessLayer.Test t = new BusinessLayer.Test();

            if (Session["login"] != null)
            {
                return RedirectToAction("AdminLogin");
            }
            else
                return RedirectToAction("ListEmployee");
        }

        public ActionResult AdminLogin()
        {
            if (Session["login"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ListEmployee");
            }
        }

        [HttpPost]
        public ActionResult AdminLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Admin> res = adminManager.LoginAdmin(model);
                if (res.Errors.Count > 0)
                {

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                Session["login"] = res.Result;
                return RedirectToAction("ListEmployee");
            }
            return View();
        }

        public ActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminRegister(Admin model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Admin> res = new BusinessLayerResult<Admin>();
                res.Result = model;

                if (adminManager.Insert(model) > 0)
                {

                    OkViewModel ntfobj = new OkViewModel()
                    {
                        Title = "Registration Successful",
                        RedirectingUrl = "/Home/AdminLogin",
                        RedirectingTimeout = 1000
                    };
                    ntfobj.Items.Add("Admin Registration Was Succesful..");
                    return View("Ok", ntfobj);
                }
                else
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
            }
            return View();
        }

        public ActionResult AddEmployee()
        {
            if (Session["login"]!=null)
            {
                ViewBag.DepartmentId = new SelectList(departmentManager.List(), "DepartmentId", "Title");
                ViewBag.DirectorId = new SelectList(employeeManager.List(), "EmployeeID", "Name");
                return View();
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
                
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(AddEmployeViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                Employee e = new Employee()
                {
                    Departman = departmentManager.Find(x => x.DepartmentId == model.DepartmentId),
                    Director = employeeManager.Find(x => x.EmployeeID == model.DirectorId),
                    Name = model.Name,
                    Surname = model.Surname,
                    PhoneNumber = model.PhoneNumber
                };


                if (employeeManager.Insert(e) > 0)
                {

                    OkViewModel ntfobj = new OkViewModel()
                    {
                        Title = "Registration Successful",
                        RedirectingUrl = "/Home/ListEmployee",
                        RedirectingTimeout = 1000

                    };
                    ntfobj.Items.Add("Employee registration was successful..");
                    return View("Ok", ntfobj);
                }


            }
            ViewBag.DepartmentId = new SelectList(departmentManager.List(), "DepartmentId", "Title", model.DepartmentId);
            if (model.DirectorId != null)
            {
                ViewBag.EmployeeID = new SelectList(employeeManager.List(), "EmployeeID", "Name", model.DirectorId);
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(employeeManager.List(), "EmployeeID", "Name", "- None -");
            }

            return RedirectToAction("ListEmployee");
        }

        #region EditEmployee
        public ActionResult EditEmployee(int? id)
        {
            if (Session["login"]!=null)
            {
                Employee e = employeeManager.Find(x => x.EmployeeID == id.Value);

                ViewBag.DepartmentId = new SelectList(departmentManager.List(), "DepartmentId", "Title", e.DepartmentId);
                ViewBag.DirectorId = new SelectList(employeeManager.List(), "EmployeeID", "Name", e.DirectorId);
                return View(e);
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                if (model.DirectorId == model.EmployeeID)
                {
                    ErrorViewModel ntfobj = new ErrorViewModel()
                    {
                        Title = "Error",
                        RedirectingUrl = "/Home/EditEmployee/" + model.EmployeeID,
                        RedirectingTimeout = 1800
                    };
                    ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.EmployeeIsADirector, "Selected Employee is A Director..!");
                    ntfobj.Items.Add(obj);
                    return View("Error", ntfobj);
                }
                else
                {
                    Employee e = employeeManager.Find(x => x.EmployeeID == model.EmployeeID);
                    e.DepartmentId = model.DepartmentId;
                    e.DirectorId = model.DirectorId;
                    e.Name = model.Name;
                    e.Surname = model.Surname;
                    e.PhoneNumber = model.PhoneNumber;

                    if (employeeManager.Update(e) > 0)
                    {

                        OkViewModel ntfobj = new OkViewModel()
                        {
                            Title = "Update Successful",
                            RedirectingUrl = "/Home/ListEmployee",
                            RedirectingTimeout = 1000

                        };
                        ntfobj.Items.Add("Employee update was successful..");
                        return View("Ok", ntfobj);
                    }

                }

            }
            ViewBag.DepartmentId = new SelectList(departmentManager.List(), "DepartmentId", "Title", model.DepartmentId);
            if (model.DirectorId != null)
            {
                ViewBag.EmployeeID = new SelectList(employeeManager.List(), "EmployeeID", "Name", model.DirectorId);
            }
            else
            {
                ViewBag.EmployeeID = new SelectList(employeeManager.List(), "EmployeeID", "Name", "- None -");
            }

            return RedirectToAction("ListEmployee");
        }
        #endregion

        public ActionResult DeleteEmployee(int? id)
        {
            if (Session["login"]!=null)
            {
                Employee e = employeeManager.Find(x => x.EmployeeID == id.Value);
                return View(e);
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
            

        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployeeConfirmed(int id)
        {
            if (employeeManager.Find(y => y.DirectorId == id) != null)
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee",
                    RedirectingTimeout = 3000

                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.EmployeeIsADirector, "Selected Employee is A Director..!");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
            else
            {
                Employee e = employeeManager.Find(x => x.EmployeeID == id);
                if (employeeManager.Delete(e) > 0)
                {
                    OkViewModel ntfobj = new OkViewModel()
                    {
                        Title = "Delete Successful",
                        RedirectingUrl = "/Home/ListEmployee",
                        RedirectingTimeout = 1000

                    };
                    ntfobj.Items.Add("Employee delete was successful..");
                    return View("Ok", ntfobj);
                }
                return RedirectToAction("ListEmployee");
            }
        }

        public ActionResult EditAdminProfile()
        {
            if (Session["login"]!=null)
            {
                Admin currentAdmin = Session["login"] as Admin;
                AdminManager adm = new AdminManager();
                BusinessLayerResult<Admin> res = new BusinessLayerResult<Admin>();
                res.Result = adminManager.Find(x => x.AdminId == currentAdmin.AdminId);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel ErrnotifyObj = new ErrorViewModel()
                    {
                        Title = "Error",
                        Items = res.Errors
                    };
                    return View("Error", ErrnotifyObj);
                }
                return View(res.Result);
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
            
        }
        [HttpPost]
        public ActionResult EditAdminProfile(Admin admin)
        {
            if (ModelState.IsValid)
            {
                Admin a = adminManager.Find(x => x.AdminId == admin.AdminId);
                a.Name = admin.Name;
                a.Surname = admin.Surname;
                a.Username = admin.Username;
                a.Password = admin.Password;

                if (adminManager.Update(a) <= 0)
                {
                    ErrorViewModel Errmsgs = new ErrorViewModel()
                    {
                        Title = "Update Failed",
                        RedirectingUrl = "/Home/EditAdminProfile"
                    };
                    return View("Error", Errmsgs);
                }
                Session["login"] = admin;
                return RedirectToAction("ShowAdminProfile");
            }
            return View(adminManager.Find(x => x.AdminId == admin.AdminId));
        }

        public ActionResult ShowAdminProfile()
        {
            if (Session["login"]!=null)
            {
                Admin currentAdmin = Session["login"] as Admin;
                AdminManager tm = new AdminManager();
                BusinessLayerResult<Admin> res = new BusinessLayerResult<Admin>();
                res.Result = tm.Find(x => x.AdminId == currentAdmin.AdminId);
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel ErrnotifyObj = new ErrorViewModel()
                    {
                        Title = "Error",
                        Items = res.Errors
                    };
                    return View("Error", ErrnotifyObj);
                }
                return View(res.Result);
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
            
        }

        public ActionResult DeleteDepartment(int? id)
        {
            if (Session["login"]!=null)
            {
                Department d = departmentManager.Find(x => x.DepartmentId == id.Value);
                return View(d);
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
            

        }

        [HttpPost, ActionName("DeleteDepartment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartmentConfirmed(int id)
        {
            if (employeeManager.Find(y => y.DepartmentId == id) != null)
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListDepartment",
                    RedirectingTimeout = 3000

                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.EmployeeIsADirector, "There are employees in your chosen department..!");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
            else
            {
                Department d = departmentManager.Find(x => x.DepartmentId == id);
                if (departmentManager.Delete(d) > 0)
                {
                    OkViewModel ntfobj = new OkViewModel()
                    {
                        Title = "Delete Successful",
                        RedirectingUrl = "/Home/ListDepartment/",
                        RedirectingTimeout = 1000

                    };
                    ntfobj.Items.Add("Department delete was successful..");
                    return View("Ok", ntfobj);
                }
                return RedirectToAction("ListDepartment");
            }
        }

        public ActionResult AddDepartment()
        {
            if (Session["login"]!=null)
            {
                return View();
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
        }

        [HttpPost]
        public ActionResult AddDepartment(Department model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Department> res = new BusinessLayerResult<Department>();
                res.Result = model;

                if (departmentManager.Insert(model) > 0)
                {

                    OkViewModel ntfobj = new OkViewModel()
                    {
                        Title = "Registration Successful",
                        RedirectingUrl = "/Home/ListDepartment",
                        RedirectingTimeout = 1000

                    };
                    ntfobj.Items.Add("Department registration was successful");
                    return View("Ok", ntfobj);
                }
                else
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
            }
            return View();
        }
        public ActionResult DepartmentDetail(int? id)
        {
            DepartmentViewModel d = new DepartmentViewModel();
            d.Department = departmentManager.Find(x => x.DepartmentId == id.Value);
            d.DepartmentEmployees = employeeManager.List(x => x.DepartmentId == id.Value);
            return View(d);
        }

        public ActionResult EditDepartment(int? id)
        {
            Department d = departmentManager.Find(x => x.DepartmentId == id.Value);
            return View(d);
        }

        [HttpPost]
        public ActionResult EditDepartment(Department model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Department> d = new BusinessLayerResult<Department>();

                d.Result = departmentManager.Find(x => x.DepartmentId == model.DepartmentId);

                d.Result.Title = model.Title;
                if (departmentManager.Update(d.Result) > 0)
                {

                    OkViewModel ntfobj = new OkViewModel()
                    {
                        Title = "Update Successful",
                        RedirectingUrl = "/Home/ListDepartment",
                        RedirectingTimeout = 1000

                    };
                    ntfobj.Items.Add("Department update was successful..");
                    return View("Ok", ntfobj);
                }
                else
                {
                    d.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
            }
            return View();
        }






        public ActionResult EmployeeDetails(int? id)
        {
            if (id == null)
            {
                //Eksik parametre hata sayfası gösterilecek
                return RedirectToAction("ListEmployee");
            }
            else
            {
                Employee e = employeeManager.Find(x => x.EmployeeID == id.Value);
                return View(e);
            }

        }

        public ActionResult DepartmentDetails(int? id)
        {
            if (id == null)
            {
                //Eksik parametre hata sayfası gösterilecek
                return RedirectToAction("ListDepartment");
            }
            else
            {
                Department d = departmentManager.Find(x => x.DepartmentId == id.Value);
                return View(d);
            }

        }

        public ActionResult ListEmployee()
        {
            List<Employee> employees = employeeManager.List();
            return View(employees);
        }

        public ActionResult ListDepartment()
        {
            if (Session["login"]!=null)
            {
                List<Department> departments = departmentManager.List();
                return View(departments);
            }
            else
            {
                ErrorViewModel ntfobj = new ErrorViewModel()
                {
                    Title = "Error",
                    RedirectingUrl = "/Home/ListEmployee/",
                    RedirectingTimeout = 1800
                };
                ErrorMessageObj obj = new ErrorMessageObj(ErrorMessageCode.UnauthorizedRequest, "Unauthorized Request");
                ntfobj.Items.Add(obj);
                return View("Error", ntfobj);
            }
            
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("AdminLogin");
        }
    }
}