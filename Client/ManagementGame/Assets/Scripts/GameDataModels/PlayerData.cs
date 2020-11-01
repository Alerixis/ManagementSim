using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataModels
{
    public class PlayerData
    {
        private const long STARTING_CASH = 3000; 

        public string StoreName { set; get; }
        public string ManagerName { set; get; }

        private long[] mCurrentCurrency = new long[Enum.GetValues(typeof(CurrencyType)).Length];

        public PlayerData()
        {
            mCurrentCurrency[(int)CurrencyType.STANDARD] = STARTING_CASH;
        }

        /// <summary>
        /// Retrieves the given currency type amount on the player.
        /// </summary>
        /// <param name="currencyType"></param>
        /// <returns></returns>
        public long GetCurrencyTypeAmount(CurrencyType currencyType)
        {
            return mCurrentCurrency[(int)currencyType];
        }

        /// <summary>
        /// Changes the given currency type on that player data by the amount provided.
        /// </summary>
        /// <param name="currencyType"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public void AddCurrency(CurrencyType currencyType, long amount)
        {
            mCurrentCurrency[(int)currencyType] += amount;
        }
        
        /// <summary>
        /// Retrieves the given stock type amount on the player.
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public int GetStockTypeAmount(ProductType productType)
        {
            return 1;// mCurrentStock[(int)productType].Amount;
        }

        /// <summary>
        /// Changes the give stock type on that player data by the amount provided.
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public void AddStock(ProductType productType, int amount)
        {
            //mCurrentStock[(int)productType].Amount += amount;
        }
    }
}
