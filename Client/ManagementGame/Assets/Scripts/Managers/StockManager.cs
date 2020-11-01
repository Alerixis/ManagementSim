using GameDataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StockManager : MonoBehaviour
{
    private Dictionary<ProductType, Product> mStock;

    // Start is called before the first frame update
    private void Start()
    {
        mStock = new Dictionary<ProductType, Product>();
        foreach(ProductType value in Enum.GetValues(typeof(ProductType)))
        {
            mStock.Add(value, new Product(value));
        }
    }

    /// <summary>
    /// Add stock of the given amount to the players inventory.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AddStockToType(ProductType type, int amount)
    {
        if (mStock.ContainsKey(type))
        {
            mStock[type].Amount += amount;
        }
        else
        {
            mStock.Add(type, new Product(type, amount));
        }
    }

    /// <summary>
    /// Get the remaining stock of the given product.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetStockOfType(ProductType type)
    {
        if (mStock.ContainsKey(type))
        {
            return mStock[type].Amount;
        }
        Debug.LogError("No stock of type:" + type);
        return -1;
    }

    /// <summary>
    /// Gets the price of the given product. Returns -1 if one doesnt exist.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetPriceOfType(ProductType type)
    {
        if (mStock.ContainsKey(type))
        {
            return mStock[type].Price;
        }
        Debug.LogError("No stock of type: " + type);
        return -1;
    }

    /// <summary>
    /// Set the price of the given product.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="price"></param>
    public void SetPriceOfType(ProductType type, int price)
    {
        if (price >= 0)
        {
            if (mStock.ContainsKey(type))
            {
                mStock[type].Price = price;
                return;
            }
            Debug.LogError("Given type was not in the stock: " + type.ToString());
        }
        Debug.LogError("Cant set price below zero. Given price: " + price);
    }
}


