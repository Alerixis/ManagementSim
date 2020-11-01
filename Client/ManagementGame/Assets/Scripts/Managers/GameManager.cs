using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataModels;

public class GameManager : MonoBehaviour
{
    private PlayerData mPlayerData;
    private StockManager mStockManager;
    private UIManager mUIManager;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate PlayerData
        mPlayerData = new PlayerData();
        mStockManager = GetComponent<StockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyProducts(Dictionary<ProductType, int> purchases) 
    {
        foreach(KeyValuePair<ProductType, int> purchase in purchases)
        {
            int costOfItem = mStockManager.GetPriceOfType(purchase.Key);
            int profit = costOfItem * purchase.Value;

            mPlayerData.AddCurrency(CurrencyType.STANDARD, profit);
        }
        Debug.Log(mPlayerData.GetCurrencyTypeAmount(CurrencyType.STANDARD));
    }
}
