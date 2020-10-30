using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDataModels;

public class GameManager : MonoBehaviour
{
    private PlayerData mPlayerData;
    private CustomerManager mCustomerManager;
    private StockManager mStockManager;
    private UIManager mUIManager;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate PlayerData
        mPlayerData = new PlayerData();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
