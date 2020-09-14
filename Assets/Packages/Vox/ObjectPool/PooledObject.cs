using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Voxellers.ObjectPooling
{
    public interface IPooled
    {
        void ResetObject();
    }

    public class PooledObject : MonoBehaviour, IPooled
    {
        public UnityEvent OnObjectReset = new UnityEvent();

        public void ResetObject()
        {
            OnObjectReset.Invoke();
        }

        private void OnDisable()
        {
            gameObject.SetActive(false);
        }
    }
}