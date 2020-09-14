using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers
{
    public class VoxSingleton<T> : VoxType where T : VoxType
    {
        protected static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = System.Activator.CreateInstance(typeof(T)) as T;
                    instance.OnCreate();
                }

                return instance;
            }
        }
    }
}