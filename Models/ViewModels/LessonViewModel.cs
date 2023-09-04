using System;
using System.Data;
using MyCourse.Models.Entities;


namespace MyCourse.Models.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        internal static LessonViewModel FromDataRow(DataRow lessonRow)
        {
            var lessonViewModel = new LessonViewModel
            {
                Id = Convert.ToInt32(lessonRow["Id"]),
                Title = Convert.ToString(lessonRow["Title"]),
                Description = Convert.ToString(lessonRow["Description"]),
                Duration = TimeSpan.Parse(Convert.ToString(lessonRow["Duration"]))
            };
            return lessonViewModel;
        }
        public static LessonViewModel FromEntity(Lesson lesson)
        {
            return new LessonViewModel
            {
                Id = Convert.ToInt32(lesson.Id),
                Title = lesson.Title,
                Description = lesson.Description,
                Duration = lesson.Duration
            };
        }

    }
}