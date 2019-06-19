using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class Cart
    {
            private List<CartLine> lineCollection = new List<CartLine>();

            public void AddItem(Product prod, int quantity)
            {
                CartLine line = lineCollection
                    .Where(p => p.Product.Id == prod.Id)
                    .FirstOrDefault();

                if (line == null)
                {
                    lineCollection.Add(new CartLine
                    {
                        Product = prod,
                        Quantity = quantity
                    });
                }
                else
                {
                    line.Quantity += quantity;
                }
            }

            public void RemoveLine(Product prod)
            {
                lineCollection.RemoveAll(l => l.Product.Id == prod.Id);
            }

            public decimal ComputeTotalValue()
            {
                return lineCollection.Sum(e => e.Product.Price * e.Quantity);

            }
            public void Clear()
            {
                lineCollection.Clear();
            }

            public IEnumerable<CartLine> Lines
            {
                get { return lineCollection; }
            }
        }

     public class CartLine
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    
}