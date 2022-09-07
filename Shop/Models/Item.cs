using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int? SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}
