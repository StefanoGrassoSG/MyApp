using System;
using System.Collections.Generic;

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

    public decimal? FullPriceAmount { get; set; }

    public string FullPriceCurrency { get; set; } = null!;

    public decimal? CurrentPriceAmount { get; set; }

    public string CurrentPriceCurrency { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
