using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxellers.ObjectPooling;

namespace Voxellers.ObjectSpawning
{
    public class ObjectSpawner : MonoSingleton<ObjectSpawner>
    {
        private Dictionary<GameObject, string> _objectKeyDict;

        private void Awake()
        {
            _objectKeyDict = new Dictionary<GameObject, string>();
        }

        public GameObject Spawn(string key, Vector3 position, Quaternion rotation)
        {
            GameObject spawningObject = ObjectPoolingManager.Instance.GetObject(key);
            spawningObject.transform.position = position;
            spawningObject.transform.rotation = rotation;
            _objectKeyDict.Add(spawningObject, key);

            return spawningObject;
        }

        public GameObject SpawnRelative(string key, Vector3 targetPosition, Vector3 additionalPos, Quaternion rotation)
        {
            return null;
        }

        public GameObject SpawnByMarkerPosition(string key, string markerKey)
        {
            return null;
        }

        public void Return(GameObject returningObject)
        {
            SpawnableObject spawnableObject = returningObject.GetComponent<SpawnableObject>();
            if (spawnableObject)
            {
                spawnableObject.ExecuteReturnActions(() =>
                {
                    string key = _objectKeyDict[returningObject];
                    ObjectPoolingManager.instance.ReturnObject(key, returningObject);
                });
            }
            else
            {
                string key = _objectKeyDict[returningObject];
                ObjectPoolingManager.instance.ReturnObject(key, returningObject);
            }
        }
    }
}