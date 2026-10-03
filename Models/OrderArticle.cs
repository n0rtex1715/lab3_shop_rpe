using System;
using System.Collections.Generic;

namespace Lab3_Shop_Rukoleev.Models;

public partial class OrderArticle
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public double? Count { get; set; }

    public int? ProductId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
