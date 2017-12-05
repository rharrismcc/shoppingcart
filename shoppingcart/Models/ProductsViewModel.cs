using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoppingcart.Models
{
    public class ProductsViewModel
    {
        public IEnumerable<Product>  Products { get; set; }

        public PagingModel Pager { get; set; }
    }
}