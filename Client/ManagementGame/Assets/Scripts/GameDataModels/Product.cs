using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameDataModels
{
    public class Product
    {
        public int Amount { set; get; }
        public int Price { set; get; }
        public ProductType Type { set; get; }
        public Product(ProductType productType, int amount = 0, int price = 1)
        {
            Type = productType;
            Amount = amount;
            Price = price;
        }
    }
}
