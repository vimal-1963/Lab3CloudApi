using System;
using System.Collections.Generic;

namespace MVCApplication.Models;

public partial class LeadDetail
{
    public int LeadId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }
}
