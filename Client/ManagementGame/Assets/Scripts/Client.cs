using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using Shared.Constants;
using Shared;
using Assets;
using UnityEditor.PackageManager;

public class Client : MonoBehaviour
{
    public static Client gInstance;

    public string Ip = "127.0.0.1";
    public int Port = SharedConstants.SERVER_PORT;
    public int ID = 0;
    public ClientTcpConnection Connection;

    private void Awake()
    {
        if(gInstance == null)
        {
            gInstance = this;
        }
        else if (gInstance != this)
        {
            Debug.Log("Instance already exists, reallocating singleton.");
            Destroy(this);
        }
    }

    private void Start()
    {
        Connection = new ClientTcpConnection();
    }

    public void ConnectToServer()
    {
        Connection.Connect();
    }
}
