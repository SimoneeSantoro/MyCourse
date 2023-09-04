using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCourse.Models.Entities;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;

        public EfCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            IQueryable<CourseDetailViewModel> queryLinq = dbContext.Courses
                .AsNoTracking()
                .Include(course => course.Lessons)
                .Where(course => course.Id == id)
                .Select(course => CourseDetailViewModel.FromEntity(course));
                                                                                    // .FirstAsync(); restituisce il primo elemento dell'elenco, ma se l'elenco é vuoto allora solleva eccezione
                                                                                    // .SingleOrDefaultAsync(); tollera il fatto che l'elenco sia vuoto e in quel caso restituisce null, oppure se l'elenco contiene piú di un elemento da eccezione
                                                                                    // .FirstOrDefaultAsync(); restitusice null se l'elenco e vuoto e non solleva mai eccezione
                CourseDetailViewModel courseDetail = await queryLinq.SingleAsync(); // restituisce il primo elemento dell'elenco, ma se l'elenco ne contiene 0 o piú di 1 allora solleva eccezione

            return courseDetail;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses
                .AsNoTracking()
                .Select(course => CourseViewModel.FromEntity(course));

            List<CourseViewModel> courses = await queryLinq.ToListAsync(); // La query al db viene inviata da qui

            return courses;
        }
    }
}