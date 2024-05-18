using System;
using System.Collections.Generic;

namespace WebAppCourse.Models.Entities;

public partial class Lesson
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Duration { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
