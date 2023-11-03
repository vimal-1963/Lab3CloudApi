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

    public UserInfo() { }

    public UserInfo(int IdArg, string FirstNameArg, string LastNameArg, string EmailArg, string pwdArg) {
        this.Id = IdArg;
        this.FirstName = FirstNameArg;  
        this.LastName = LastNameArg;    
        this.Email = EmailArg;  
        this.Pwd = pwdArg;  
    
    }
}
