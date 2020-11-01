using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameDataModels;

public class UIManager : MonoBehaviour
{
    public static UIManager gInstance;

    public GameManager GameManager;
    public TextMeshProUGUI CurrencyElement;

    private void Awake()
    {
        if (gInstance == null)
        {
            gInstance = this;
        }
        else if (gInstance != this)
        {
            Debug.Log("Instance already exists, reallocating singleton (UIManager).");
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        CurrencyElement.text = "Currency: " + GameManager.GetPlayerMoney(CurrencyType.STANDARD);
    }

    public void ConnectToServer()
    {
        //Client.gInstance.ConnectToServer();
    }
}
