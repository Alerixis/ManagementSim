using System.Collections.Generic;
using UnityEngine;
using CustomerScripts;
using GameUtils;
using GameDataModels;

public class CustomerManager : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public int MinCustomerSpawnWait = 1;
    public int MaxCustomerSpawnWait = 3;
    public int MaxCustomersInStore = 10;
    public GameManager GameManager;

    public Vector3 CashierLocation { get; private set; }
    public Vector3 CustomerExitLocation { get; private set; }

    private List<Customer> mActiveCustomers = new List<Customer>();
    private float mNextCustomerTimer = 0f;
    private GameObject[] mShelves;

    private Vector3 mCustomerSpawnLocation;

    /// <summary>
    /// Grab the locations for spawn, exit, and cashier
    /// </summary>
    private void Start()
    {
        //Gather all the vector3 pos's we need for intelligent spawning and movement.
        GameObject entrance = GameObject.FindGameObjectWithTag(GameTags.ENTRANCE_TAG);
        mCustomerSpawnLocation = GameObjectExtensions.FindComponentInChildWithTag<Transform>(entrance, GameTags.SPAWN_TAG).position;
        CustomerExitLocation = GameObjectExtensions.FindComponentInChildWithTag<Transform>(entrance, GameTags.NAV_LOC_TAG).position;
        CashierLocation = GameObject.FindGameObjectWithTag(GameTags.CASHIER_TAG).FindComponentInChildWithTag<Transform>(GameTags.NAV_LOC_TAG).position;
        mShelves = GameObject.FindGameObjectsWithTag(GameTags.SHELF_TAG);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < mActiveCustomers.Count; i++)
        {
            if (mActiveCustomers[i].IsReadyForNextShelf())
            {
                mActiveCustomers[i].SetShelfToMoveTo(mShelves[RandomUtils.gRandom.Next(0, mShelves.Length)]);
            }
        }

        if (mNextCustomerTimer <= 0)
        {
            mNextCustomerTimer = RandomUtils.gRandom.Next(MinCustomerSpawnWait, MaxCustomerSpawnWait);
            if (mActiveCustomers.Count < MaxCustomersInStore)
            {
                GameObject newCustomer = Instantiate(CustomerPrefab, mCustomerSpawnLocation, new Quaternion());
                Customer customer = newCustomer.GetComponent<Customer>();
                customer.SetManager(this);
                mActiveCustomers.Add(customer);
            }
        }
        mNextCustomerTimer -= Time.deltaTime;
        
    }

    public void CustomerLeft(Customer customer)
    {
        for(int i = 0; i < mActiveCustomers.Count; i++)
        {
            if(mActiveCustomers[i] == customer)
            {
                mActiveCustomers.RemoveAt(i);
                break;
            }
        }
    }

    public void PurchaseItems(Dictionary<ProductType, int> purchases)
    {
        
    }
}
