using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSearch.Models;
using JobSearch.ViewModel;

namespace JobSearch.Controllers
{
    
    public class HomeController : Controller

    {
        JobSearchDbEntities db = new JobSearchDbEntities();
        EmployerModel em = new EmployerModel();
        SingleModel sm = new SingleModel();
        public ActionResult Index()
        {
            em._employer = db.Employers.ToList();
            em._city = db.Cities.ToList();
            em._degree = db.Education_degrees.ToList();
            em._work = db.Work_experience.ToList();
            em._category = db.Categories.ToList();
            return View(em);
        }
        public ActionResult Single(int id)
        {
            sm._emp = db.Employers.Where(m => m.id == id).FirstOrDefault();
            return View(sm);
        }

        public ActionResult EmployerSearch(string search_cat, string search_cit, string search_sal, string search_deg, string search_wor)
        {
            IEnumerable<Employer> emp_list = db.Employers.Where(e => e.Category.category_name == search_cat || e.City.city_name==search_cit || e.Education_degrees.degree_name==search_deg || e.minimum_salary.ToString()==search_sal || e.Work_experience.experience_interval==search_wor ).ToList();
            ViewBag.Count = db.Employers.Where(p => p.Category.category_name == search_cat || p.City.city_name == search_cit || p.Education_degrees.degree_name == search_deg || p.minimum_salary.ToString() == search_sal || p.Work_experience.experience_interval == search_wor).Count();
            return View(emp_list);
        }
    }
}