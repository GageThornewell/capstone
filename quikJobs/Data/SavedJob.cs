using System;
using System.Collections.Generic;

namespace quikJobs.Data;

public partial class SavedJob
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int JobId { get; set; }

    public DateOnly? SaveDate { get; set; }

    public virtual Job Job { get; set; } = null!;
}
