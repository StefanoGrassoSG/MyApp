using System;
using System.Collections.Generic;
using WebAppCourse.Models.ValueTypes;

namespace WebAppCourse.Models.Entities;

public partial class Course
{
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
