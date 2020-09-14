using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers.ObjectPooling
{
    public class ObjectPoolingManager : MonoSingleton<ObjectPoolingManager>
    {
        [SerializeField]
        private List<ObjectPooler> _objectPools;

        /// <summary>
        /// n초마다 사용하지 않는 오브젝트를 찾아 오브젝트 반납
        /// </summary>
        public float collectionTimeInterval = 0.1f;

        public GameObject GetObject(string poolKey)
        {
            ObjectPooler objectPool = FindPoolerByKey(poolKey);
            if (objectPool == null) // 생성된 풀이 없는 경우, 새로 생성해줌. // 게임 중 해당 스크립트가 실행되면 렉이 걸릴 가능성이 있음.
            {
                Debug.LogWarning("ObjectPoolingManager: 게임이 실행 도중에 새롭게 풀링 오브젝트를 할당하였습니다. 성능 저하의 원인이 될 수 있습니다.");

                ObjectPooler newPool = new ObjectPooler();
                newPool.SpawningParent = gameObject.transform;
                newPool.MakeNew();
                _objectPools.Add(newPool);

                objectPool = newPool;
            }

            GameObject gettingObj = objectPool.GetNew().gameObject;
            return gettingObj;
        }

        public void ReturnObject(string poolKey, GameObject gameObject)
        {
            ObjectPooler objectPool = FindPoolerByKey(poolKey);
            PooledObject pooledObj = gameObject.GetComponent<PooledObject>(); // GetComponent가 이 함수에 들어가있으면 안됨.
            if (pooledObj == null)
            {
                Debug.LogError("ObjectPoolingManager - ReturnObject(): PooledObject 컴포넌트를 찾지 못했습니다.");
                return;
            }

            objectPool.Return(pooledObj);
        }

        public GameObject GetObject(string poolKey, Vector3 position, Quaternion rotation)
        {
            GameObject gettingObj = GetObject(poolKey);
            gettingObj.transform.position = position;
            gettingObj.transform.rotation = rotation;

            return gettingObj;
        }

        private void Start() // 타이밍 Awake로 옮겨줄 필요가 있음.
        {
            foreach (ObjectPooler objPool in _objectPools)
            {
                objPool.SpawningParent = gameObject.transform;
                objPool.MakeNew();
            }

            StartCoroutine("CollectPool");
        }

        private IEnumerator CollectPool()
        {
            while (this.enabled)
            {
                yield return new WaitForSeconds(collectionTimeInterval);

                for (int poolIndex = 0; poolIndex < _objectPools.Count; poolIndex++)
                {
                    ObjectPooler currentPool = _objectPools[poolIndex];
                    currentPool.CollectGarbages();
                }
            }
        }

        private void Update()
        {

        }

        private ObjectPooler FindPoolerByKey(string poolKey)
        {
            for (int poolIndex = 0; poolIndex < _objectPools.Count; poolIndex++)
            {
                ObjectPooler currentPooler = _objectPools[poolIndex];
                if (currentPooler.poolKey.Equals(poolKey))
                {
                    return currentPooler;
                }
            }

            return null;
        }
    }
}


