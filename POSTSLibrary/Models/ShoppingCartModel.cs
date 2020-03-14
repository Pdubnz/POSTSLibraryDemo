using System;
using System.Collections.Generic;
using System.Text;

namespace POSTSLibrary.Models
{
    public class ShoppingCartModel
    {
        public List<CartItemModel> Items { get; set; }
        public ShoppingCartModel()
        {
            this.Items = new List<CartItemModel>();
        }
    }
}
