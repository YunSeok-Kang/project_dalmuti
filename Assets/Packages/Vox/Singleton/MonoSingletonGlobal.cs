using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers
{
    public class MonoSingletonGlobal<T> : MonoBehaviour where T : MonoBehaviour
        //public class MonoSingletonGlobal<T> : MonoSingleton<T> where T : MonoBehaviour
    {
        protected static T instance = null;
        public static T Instance
        {
            get
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    GameObject newInstance = new GameObject("[Singleton] " + typeof(T).ToString(), typeof(T));
                    instance = newInstance.GetComponent<T>();

                    DontDestroyOnLoad(instance);
                }

                return instance;
            }
        }

        public virtual void StartSingleton()
        {

        }
    }
}