using System;
using System.Collections.Generic;

namespace quikJobs.Data;

public partial class Job
{
    public int JobId { get; set; }

    public string? UserId { get; set; }

    public DateOnly? CreatedOn { get; set; }

    public int? Views { get; set; }

    public string? Description { get; set; }

    public decimal? Pay { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();
}
