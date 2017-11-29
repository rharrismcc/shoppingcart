using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace shoppingcart.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int QtyOnHand { get; set; }



        public string Title { get; set; }

        public string AltText { get; set; }

        public string Caption { get; set; }


        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }


        [DataType(DataType.Upload)]
        public  HttpPostedFileBase  ImageUpload { get; set; }
    }
}