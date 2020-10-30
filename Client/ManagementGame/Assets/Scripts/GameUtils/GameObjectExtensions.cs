using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtils
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Finds a child with the given tag. However, will return null if one is not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            Transform parentTransform = parent.transform;
            foreach(Transform childTransform in parentTransform)
            {
                if (childTransform.tag == tag) 
                {
                    return childTransform.GetComponent<T>();
                }
            }
            return null;
        }

        /// <summary>
        /// Returns all components in children with the given tag. Will return empty list if not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<T> FindComponentsOfChildrenWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            List<T> foundComponents = new List<T>();
            Transform parentTransform = parent.transform;
            foreach(Transform childTransform in parentTransform)
            {
                if(childTransform.tag == tag)
                {
                    foundComponents.Add(childTransform.GetComponent<T>());
                }
            }
            return foundComponents;
        }
    }
}
