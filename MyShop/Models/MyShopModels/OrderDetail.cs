using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.MyShopModels
{
    public class OrderDetail
    {
        public int Id { get; set; }
       
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderQTY { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductSum { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public OrderDetail(int productid, int orderQTY, decimal productPrice, decimal ProductSum)
        {
        this.ProductId = productid;
        this.OrderQTY = orderQTY;
        this.ProductPrice = productPrice;
        this.ProductSum = ProductSum;
        }
        public OrderDetail()
        {
           
        }

    }

    
}
