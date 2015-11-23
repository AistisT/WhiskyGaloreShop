using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiskyGalore.Models
{
    public class CartItem
    {
        
        public int productId { get; set; } 
        public int count { get; set; }
        public float weight { get; set; }
        public float totalWeight { get; set; }
        public decimal totalPrice { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }



        public CartItem(int productId, decimal unitPrice, string productName, float productWeight)
        {
            this.productId = productId;
            this.price = unitPrice;
            this.name = productName;
            this.weight = productWeight; 
            count = 1;
           
        }

        public void setTotalPriceAndWeight()
        {
            
            totalPrice = price * count;
            totalWeight = weight * count;
        }
       


    }
}