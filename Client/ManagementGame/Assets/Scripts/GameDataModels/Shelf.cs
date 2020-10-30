using GameUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataModels
{


    public class Shelf : MonoBehaviour
    {
        private const int MAX_STOCK_ON_SHELF = 12;

        private class StockOnShelf
        {
            public Transform SpawnPoint { get; private set; }
            public GameObject SpawnedProduct { get; private set; }
            public int SpawnPointIndex { get; private set; }

            public StockOnShelf(Transform spawnPoint, GameObject productObject, int index)
            {
                SpawnPoint = spawnPoint;
                SpawnedProduct = productObject;
                SpawnPointIndex = index;
            }

            public void DeleteProduct()
            {
                Destroy(SpawnedProduct);
            }
        }

        public GameObject ProductSpawnable;
        public ProductType ProductType;
        public int CurrentStock;

        private List<StockOnShelf> mProductsOnShelf = new List<StockOnShelf>(MAX_STOCK_ON_SHELF);
        /// <summary>
        /// Will instantiate all the product on the shelf. TODO: This will all be changed once employee systems and stockers are made.
        /// </summary>
        private void Start()
        {
            StockShelf(true);
        }

        private void FixedUpdate()
        {
            if(CurrentStock <= 0)
            {
                StockShelf();
            }
        }

        /// <summary>
        /// Restock all the game object products on this shelf and reset the stock.
        /// </summary>
        private void StockShelf(bool initialStock = false)
        {
            int newStock = 0;
            List<Transform> spawnPoints = this.gameObject.FindComponentsOfChildrenWithTag<Transform>(GameTags.SPAWN_TAG);
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if (initialStock)
                {
                    mProductsOnShelf.Add(new StockOnShelf(spawnPoints[i].transform,
                        Instantiate(ProductSpawnable, spawnPoints[i].transform.position, new Quaternion()),
                        i));
                }
                else
                {
                    mProductsOnShelf[i] = new StockOnShelf(spawnPoints[i].transform,
                        Instantiate(ProductSpawnable, spawnPoints[i].transform.position, new Quaternion()),
                        i);
                }
                newStock++;
            }
            CurrentStock = newStock;
        }

        /// <summary>
        /// Randomly grab one of the items off of the shelf. Decrementing current stock and deleting the game object 
        /// representing the product.
        /// </summary>
        public bool TryToGrabSomethingOffShelf()
        {
            if (CurrentStock > 0)
            {
                //Get a list of indices of all active products.
                List<int> activeProductIndices = new List<int>(MAX_STOCK_ON_SHELF);
                for (int i = 0; i < mProductsOnShelf.Count; i++)
                {
                    if (mProductsOnShelf[i].SpawnedProduct != null)
                    {
                        activeProductIndices.Add(mProductsOnShelf[i].SpawnPointIndex);
                    }
                }

                //Randomly delete one of them or restock the shelf.
                if (activeProductIndices.Count > 0)
                {
                    CurrentStock--;
                    mProductsOnShelf[activeProductIndices[RandomUtils.gRandom.Next(0, activeProductIndices.Count)]].DeleteProduct();
                    return true;
                }
            }
            return false;
        }
    }
}
