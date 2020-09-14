using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject newInstance = new GameObject("[Singleton] " + typeof(T).ToString(), typeof(T));
                        instance = newInstance.GetComponent<T>();
                    }
                }

                return instance;
            }
        }
    }
}

