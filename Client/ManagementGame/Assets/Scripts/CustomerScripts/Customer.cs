using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataModels;
using UnityEngine;
using UnityEngine.AI;
using GameUtils;

namespace CustomerScripts
{
    public class Customer : MonoBehaviour
    {
        private enum CustomerState
        {
            GetShelf,
            Moving,
            Waiting,
            Purchasing,
            GoToRegister,
            MovingToRegister,
            Checkout,
            Leaving,
        }

        public int StarRating { get; private set; }
        public int MoneyLimit { get; private set; }
        public int MaxPurchaseLimit { get; private set; }
        public int MaxPurchaseAttempts { get; private set; }
        public ProductType MostWantedProduct { get; private set; }

        private CustomerManager mCustomerManager;
        private int mPurchaseChance;
        private int mItemsGrabbed;
        private NavMeshAgent mNavAgent;
        private Shelf mTargetShelf;
        private CustomerState mCustomerState;
        private Dictionary<ProductType, int> mInventory = new Dictionary<ProductType, int>();
        private float mWaitToGrabItemTime;
        private int mPurchaseAttempts = 0;

        public Customer()
        {
            //TODO: Star Rating should be a weighted roll. Lower stars more likely than higher.
            //The star rating weight will also be used in the random rolls for money limit and buying cap.
            StarRating = RandomUtils.gRandom.Next(1, 6);
            MoneyLimit = 100;
            MaxPurchaseLimit = 6;
            MaxPurchaseAttempts = 10;
            mPurchaseChance = RandomUtils.gRandom.Next(40, 101);
            Array products = Enum.GetValues(typeof(ProductType));
            MostWantedProduct = (ProductType)products.GetValue(RandomUtils.gRandom.Next(0, products.Length));
            mCustomerState = CustomerState.GetShelf;
        }
        private void Awake()
        {
            mNavAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            Debug.Log(mCustomerState.ToString());
            if (!mNavAgent.pathPending && mNavAgent.remainingDistance <= 0.1)
            {
                if (mCustomerState == CustomerState.Moving)
                {
                    //Set a random wait at the shelf for 1 - 3 seconds for slight realism
                    mCustomerState = CustomerState.Waiting;
                    mWaitToGrabItemTime = RandomUtils.gRandom.Next(1, 3);
                }
                else if(mCustomerState == CustomerState.MovingToRegister)
                {
                    PurchaseItems();
                    LeaveStore();
                }
                else if(mCustomerState == CustomerState.Leaving)
                {
                    mCustomerManager.CustomerLeft(this);
                    Destroy(this.gameObject);
                }
            }

            if (mCustomerState == CustomerState.Waiting)
            {
                if(mWaitToGrabItemTime <= 0)
                {
                    mCustomerState = CustomerState.Purchasing;
                    RollToPurchaseFromShelf();
                }
                mWaitToGrabItemTime -= Time.deltaTime;
            }

            if (mCustomerState == CustomerState.Purchasing)
            {
                if (mItemsGrabbed == MaxPurchaseLimit || mPurchaseAttempts == MaxPurchaseAttempts)
                {
                    mCustomerState = CustomerState.GoToRegister;
                }
                else
                {
                    mCustomerState = CustomerState.GetShelf;
                }
            }

            if(mCustomerState == CustomerState.GoToRegister)
            {
                GoToRegister();   
            }
        }

        /// <summary>
        /// Needs game inventory and game manager to manage the purchase flow.
        /// </summary>
        private void PurchaseItems()
        {
            mCustomerManager.PurchaseItems(mInventory);
            return;
        }

        private void LeaveStore()
        {
            mCustomerState = CustomerState.Leaving;
            mNavAgent.SetDestination(mCustomerManager.CustomerExitLocation);
        }

        /// <summary>
        /// Roll to see if the customer wants to buy this item. If it's their favorite item,
        /// add 20% to the roll.
        /// </summary>
        private void RollToPurchaseFromShelf()
        {
            mPurchaseAttempts++;   
            int purchaseChance = mPurchaseChance;
            if(MostWantedProduct == mTargetShelf.ProductType)
            {
                purchaseChance += 20;
            }

            if(RandomUtils.gRandom.Next(1, 101) <= purchaseChance)
            {
                if (mTargetShelf.TryToGrabSomethingOffShelf())
                {
                    if (mInventory.ContainsKey(mTargetShelf.ProductType))
                    {
                        mInventory[mTargetShelf.ProductType]++;
                    }
                    else
                    {
                        mInventory.Add(mTargetShelf.ProductType, 1);
                    }
                    mItemsGrabbed++;
                }
                else
                {
                    //TODO: Customer gets mad as hell. Customer Happiness system.
                }
            }
        }

        private void GoToRegister()
        {
            mCustomerState = CustomerState.MovingToRegister;
            mNavAgent.SetDestination(mCustomerManager.CashierLocation);
        }

        /// <summary>
        /// Sets the target location and shelf for the customer to move to.
        /// </summary>
        /// <param name="shelf"></param>
        public void SetShelfToMoveTo(GameObject shelf)
        {
            mTargetShelf = shelf.GetComponent<Shelf>();
            mNavAgent.SetDestination(shelf.FindComponentInChildWithTag<Transform>(GameTags.NAV_LOC_TAG).position);
            mCustomerState = CustomerState.Moving;
        }

        /// <summary>
        /// Determines if the customer is ready for their next shelf destination.
        /// </summary>
        /// <returns></returns>
        public bool IsReadyForNextShelf()
        {
            return mCustomerState == CustomerState.GetShelf;
        }

        public void SetManager(CustomerManager manager)
        {
            mCustomerManager = manager;
        }
    }
}
