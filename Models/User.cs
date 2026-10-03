using System;
using System.Collections.Generic;

namespace Lab3_Shop_Rukoleev.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }
}
