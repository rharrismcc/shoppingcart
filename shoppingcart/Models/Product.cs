using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoppingcart.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int QtyOnHand { get; set; }


        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}