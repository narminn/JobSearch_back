using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobSearch.Models;

namespace JobSearch.Controllers
{
    public class EmployersController : Controller
    {
        private JobSearchDbEntities db = new JobSearchDbEntities();

        // GET: Employers
        public ActionResult Index()
        {
            var employers = db.Employers.Include(e => e.Category).Include(e => e.City).Include(e => e.Education_degrees).Include(e => e.Gender).Include(e => e.Marriage_status).Include(e => e.Work_experience);
            return View(employers.ToList());
        }

        // GET: Employers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // GET: Employers/Create
        public ActionResult Create()
        {

            ViewBag.category_id = new SelectList(db.Categories, "id", "category_name");
            ViewBag.city_id = new SelectList(db.Cities, "id", "city_name");
            ViewBag.education_degree_id = new SelectList(db.Education_degrees, "id", "degree_name");
            ViewBag.gender_id = new SelectList(db.Genders, "id", "gender_name");
            ViewBag.marriage_status_id = new SelectList(db.Marriage_status, "id", "status_name");
            ViewBag.work_experience_id = new SelectList(db.Work_experience, "id", "experience_interval");
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,fathername,date_of_birth,gender_id,marriage_status_id,image,education_degree_id,education_institution,education_faculty,work_experience_id,work_experience_about,category_id,position,city_id,minimum_salary,lang_skills,other_skills,addition_information,email,phone1,phone2,address")] Employer employer, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, 100000);
                    var filename = System.IO.Path.GetFileName(image.FileName);
                    var src = Path.Combine(Server.MapPath("~/Uploads/"), randomNumber + filename);
                    image.SaveAs(src);
                    employer.image = "/Uploads/" + randomNumber + filename;


                    db.Employers.Add(employer);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");

                }

            }
            catch (Exception)
            {
                ViewBag.Error = "don't empty with * sections";
            }

            ViewBag.category_id = new SelectList(db.Categories, "id", "category_name", employer.category_id);
            ViewBag.city_id = new SelectList(db.Cities, "id", "city_name", employer.city_id);
            ViewBag.education_degree_id = new SelectList(db.Education_degrees, "id", "degree_name", employer.education_degree_id);
            ViewBag.gender_id = new SelectList(db.Genders, "id", "gender_name", employer.gender_id);
            ViewBag.marriage_status_id = new SelectList(db.Marriage_status, "id", "status_name", employer.marriage_status_id);
            ViewBag.work_experience_id = new SelectList(db.Work_experience, "id", "experience_interval", employer.work_experience_id);
            return View(employer);
       
            
        }

        // GET: Employers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "category_name", employer.category_id);
            ViewBag.city_id = new SelectList(db.Cities, "id", "city_name", employer.city_id);
            ViewBag.education_degree_id = new SelectList(db.Education_degrees, "id", "degree_name", employer.education_degree_id);
            ViewBag.gender_id = new SelectList(db.Genders, "id", "gender_name", employer.gender_id);
            ViewBag.marriage_status_id = new SelectList(db.Marriage_status, "id", "status_name", employer.marriage_status_id);
            ViewBag.work_experience_id = new SelectList(db.Work_experience, "id", "experience_interval", employer.work_experience_id);
            return View(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,fathername,date_of_birth,gender_id,marriage_status_id,image,education_degree_id,education_institution,education_faculty,work_experience_id,work_experience_about,category_id,position,city_id,minimum_salary,lang_skills,other_skills,addition_information,email,phone1,phone2,address")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "category_name", employer.category_id);
            ViewBag.city_id = new SelectList(db.Cities, "id", "city_name", employer.city_id);
            ViewBag.education_degree_id = new SelectList(db.Education_degrees, "id", "degree_name", employer.education_degree_id);
            ViewBag.gender_id = new SelectList(db.Genders, "id", "gender_name", employer.gender_id);
            ViewBag.marriage_status_id = new SelectList(db.Marriage_status, "id", "status_name", employer.marriage_status_id);
            ViewBag.work_experience_id = new SelectList(db.Work_experience, "id", "experience_interval", employer.work_experience_id);
            return View(employer);
        }

        // GET: Employers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // POST: Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employer employer = db.Employers.Find(id);
            db.Employers.Remove(employer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
