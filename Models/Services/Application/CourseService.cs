using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class CourseService
    {
        public List<CourseViewModel> GetCourses()
        {
            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for(int i=1; i<=20; i++)
            {
                var price = Convert.ToDecimal(Math.Round(rand.NextDouble()* 10 + 10, 2));
                var courses = new CourseViewModel
                {
                  idCourse = i,
                  title = $"Corso numero {i}",
                  currentPrice = price - (Math.Round(price/2, 2)),
                  fullPrice = price,
                  author = "Nome Cognome",
                  rating = rand.NextDouble()*5.0,
                  imagePath = "/logo.svg"
                };
                courseList.Add(courses);
            }
            return courseList;
        }
    }
}