using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameDataModels
{
    public class Product : MonoBehaviour
    {
        public double Price { set; get; }
        public ProductType Type { set; get; }
        public Product(ProductType productType)
        {
            Type = productType;
            Price = 0;
        }
    }
}
