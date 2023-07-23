using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCourse.Models.ViewModels
{
    public class CourseViewModel
    {
        public int idCourse {get; set;}
        public string title{get; set;}
        public string imagePath{get; set;}
        public string author{get; set;}
        public double rating{get; set;}
        public decimal fullPrice{get; set;}
        public decimal currentPrice{get; set;}
    }
}