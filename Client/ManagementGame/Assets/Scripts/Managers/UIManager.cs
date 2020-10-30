using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager gInstance;

    public GameObject StartMenu;
    public InputField UsernameField;

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

    public void ConnectToServer()
    {
        StartMenu.SetActive(false);
        UsernameField.interactable = false;
        Client.gInstance.ConnectToServer();
    }
}
