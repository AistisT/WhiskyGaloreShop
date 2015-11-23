using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhiskyGalore.Models
{
    public class ShoppingCart
    {
        
        public string shoppingCartId { get; set; }
        public LinkedList<CartItem> items = new LinkedList<CartItem>();
        public int totalItemCount { get; set; }
        public decimal totalCartCost { get; set; }
        public float totalCartWeight { get; set; }
        
        public ShoppingCart(){}
        public ShoppingCart (string cartId)
        {
            
            shoppingCartId = cartId;
           
        }
        
        public void addToCart(int productId, decimal unitPrice, string productName, float productWeight)
        {
            // Get the matching cart and product instances
            if (items.Count() != 0)
            {
                var currentNode = items.First;
                while (currentNode != null)
                {
                    if (currentNode.Value.productId == productId)
                    {
                        currentNode.Value.count++;
                        currentNode.Value.setTotalPriceAndWeight();
                        break;
                    }
                   
                    else
                    {
                        currentNode = currentNode.Next;
                        if (currentNode == null)
                        {
                            CartItem newItem = new CartItem(productId, unitPrice, productName, productWeight);
                            newItem.setTotalPriceAndWeight();
                            items.AddFirst(newItem);
                            break;
                        }
                    }
                }
            }

            else
            {
                CartItem newItem = new CartItem(productId, unitPrice, productName, productWeight);
                newItem.setTotalPriceAndWeight();
                items.AddFirst(newItem);
            }
        }

        public void removeFromCart(int productId)
        {
            var currentNode = items.First;
            while (currentNode != null)
            {
                if (currentNode.Value.productId == productId)
                {
                    if (currentNode.Value.count > 1)
                    {
                        currentNode.Value.count--;
                        currentNode.Value.setTotalPriceAndWeight();
                        break;
                    }
                    else if (currentNode.Value.count < 2)
                    {
                        currentNode.Value.count--;
                        currentNode.Value.setTotalPriceAndWeight();
                        var toRemove = currentNode;
                        currentNode = currentNode.Next;
                        items.Remove(toRemove);
                    }
                }
                else
                {
                    currentNode = currentNode.Next;
                }
            }
        }
            
           

        public void emptyCart()
        { 
            foreach (var item in items)
            {
                items.Remove(item);
            }
           
        }

       public void setTotals()
        {
            totalItemCount = 0;
            totalCartCost = 0;
            totalCartWeight = 0;
            foreach (var item in items)
            {
                totalItemCount += item.count;
                totalCartCost += item.totalPrice;
                totalCartWeight += item.totalWeight;
            }
        }
        
    }
}