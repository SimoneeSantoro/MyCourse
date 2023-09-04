using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using MyCourse.Models.ValueObjects;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Entities
{
    public partial class Course
    {
        public Course(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("The course must have the title");
            }
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("The course must have the author");
            }

            Title = title;
            Author = author;

            Lessons = new HashSet<Lesson>();
        }

        public long Id { get;  private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Author { get; private set; }
        public string Email { get; private set; }
        public double Rating { get; private set; }
        public Money FullPrice { get; private set; }
        public Money CurrentPrice { get; private set; }

        public void ChangeTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("The course must have the title");
            }
            Title = newTitle;
        }

        public void ChangePrices(Money newFullPrice, Money newCurrentPrice)
        {
        if (newFullPrice == null || newCurrentPrice == null)
        {
            throw new ArgumentException("Prices can't be null");
        }
        if (newFullPrice.Currency != newCurrentPrice.Currency)
        {
            throw new ArgumentException("Currencies don't match");
        }
        if (newFullPrice.Amount < newCurrentPrice.Amount)
        {
            throw new ArgumentException("Full price can't be less than the current price");
        }
        FullPrice = newFullPrice;
        CurrentPrice = newCurrentPrice;
        }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
