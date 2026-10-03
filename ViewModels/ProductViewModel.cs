using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Lab3_Shop_Rukoleev.ViewModels
{
    public class ProductViewModel
    {  
        public int Id { get; set; }
        public string Article { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string Supplier { get; set; } = "";
        public double Price { get; set; }
        public double Count { get; set; }
        public double Discount { get; set; }
        public string Unit { get; set; } = "";
        public string ImagePath { get; set; } = "";
        // Нужно для подсветки
        public bool HasHighDiscount => Discount > 20;
        public string DiscountText => $"{Discount:0.#}%";
        public string PriceText => $"{Price:0.00} ₽";
        public string CountText => $"{Count:0.##} {Unit}";
    }
}
