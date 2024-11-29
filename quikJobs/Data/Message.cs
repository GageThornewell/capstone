using System;
using System.Collections.Generic;

namespace quikJobs.Data;

public partial class Message
{
    public int Id { get; set; }

    public string SenderId { get; set; } = null!;

    public string ReceiverId { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string? Message1 { get; set; }
}
