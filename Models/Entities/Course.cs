using System;
using System.Collections.Generic;
using WebAppCourse.Models.ValueTypes;

namespace WebAppCourse.Models.Entities;

public partial class Course
{
    public Course(string title, string author)
    {
        if(string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Il corso deve avere un titolo");
        }
        if(string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Il corso deve avere un autore");
        }
        this.Title = title;
        this.Author = author;
        this.CurrentPrice = new Money(Enums.Currency.EUR, 0);
        this.FullPrice = new Money(Enums.Currency.EUR, 0);
        ImagePath = "/Courses/default.png";
    }
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImagePath { get; set; }

    public string Author { get; set; } = null!;

    public string? Email { get; set; }

    public decimal? Rating { get; set; }

    public Money FullPrice { get; set; }

    public Money CurrentPrice { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
