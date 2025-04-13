using Interview1.Task.DAL;
using Interview1.Task.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interview1.Task.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDAL _dal;
        public StudentController(StudentDAL dal)
        {
            _dal = dal;
        }

        public ActionResult Index()
        {
            List<Student> studentList = _dal.GetAllStudents();
            return View(studentList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                _dal.AddStudent(student);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



    }
}
