using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobSearch.Models;

namespace JobSearch.ViewModel
{
    public class EmployerModel
    {
        public List<Employer> _employer { get; set; }
        public List<City> _city { get; set; }
        public List<Category> _category { get; set; }
        public List<Education_degrees> _degree { get; set; }
        public List<Work_experience> _work { get; set; }
    }
}