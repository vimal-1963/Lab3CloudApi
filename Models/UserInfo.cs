using System;
using System.Collections.Generic;

namespace MVCApplication.Models;

public partial class UserInfo
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Pwd { get; set; }
}
