using System;
using System.Collections.Generic;

namespace Lab3_Shop_Rukoleev.Models;

public partial class Order
{
    public int Id { get; set; }

    public double? Number { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? PickupPointId { get; set; }

    public int? UserId { get; set; }

    public double? Code { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<OrderArticle> OrderArticles { get; set; } = new List<OrderArticle>();
}
