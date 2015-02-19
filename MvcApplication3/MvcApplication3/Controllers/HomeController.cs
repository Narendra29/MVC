using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practice.Models;
using System.Data.Entity;
using System.Data;
using MvcApplication3.Models;
using System.Text;

 

namespace MvcApplication3.Controllers
{
    public class HomeController : Controller
    {
        private UsersDbContext db = new UsersDbContext();
       
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Create()
        {
                     

            var listData = db.UanList.Select(c => new { c.UAN_ID, c.UAN_Number });

            int[] selectedItemsArray = listData.Select(s => s.UAN_ID).ToArray();
            ViewBag.List = new SelectList(listData.AsEnumerable(), "UAN_ID", "UAN_Number", selectedItemsArray);         
            ViewBag.Country = new SelectList(db.countrylist, "Country_ID", "Country_Name");   
       
           return View(); 
           
                
 
        }

        [HttpPost]
        public ActionResult EmployeeSave(Employement emp)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var abc = emp.UAN_ID1;

                    StringBuilder objStb = new StringBuilder(); 
                    Employement objemp = new Employement();  
                    foreach (string idd in abc)
                    {                       
                        objStb.Append(idd + ","); 
                    }
                    
                    objemp.Emp_Name = emp.Emp_Name;
                    objemp.Emp_address = emp.Emp_address;
                    objemp.salary = emp.salary;
                    objemp.Designation = emp.Designation;
                    objemp.Country_ID = emp.Country_ID;   
                    objemp.State_Id = emp.State_Id;
                    objemp.Active = emp.Active;    
                    objemp.UAN_ID = objStb.ToString().Substring(0,objStb.ToString().LastIndexOf(','));
                    objemp.Date = DateTime.Now;
                    db.Employeelist.Add(objemp);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString(); 
                }
            }
            return RedirectToAction("Registrationlist");

        }

        public JsonResult getstate(int countryId)
        {              

            int counId = Convert.ToInt32(countryId);
             
            var objstate = (from c in db.statelist where c.Country_ID == counId select c).ToList();
            return Json(objstate.Select(m => new SelectListItem() { Value = Convert.ToString(m.State_Id), Text = m.State_Name }), JsonRequestBehavior.AllowGet);
           
                           
            
        }

        public ActionResult Registrationlist()
        {         

            IEnumerable<Employement> EmplyoeeList = from employee in db.Employeelist select employee;
            return View("Employeelist", EmplyoeeList.ToList()); 

        }

        public ActionResult Emplist()
        {
            IEnumerable<Employement> EmplyoeeList = from employee in db.Employeelist select employee;
            return View("Emplist", EmplyoeeList.ToList()); 
        }

        public ActionResult JqueryDatatable()
        {
            IEnumerable<Employement> EmplyoeeList = from employee in db.Employeelist select employee;
            return View(EmplyoeeList.ToList()); 
        }
        public ActionResult Employeelistview(int id)
        {         

            Employement objemp = db.Employeelist.Find(id);
            return View(objemp);
        }

        public ActionResult EmployeelistEdit(int id)
        {
            Employement objemp = db.Employeelist.Find(id);
            objemp.CountryList = new SelectList(db.countrylist, "Country_ID", "Country_Name",objemp.Country_ID);
            ViewBag.state = new SelectList(db.statelist, "State_Id", "State_Name", objemp.State_Id); 
            return View(objemp);

        }     

       


        [HttpPost]
        public ActionResult EmployeeUpdate(Employement objemp)
        {
            try
            {
                objemp.Date = DateTime.Now; 
                db.Entry(objemp).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View(objemp);
            }
            return RedirectToAction("Registrationlist");
        }

        public ActionResult EmployeeDelete(int id)
        {
            Employement objemp = db.Employeelist.Find(id);
            db.Employeelist.Remove(objemp);
            db.SaveChanges();
            return RedirectToAction("RegistrationList");
        }


    }
}
